using Ecommerce.User.CrossCutting.Entity;

namespace Ecommerce.User.Domain.Entity.User
{
    public class User : Entity<int>
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
