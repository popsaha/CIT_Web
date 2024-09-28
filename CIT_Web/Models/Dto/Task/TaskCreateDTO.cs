namespace CIT_Web.Models.Dto.Task
{
    public class TaskCreateDTO
    {
        public string OrderId { get; set; }
        public int OrderTypeID { get; set; }
        public int PriorityId { get; set; }
        public int PickUpTypeId { get; set; }
        public string OrderCreateDate { get; set; }
        public string EndOnDate { get; set; }
        public int CustomerId { get; set; }
        public int BranchID { get; set; }
        public int CustomerRecipiantId { get; set; }
        public int CustomerRecipiantLocationId { get; set; }
        public int RepeatId { get; set; }
        public string RepeatDaysName { get; set; }
        public int VaultID { get; set; }
        public bool isVault { get; set; }
        public bool isVaultFinal { get; set; }
        public int OrderRouteId { get; set; }
        public bool NewVehicleRequired { get; set; }
    }
}
