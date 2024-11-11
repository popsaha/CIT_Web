using CIT_Web.Models.Dto.CrewCommander;
using CIT_Web.Models.Dto.Order;
using CIT_Web.Models.Dto.OrderRoute;
using CIT_Web.Models.Dto.Vehicle;

namespace CIT_Web.Models.ViewModel
{
    public class TaskCreateVM
    {

        public string OrderId { get; set; }
        public int OrderTypeID { get; set; }
        public int PriorityId { get; set; }
        public int PickUpTypeId { get; set; }
        public string OrderCreateDate { get; set; }
        public string? EndOnDate { get; set; }
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
        public bool fullDayCheck { get; set; }
        public string OrderNumber { get; set; }
        public bool OrderCreateStatus { get; set; }
        public int TaskId { get; set; } 
        public List<OrderType> OrderTypelist { get; set; }
        public List<PriorityMaster> PriorityMasterlist { get; set; }
        public List<PickTypeMaster> Picktypemasterlst { get; set; }
        public List<CustomerDTO> customerslist { get; set; }
        public List<TaskBranch> taskbranchlist { get; set; }
        public List<RepeatsTaskMaster> repeatsaskmasterslist { get; set; }
        public List<RepeatsInDaysMaster> repeatsInDaysMasterslist { get; set; }
        public List<VaultLovationMaster> vaultLovationMasters { get; set; }
        public List<TaskDTOlst> taskDTOlsts { get; set; }
        public List<OrderRoutes> Orderrouteslst { get; set; }
        public List<VehicleDTO> vehicledtolst { get; set; }
        public List<CrewCommanderDTO> crews { get; set; }
        public int IsEditTask { get; set; }
        public int UserRegionId { get; set; }

        //public List<OrderListDTO> orderLists { get; set; }

        public List<OrderRouteDTO> orderRouteDTOs { get; set; } = new List<OrderRouteDTO>();

    }
    public class CustomerDTO
    {
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
    }
    public class TaskBranch
    {
        public int BranchID { get; set; }
        public string? BranchName { get; set; }
    }
    public class OrderType
    {
        public int OrderTypeID { get; set; }
        public string? TypeName { get; set; }
    }

    public class PriorityMaster
    {
        public int PriorityId { get; set; }
        public string? PriorityName { get; set; }
    }

    public class PickTypeMaster
    {
        public int PickUpTypeId { get; set; }
        public string PickUpTypeName { get; set; }
    }
    public class RepeatsTaskMaster
    {
        public int RepeatId { get; set; }
        public string RepeatName { get; set; }
    }

    public class RepeatsInDaysMaster
    {
        public string RepeatDaysName { get; set; }
        public string RepeatInDay { get; set; }
    }

    public class VaultLovationMaster
    {
        public int VaultID { get; set; }
        public string VaultName { get; set; }
    }

    public class TaskDTOlst
    {
        public int TaskId { get; set; }
        public string OrderType { get; set; }
        public string PickupCustomerName { get; set; }
        public string DeliveryCustomerName { get; set; }
        public string OrderNumber { get; set; }
        public string PickupType { get; set; }
        public string PickupLocation { get; set; }
        public string DeliveryLocation { get; set; }
        public DateTime OrderDate { get; set; }
    }
    public class OrderRoutes
    {
        public int OrderRouteId { get; set; }
        public string RouteName { get; set; }
    }



}
