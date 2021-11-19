using System;
using System.Collections.Generic;
using todo_rest_api.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace todo_rest_api
{
    public class TodoItemService
    {
        private TodoListContext _context;

        public TodoItemService(TodoListContext context)
        {
            this._context = context;
        }

        private void Add(TodoItem item)
        {
            _context.Tasks.Add(item);
            _context.SaveChanges();
        }


        private void Delete(TodoItem item)
        {
            _context.Tasks.Remove(item);
            _context.SaveChanges();
        }

        private TodoItem PutItem(TodoItem item)
        {
            var task = _context.Tasks.Where(t => t.Id == item.Id).Single();

            task.Title = item.Title;
            task.Description = item.Description;
            task.DueDate = item.DueDate;
            task.Done = item.Done;

            _context.SaveChanges();
            return task;
        }

        private TodoItem GetItemById(int id)
        {
            var todoItem = _context.Tasks
                .Where(t => t.Id == id)
                .SingleOrDefault();

            return todoItem;
        }

        // Work with http request
        public List<TodoItem> GetAll()
        {
            var allTasks = _context.Tasks
                .ToList();

            return allTasks;
        }

        public TodoItem CreateTask(TodoItem item)
        {
            var todoList = _context.Lists
                .Where(l => l.Id == item.ListId)
                .Single();

            item.TodoList = todoList;

            Add(item);

            return item;
        }
        public TodoItem DeleteTask(int id)
        {
            var todoItem = GetItemById(id);

            if (todoItem != null)
            {
                Delete(todoItem);
            }

            return todoItem;
        }

        public TodoItem GetTaskById(int id)
        {
            var todoItem = GetItemById(id);

            return todoItem;
        }

        public TodoItem PutTask(int id, TodoItem model)
        {
            var todoItem = GetItemById(id);

            if (todoItem != null)
            {
                return PutItem(model); ;
            }

            return todoItem;
        }

        public TodoItem PatchTask(int id, TodoItem model)
        {
            var todoItem = GetItemById(id);

            if (todoItem != null)
            {
                if (model.Title != null)
                {
                    todoItem.Title = model.Title;
                }
                if (model.Description != null)
                {
                    todoItem.Description = model.Description;
                }
                if (model.DueDate != null)
                {
                    todoItem.DueDate = model.DueDate;
                }
                todoItem.Done = model.Done;
                _context.SaveChanges();

                var patchedTask = GetItemById(id);

                return patchedTask;
            }
            return todoItem;
        }

        public List<TodoItem> GetAllTasksByTodoListId(int listId, bool all)
        {
                return _context.Tasks.Where(t => t.ListId == listId && (all || !t.Done)).ToList();
        }

        public List<TodoItemDTO> GetTaskCollectionForToday()
        {
            var todayTasks = _context.Tasks
                .Where(t => t.DueDate == DateTime.Today)
                .Include(t => t.TodoList)
                .Select(t => new TodoItemDTO(t))
                .ToList();
            

            return todayTasks;
        }
    }
}