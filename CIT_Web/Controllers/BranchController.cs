using AutoMapper;
using CIT_Web.Models;
using CIT_Web.Models.Dto.Branch;
using CIT_Web.Models.Dto.Customer;
using CIT_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CIT_Web.Controllers
{
    public class BranchController : Controller
    {
        private readonly IBranchService _branchService;
        private readonly IMapper _mapper;

        public BranchController(IBranchService branchService, IMapper mapper)
        {
            _branchService = branchService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexBranch()
        {
            List<BranchDTO> list = new();
            var response = await _branchService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<BranchDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
    }
}
