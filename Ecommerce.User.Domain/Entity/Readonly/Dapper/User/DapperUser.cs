﻿namespace Ecommerce.User.Domain.Entity.Readonly.Dapper.User
{
    public class DapperUser
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
