using CIT_Web.Models.Dto.Branch;

namespace CIT_Web.Models.ViewModel
{
    public class BranchVM
    {
        public List<BranchDTO> branchDTOs { get; set; }
        public BranchCreateDTO branchCreateDTO { get; set; }
    }
}
