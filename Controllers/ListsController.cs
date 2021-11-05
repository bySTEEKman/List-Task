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
    public class ListsController : ControllerBase
    {
        private TodoListService listService;
        public ListsController(TodoListService service)
        {
            this.listService = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoList>> GetTodoLists()
        {
            return listService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<TodoList> GetTodoListById(int id)
        {
            var todoList = listService.GetListByListId(id);

            if(todoList == null)
            {
                return NotFound();
            }

            return todoList;
        }

        [HttpPost]
        public ActionResult<TodoList> PostTodoList(TodoList model)
        {
            var createdList = listService.CreateList(model);
            
            return Created($"api/lists/{createdList.Id}", createdList);
        }

        [HttpPut("{id}")]
        public IActionResult PutTodoList(int id, TodoList model)
        {
            var todoList = listService.PutList(id, model);

            if (todoList == null)
            {
                return NotFound();
            }

            return Created($"/lists/{id}", todoList);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchTask (int id, TodoList model)
        {
            var todoList = listService.PatchList(id, model);

            if (todoList == null)
            {
                return NotFound();
            }

            return Ok(todoList);
        }

        [HttpDelete("{id}")]
        public ActionResult<TodoItem> DeleteTodoListById(int id)
        {
            var todoList = listService.DeleteList(id);

            if (todoList == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}