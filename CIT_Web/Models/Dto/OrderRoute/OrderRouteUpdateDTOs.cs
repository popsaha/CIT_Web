namespace CIT_Web.Models.Dto.OrderRoute
{
    public class OrderRouteUpdateDTOs
    {
        public int OrderRouteId { get; set; }
        public string RouteName { get; set; }
        public string? RouteDescription { get; set; }
        public bool? IsActive { get; set; }
    }
}
