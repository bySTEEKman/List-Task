using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using todo_rest_api.Models;

namespace todo_rest_api.Controllers
{

    [Route("api/lists/{listId}/tasks")]
    [ApiController]
    public class ListsTasksController : ControllerBase
    {
        private TodoItemService tasksService;

        public ListsTasksController(TodoItemService service)
        {
            this.tasksService = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetTask(int listId, bool all)
        {
            return tasksService.GetAllTasksByTodoListId(listId, all);
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetTaskById(int id)
        {
            var todoTask = tasksService.GetTaskById(id);

            if(todoTask == null)
            {
                return NotFound();
            }

            return todoTask;
        }

        [HttpPost]
        public ActionResult<TodoItem> CreateTask(TodoItem todoTask, int listId)
        {
            todoTask.ListId = listId;

            // if (ModelState.IsValid) 
            // {
                TodoItem createdTask = tasksService.CreateTask(todoTask);                
                return Created($"api/task/{createdTask.Id}", createdTask);
                // } else 
            // {
            //     return new BadRequestObjectResult(ModelState);
            // }
        }

        [HttpPut("{id}")]
        public IActionResult PutTask(int id, TodoItem model)
        {
            var todoTask = tasksService.PutTask(id, model);

            if (todoTask == null)
            {
                return NotFound();
            }

            return Created($"/task/{id}", todoTask);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchTask (int id, TodoItem model)
        {

            var todoTask = tasksService.PatchTask(id, model);

            if (todoTask == null)
            {
                return NotFound();
            }

            return Ok(todoTask);
        }

        [HttpDelete("{id}")]
        public ActionResult<TodoItem> DeleteTaskById(int id, int listId)
        {
            var todoTask = tasksService.DeleteTask(id);

            if (todoTask == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}