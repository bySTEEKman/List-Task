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

    [Route("api/lists/{listId?}/tasks")]
    [ApiController]
    public class ListsTasksController : ControllerBase
    {
        private TodoItemService tasksService;
        private TodoListService listService;

        public ListsTasksController(TodoItemService service, TodoListService listservice)
        {
            this.tasksService = service;
            this.listService = listservice;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetTask(int listId)
        {
            return tasksService.GetAllTaskByTodoListId(listService.GetListByListId(listId));
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetTaskById(int id, int listId)
        {
            var todoTask = tasksService.GetTaskByIdAndListId(id, listService.GetListByListId(listId));

            if(todoTask == null)
            {
                return NotFound();
            }

            return todoTask;
        }

        [HttpPost]
        public ActionResult<TodoItem> CreateTask(TodoItem todoTask,  int listId)
        {
            
            TodoItem createdTask = tasksService.CreateTask(todoTask, listService.GetListByListId(listId), listId);
            
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