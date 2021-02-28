namespace Api.Domain.Dtos
{
    public class LoginResultDto
    {
        public bool authenticated { get; set; }
        public string create { get; set; }
        public string expiration { get; set; }
        public string accessToken { get; set; }
        public string userName { get; set; }
        public string message { get; set; }
    }
}