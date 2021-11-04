using System;
using System.Collections.Generic;
using todo_rest_api.Models;

namespace todo_rest_api
{
    public class TodoItemService
    {
        private TodoListContext _context;

        public TodoItemService(TodoListContext context)
        {
            this._context = context;
        }

        // Work with database
        private void Add(TodoItem item)
        {
            _context.Tasks.Add(item);
            _context.SaveChanges();
        }

        private void AddToList(TodoItem item)
        {
            _context.Lists
            .Where(l => l.Id == item.ListId)
            .TaskList.Add(item);
        }

        private void Delete(TodoItem item)
        {
            _context.Tasks.Remove(item);
            _context.SaveChanges();
        }

        private void DeleteFromList(TodoItem item)
        {
            _context.Lists
            .Where(l => l.Id == item.ListId)
            .TaskList.Remove(item);
        }

        private void PutItem(TodoItem item)
        {
            _context.Tasks
            .Where(t => t.Id == item.id)
            .Select(t => t = item);
        }

        private void PutItemToList(TodoItem item)
        {
            _context.Lists
            .Where(l => l.Id == item.ListId)
            .TaskList
            .Where(t => t.Id == item.Id)
            .Select(t => t = item);
        }

        private TodoItem GetItemById(int id)
        {
            var todoItem = _context.Tasks
                .Where(t => t.Id == id)
                .Single();

            return todoItem;
        }

        // Work with http request
        public List<TodoItem> GetAll(List<TodoList> list)
        {
            var allTasks = _context.Tasks
                .ToList();

            return allTasks;
        }

        public TodoItem CreateTask(TodoItem item, int listId)
        {
            var todoList = _context.Lists
                .Where(l => l.Id == listId)
                .Single();

            item.TodoList = todoList;

            Add(item);
            AddToList(item);

            return item;
        }
        public TodoItem DeleteTask(int id)
        {
            var todoItem = GetItemById(id);

            if(todoItem != null)
            {
                Delete(todoItem);
                DeleteFromList(todoItem);
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

            if(todoItem != null)
            {
                var changedItem = model;

                changedItem.Id = todoItem.Id;
                changedItem.ListId = todoItem.ListId;
                changedItem.TaskList = todoItem.TaskList;

                PutItem(changedItem);
                PutItemToList(changedItem);

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
                    .Where(t => t.Id = id)
                    .Select(t => t.Title = model.Title);
                }
                if (model.Description != null)
                {
                    _context.Tasks
                    .Where(t => t.Id = id)
                    .Select(t => t.Description = model.Description);
                }
                if (model.DueDate != null)
                {
                    _context.Tasks
                    .Where(t => t.Id = id)
                    .Select(t => t.DueDate = model.DueDate);
                }
                if (todoItem.Done != null)
                {
                    _context.Tasks
                    .Where(t => t.Id = id)
                    .Select(t => t.Done = model.Done);
                }

                var patchedTask = GetItemById(id);
                PutItemToList(patchedTask);

                return patchedTask;
            }
            return todoItem;
        }
    }
}