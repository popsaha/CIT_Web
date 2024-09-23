using System.ComponentModel.DataAnnotations;

namespace CIT_Web.Models.Dto.Task
{
    public class TaskDTO
    {
        public string OrderId { get; set; }
        public int OrderTypeID { get; set; }
        public int PriorityId { get; set; }
        public int PickUpTypeId { get; set; }
        public int CustomerId { get; set; }
        public int BranchID { get; set; }
        public int CustomerRecipiantId { get; set; }
        public int CustomerRecipiantLocationId { get; set; }
        public int RepeatId { get; set; }

    }
}
