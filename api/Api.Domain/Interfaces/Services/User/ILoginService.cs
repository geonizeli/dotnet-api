using System.Threading.Tasks;
using Api.Domain.Dtos;

namespace Api.Domain.Interfaces.Services.User
{
    public interface ILoginService
    {
        Task<LoginResultDto> FindByLoginAsync(LoginDto user);
    }
}