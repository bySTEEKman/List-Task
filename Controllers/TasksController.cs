using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using todo_rest_api.Models;

namespace todo_rest_api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private TodoItemService tasksService;
        
        public TasksController(TodoItemService service)
        {
            this.tasksService = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetTask()
        {
            return tasksService.GetAll();
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
        public ActionResult<TodoItem> CreateTask(TodoItem item)
        {
            var createdTask = tasksService.CreateTask(item);
            
            return Created($"api/task/{createdTask.Id}", createdTask);
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
        public ActionResult<TodoItem> DeleteTaskById(int id)
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