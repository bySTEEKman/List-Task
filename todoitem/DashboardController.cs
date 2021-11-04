using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using todo_rest_api.Models;

namespace todo_rest_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private DashboardService dashboardService;
        public DashboardController(DashboardService service)
        {
            this.dashboardService = service;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Dashboard>> GetTModels()
        {
            return new List<TModel> { };
        }
    }
}