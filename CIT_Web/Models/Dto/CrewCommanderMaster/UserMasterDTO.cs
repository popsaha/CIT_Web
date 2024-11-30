﻿namespace CIT_Web.Models.Dto.CrewCommanderMaster
{
    public class UserMasterDTO
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        //public string RoleName { get; set; }
        public string? DataSource { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int? IsDeleted { get; set; }
        public Guid? UUID { get; set; }
    }
}
