using System;
using System.Collections.Generic;

namespace DOMAIN.Models
{
    public class User : Base.Base
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}