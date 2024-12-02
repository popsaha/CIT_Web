namespace CIT_Web.Models.Reports
{
    public class Branch
    {
        public int statusCode { get; set; }
        public List<BranchDetails> result { get; set; }
    }

    public class BranchDetails
    {
        public string branchID { get; set; }
        public string branchName { get; set; }
    }
}
