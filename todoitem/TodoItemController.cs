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
    public class TodoItemController : ControllerBase
    {
        private TodoItemService todoItemService;

        public TodoItemController(TodoItemService service)
        {
            this.todoItemService = service;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<TodoItem>> GetTodoItems()
        {
            return todoItemService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetTodoItemById(int id)
        {
            var todoItem = todoItemService.GetTodoItemById(id);

            if(todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpPost("")]
        public ActionResult<TodoItem> CreateTodoItem(TodoItem todoItem)
        {
            TodoItem createdItem = todoItemService.CreateItem(todoItem);
            
            return Created($"api/todoitem/{createdItem.Id}", createdItem);
        }

        [HttpPut("{id}")]
        public IActionResult PutTodoItem(int id, TodoItem model)
        {
            var todoItem = todoItemService.PutItem(id, model);

            if (todoItem == null)
            {
                return NotFound();
            }

            return Created($"/todoitem/{id}", todoItem);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchTodoItem(int id, TodoItem model)
        {
            var todoItem = todoItemService.PatchItem(id, model);

            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        [HttpDelete("{id}")]
        public ActionResult<TodoItem> DeleteTodoItemById(int id)
        {
            var todoItem = todoItemService.DeleteItem(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}