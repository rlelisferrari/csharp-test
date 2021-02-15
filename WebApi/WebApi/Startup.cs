using System.Text;
using DATA.Contexts;
using DATA.Repositories;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();

            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
            services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
            services.AddScoped(typeof(IOrderContainsProductRepository), typeof(OrderContainsProductRepository));

            services.AddScoped<UserService>();
            services.AddScoped<ProductService>();
            services.AddScoped<OrderService>();

            services.AddDbContext<AppDbContext>(
                options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DBConnection")));

            var key = Encoding.UTF8.GetBytes(Configuration["SecurityKey"]);

            services.AddAuthentication(
                    item =>
                    {
                        item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                .AddJwtBearer(
                    item =>
                    {
                        item.RequireHttpsMetadata = false;
                        item.SaveToken = true;
                        item.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key)
                        };
                    });

            services.AddSwaggerGen(
                c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "API Model DDD", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Model DDD"); });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(item => item.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}