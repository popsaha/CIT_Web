namespace CIT_Web.Models.Dto.CrewCommanderMaster
{
    public class CrewUserCreateDTO
    {
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        public string RegionName { get; set; }
        public bool? IsActive { get; set; }
    }
}
