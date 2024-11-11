using CIT_Web.Models.Dto.User;

namespace CIT_Web.Models.Dto.Login
{
    public class LoginResponseDTO
    {
        public UserDTO User { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
    public class Root
    {
        public string unique_name { get; set; }
        public string role { get; set; }
        public int nbf { get; set; }
        public int exp { get; set; }
        public int iat { get; set; }
    }
}
