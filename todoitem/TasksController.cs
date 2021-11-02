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
        private TasksService tasksService;
        private ListService listService;
        
        public TasksController(TasksService service, ListService listservice)
        {
            this.tasksService = service;
            this.listService = listservice;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetTask()
        {
            return tasksService.GetAll(listService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetTaskById(int id)
        {
            var todoTask = tasksService.GetTaskById(id, listService.GetAll());

            if(todoTask == null)
            {
                return NotFound();
            }

            return todoTask;
        }

        [HttpPost]
        public ActionResult<TodoItem> CreateTask(TodoItem todoTask, int listId)
        {
            TodoItem createdTask = tasksService.CreateTask(todoTask, listService.GetListByListId(listId), listId);
            
            return Created($"api/task/{createdTask.Id}", createdTask);
        }

        [HttpPut("{id}")]
        public IActionResult PutTask(int id, TodoItem model)
        {   
            var todoTask = tasksService.PutTask(id, model, listService.GetAll());

            if (todoTask == null)
            {
                return NotFound();
            }

            return Created($"/task/{id}", todoTask);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchTask (int id, TodoItem model)
        {
            var todoTask = tasksService.PatchTask(id, model, listService.GetAll());

            if (todoTask == null)
            {
                return NotFound();
            }

            return Ok(todoTask);
        }

        [HttpDelete("{id}")]
        public ActionResult<TodoItem> DeleteTaskById(int id)
        {
            var todoTask = tasksService.DeleteTask(id, listService.GetAll());

            if (todoTask == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}