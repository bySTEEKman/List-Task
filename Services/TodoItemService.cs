using System;
using System.Collections.Generic;
using todo_rest_api.Models;
using System.Linq;

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

        private void PutItem(TodoItem item)
        {
            var todItem = _context.Tasks.Where(t => t.Id == item.Id).Single(); 

            _context.Tasks.Remove(todItem);
            _context.Tasks.Add(item);

            _context.SaveChanges();
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
                var changedItem = model;

                changedItem.Id = todoItem.Id;
                changedItem.ListId = todoItem.ListId;
                changedItem.TodoList = todoItem.TodoList;

                PutItem(changedItem);

                return changedItem;
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
                    _context.Tasks
                    .Where(t => t.Id == id)
                    .ToList()
                    .ForEach(t => t.Title = model.Title);

                    _context.SaveChanges();
                }
                if (model.Description != null)
                {
                    _context.Tasks
                    .Where(t => t.Id == id)
                    .ToList()
                    .ForEach(t => t.Description = model.Description);

                    _context.SaveChanges();
                }
                if (model.DueDate != null)
                {
                    _context.Tasks
                    .Where(t => t.Id == id)
                    .ToList()
                    .ForEach(t => t.DueDate = model.DueDate);

                    _context.SaveChanges();
                }
                if (todoItem.Done != false)
                {
                    _context.Tasks
                    .Where(t => t.Id == id)
                    .ToList()
                    .ForEach(t => t.Done = model.Done);

                    _context.SaveChanges();
                }

                var patchedTask = GetItemById(id);

                return patchedTask;
            }
            return todoItem;
        }
    }
}