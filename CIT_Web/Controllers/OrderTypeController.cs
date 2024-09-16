using AutoMapper;
using CIT_Web.Models;
using CIT_Web.Models.Dto.OrderType;
using CIT_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CIT_Web.Controllers
{
    public class OrderTypeController : Controller
    {
        private readonly IOrderTypeService _orderType;
        private readonly IMapper _mapper;

        public OrderTypeController(IOrderTypeService orderType, IMapper mapper)
        {
            _orderType = orderType;
            _mapper = mapper;
        }
        public async  Task<IActionResult> IndexOrderType()
        {
            List<OrderTypeDTO> list = new();
            var response = await _orderType.GetAllAsync<APIResponse>();
            if(response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<OrderTypeDTO>>(Convert.ToString(response.Result));

            }
            return View(list);
        }
    }
}
