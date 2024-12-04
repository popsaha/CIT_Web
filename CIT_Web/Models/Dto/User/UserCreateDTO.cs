using System.ComponentModel.DataAnnotations;

namespace CIT_Web.Models.Dto.User
{
    public class UserCreateDTO
    {
        public int? UserId { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "RoleName is required.")]
        public string RoleName { get; set; }
        public string RegionName { get; set; }
    }
}
