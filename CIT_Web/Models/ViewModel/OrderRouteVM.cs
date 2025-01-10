using CIT_Web.Models.Dto.OrderRoute;

namespace CIT_Web.Models.ViewModel
{
    public class OrderRouteVM
    {
        public List<OrderRouteDTO> OrderRoutesList { get; set; }
        public OrderRouteCreateDTO orderRouteCreateDTOs { get; set; }

        //public OrderRouteUpdateDTOs orderRouteUpdateDTOs { get; set; }

    }
}
