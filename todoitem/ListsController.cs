using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using List-Task.Models;

namespace todo_rest_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListsController : ControllerBase
    {
        private ListService listService;
        public ListsController(ListService service)
        {
            this.listService = service;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<TodoList>> GetTodoLists()
        {
            return listService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<TodoList> GetTodoListlById(int id)
        {
            var todoList = todoItemService.GetTodoListById(id);

            if(todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpPost("")]
        public ActionResult<TModel> PostTModel(TModel model)
        {
            return null;
        }

        [HttpPut("{id}")]
        public IActionResult PutTModel(int id, TModel model)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<TModel> DeleteTModelById(int id)
        {
            return null;
        }
    }
}