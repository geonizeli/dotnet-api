using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repository;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;
        public LoginService(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<object> FindByLoginAsync(UserEntity user)
        {
            var baseUser = new UserEntity();

            if (user != null && !string.IsNullOrWhiteSpace(user.Email)) {
                baseUser = await _repository.FindByLoginAsync(user.Email);
                if (baseUser != null) {
                    return baseUser;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}