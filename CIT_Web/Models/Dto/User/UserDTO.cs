namespace CIT_Web.Models.Dto.User
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string role { get; set; }
        public string userID { get; set; }
        public int RegionID { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
