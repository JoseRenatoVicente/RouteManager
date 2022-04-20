﻿using RouteManager.Domain.Entities.Base;

namespace RouteManager.Domain.Entities.Identity
{
    public class User : EntityBase
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}