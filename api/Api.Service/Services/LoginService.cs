using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repository;
using Api.Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;
        private SigningConfigurations _signingConfigurations { get; set; }
        public IConfiguration _configuration { get; set; }
        public LoginService(
            IUserRepository repository,
            SigningConfigurations signingConfigurations,
            IConfiguration configuration
        )
        {
            _repository = repository;
            _signingConfigurations = signingConfigurations;
            _configuration = configuration;
        }
        public async Task<LoginResultDto> FindByLoginAsync(LoginDto user)
        {
            var baseUser = new UserEntity();

            if (user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
                baseUser = await _repository.FindByLoginAsync(user.Email);

                if (baseUser == null)
                {
                    return new LoginResultDto
                    {
                        authenticated = false,
                        message = "Authentication failed"
                    };
                }
                else
                {
                    var identity = new ClaimsIdentity(
                        new GenericIdentity(baseUser.Email),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, baseUser.Email)
                        }
                    );

                    var createDate = DateTime.Now;
                    var expirationDate = createDate + TimeSpan.FromSeconds(
                        Convert.ToInt32(
                            Environment.GetEnvironmentVariable("AUTH_DURATION")
                        )
                    );

                    var handler = new JwtSecurityTokenHandler();
                    var token = CreateToken(identity, createDate, expirationDate, handler);

                    return SuccessObject(createDate, expirationDate, token, user);
                }

            }
            else
            {
                return new LoginResultDto
                {
                    authenticated = false,
                    message = "Authentication failed"
                };
            }
        }
        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = Environment.GetEnvironmentVariable("AUTH_ISSUER"),
                Audience = Environment.GetEnvironmentVariable("AUTH_AUDIENCE"),
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate,
            });

            return handler.WriteToken(securityToken);
        }
        private LoginResultDto SuccessObject(DateTime createDate, DateTime expirationDate, string token, LoginDto user)
        {
            return new LoginResultDto
            {
                authenticated = true,
                create = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                userName = user.Email,
                message = "Authentication successful"
            };
        }
    }
}