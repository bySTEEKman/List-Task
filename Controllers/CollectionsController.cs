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
        private TodoItemService todoItemService;
        public CollectionsController(TodoItemService service)
        {
            this.todoItemService = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoItemDTO>> GetAll()
        {
            return todoItemService.GetTaskCollectionForToday();
        }
    }
}