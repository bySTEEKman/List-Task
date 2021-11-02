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

    [Route("api/lists/{listId?}/task")]
    [ApiController]
    public class ListsTasksController : ControllerBase
    {
        private TasksService tasksService;
        private ListService listService;

        public ListsTasksController(TasksService service, ListService listservice)
        {
            this.tasksService = service;
            this.listService = listservice;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetTask(int listId)
        {
            if(listId != 0)
            {
                return tasksService.GetAllTaskByTodoListId(listService.GetListByListId(listId));
            }
            else
            {
                return tasksService.GetAll(listService.GetAll());
            }
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetTaskById(int id, int listId)
        {
            var todoTask = new TodoItem();
            if(listId != 0)
            {
                todoTask = tasksService.GetTaskByIdAndListId(id, listService.GetListByListId(listId));
            }
            else
            {
                todoTask = tasksService.GetTaskById(id, listService.GetAll());
            }
            
            if(todoTask == null)
            {
                return NotFound();
            }

            return todoTask;
        }

        [HttpPost]
        public ActionResult<TodoItem> CreateTask(TodoItem todoTask,  int listId)
        {
            var createdTask = new TodoItem();
            if(listId != 0)
            {
                createdTask = tasksService.CreateTask(todoTask, listService.GetListByListId(listId), listId);
            }
            else
            {
                createdTask = tasksService.CreateTask(todoTask, listService.GetListByListId(listId), listId);
            }
            
            return Created($"api/task/{createdTask.Id}", createdTask);
        }

        [HttpPut("{id}")]
        public IActionResult PutTask(int id, TodoItem model,  int listId)
        {
            
            var todoTask = tasksService.PutTaskWithListId(id, model, listService.GetListByListId(listId));

            if (todoTask == null)
            {
                return NotFound();
            }

            return Created($"/task/{id}", todoTask);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchTask (int id, TodoItem model,  int listId)
        {

            var todoTask = tasksService.PatchTaskWithListId(id, model, listService.GetListByListId(listId));

            if (todoTask == null)
            {
                return NotFound();
            }

            return Ok(todoTask);
        }

        [HttpDelete("{id}")]
        public ActionResult<TodoItem> DeleteTaskById(int id, int listId)
        {

            var todoTask = tasksService.DeleteTaskByListId(id, listService.GetListByListId(listId));

            if (todoTask == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}