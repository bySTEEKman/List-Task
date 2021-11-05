using System;
using System.Linq;
using System.Collections.Generic;
using todo_rest_api.Models;

namespace todo_rest_api
{
    public class TodoListService
    {
        private TodoListContext _context;

        public TodoListService(TodoListContext context)
        {
            this._context = context;
        }

        private void Add(TodoList item)
        {
            _context.Lists.Add(item);
            _context.SaveChanges();
        }

        
        public TodoList GetListByListId(int listId)
        {
            var todoList = _context.Lists
                .Where(l => l.Id == listId)
                .Single();

            return todoList;
        }
        public List<TodoList> GetAll()
        {
            var todoLists = _context.Lists
                .ToList();

            return todoLists;
        }

        public TodoList CreateList(TodoList item)
        {
            _context.Lists.Add(item);
            var todoList = _context.Lists
                .Where(l => l.OwnerName == item.OwnerName)
                .Single();

            return todoList;
        }
        public TodoList DeleteList(int id)
        {
            var todoList = _context.Lists
                .Where(l => l.Id == id)
                .Single();

            _context.Lists.Remove(todoList);

            return todoList;
        }

        public TodoList PutList(int id, TodoList model)
        {
            var todoList = _context.Lists
                .Where(l => l.Id == id)
                .Select(l => l = model)
                .Single();

            model.Id = id;

            _context.Lists.Remove(todoList);
            _context.Lists.Add(model);

            va
                
           return newTodoList;
        }

        public TodoList PatchList(int id, TodoList model)
        {
            var todoList = _context.Lists
                .Where(l => l.Id == id)
                .Single();
            
            if(todoList!= null)
            {
                if(model.OwnerName != null)
                {
                    _context.Lists
                    .Where(l => l.Id == id)
                    .Select(l => l.OwnerName = model.OwnerName);
                }
                if(model.TaskList != null)
                {
                    _context.Lists
                    .Where(l => l.Id == id)
                    .Select(l => l.TaskList = model.TaskList);
                }
                var patchedList = _context.Lists
                    .Where(l => l.Id == id)
                    .Single();
                return patchedList;
            }
            return todoList;
        }
    }
}