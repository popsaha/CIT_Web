﻿namespace CIT_Web.Models.ViewModel
{
    public class TaskCreateVM
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
        public List<OrderType> OrderTypelist { get; set; }
        public List<PriorityMaster> PriorityMasterlist { get; set; }
        public List<PickTypeMaster> Picktypemasterlst { get; set; }
        public List<CustomerDTO> customerslist { get; set; }
        public List<TaskBranch> taskbranchlist { get; set; }
        public List<RepeatsTaskMaster> repeatsaskmasterslist { get; set; }
        public List<RepeatsInDaysMaster> repeatsInDaysMasterslist { get; set; }
        public List<VaultLovationMaster> vaultLovationMasters { get; set; }
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
}