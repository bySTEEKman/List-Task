using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using todo_rest_api.Models;

namespace todo_rest_api.Controllers
{
    [Route("api/collection/today")]
    [ApiController]
    public class CollectionsController : ControllerBase
    {
        private CollectionsService collectionsService;
        public CollectionsController(CollectionsService service)
        {
            this.collectionsService = service;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<TodoItemDTO>> GetTModels()
        {
            return collectionsService.GetTaskCollectionForToday();
        }
    }
}