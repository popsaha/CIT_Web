using AutoMapper;
using CIT_Web.Models;
using CIT_Web.Models.Dto.Region;
using CIT_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CIT_Web.Controllers
{
    public class RegionController : Controller
    {
        private readonly IRegionService _regionService;
        private readonly IMapper _mapper;

        public RegionController(IRegionService regionService, IMapper mapper)
        {
            _regionService = regionService;
            _mapper = mapper;
        }
        public async Task<IActionResult> IndexRegion()
        {
            List<RegionDTO> list = new();
            var response = await _regionService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                
                list = JsonConvert.DeserializeObject<List<RegionDTO>>(Convert.ToString(response.Result));

            }
            return View(list);
        }
    }
}
