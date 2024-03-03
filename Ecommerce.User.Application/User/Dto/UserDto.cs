using Ecommerce.User.CrossCutting.Entity;

namespace Ecommerce.User.Application.User.Dto
{
    public class UserDto : OperationEntity<int>
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
