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
            var itemList = _context.Tasks
                .Where(t => t.ListId == listId)
                .ToList();

            var todoList = _context.Lists
                .Where(l => l.Id == listId)
                .SingleOrDefault();

            if (todoList != null)
            {
                todoList.TaskList = itemList;
            }

            return todoList;
        }

         private void PachedItemTaskList(TodoList list)
        {
            _context.Tasks
                .Where(t => t.ListId == list.Id)
                .ToList()
                .ForEach(t => t.TodoList = list);

            _context.SaveChanges();
        }
        
        private void DeleteTasksWithList(TodoList list)
        {
            var delList = _context.Tasks
                .Where(t => t.ListId == list.Id)
                .ToList();

            foreach(TodoItem item in delList)
            {
                _context.Tasks.Remove(item);
            }
        }


        public List<TodoList> GetAll()
        {
            List<TodoList> todoLists = _context.Lists.ToList();
            List<TodoList> answerList = new List<TodoList>();
            foreach (TodoList list in todoLists)
            {
                var todoItem = _context.Tasks
                    .Where(t => t.ListId == list.Id)
                    .ToList();

                TodoList tList = list;
                tList.TaskList = todoItem;

                answerList.Add(tList);
            }

            return todoLists;
        }

        public TodoList CreateList(TodoList item)
        {
            Add(item);

            var todoList = _context.Lists
                .Where(l => l.Title == item.Title)
                .Single();

            return todoList;
        }
        public TodoList DeleteList(int id)
        {
            var todoList = _context.Lists
                .Where(l => l.Id == id)
                .SingleOrDefault();

            if (todoList != null)
            {
                DeleteTasksWithList(todoList);
                _context.Lists.Remove(todoList);
                
                _context.SaveChanges();
            }

            return todoList;
        }

        public TodoList PutList(int id, TodoList model)
        {
            var todoList = _context.Lists
                .Where(l => l.Id == id)
                .SingleOrDefault();

            if (todoList != null)
            {
                model.Id = id;

                var deleteTasks = _context.Tasks.Where(t => t.ListId == id).ToList();

                foreach (TodoItem item in deleteTasks)
                {
                    _context.Tasks.Remove(item);
                }

                if (model.TaskList != null)
                {
                    foreach (TodoItem item in model.TaskList)
                    {
                        _context.Tasks.Add(item);
                    }
                }

                _context.Lists.Remove(todoList);
                _context.Lists.Add(model);

                PachedItemTaskList(model);

                _context.SaveChanges();

                return model;
            }

            return todoList;
        }

        public TodoList PatchList(int id, TodoList model)
        {
            var todoList = _context.Lists
                .Where(l => l.Id == id)
                .SingleOrDefault();

            if (todoList != null)
            {
                if (model.Title != null)
                {
                    _context.Lists
                    .Where(l => l.Id == id)
                    .ToList()
                    .ForEach(l => l.Title = model.Title);

                    _context.SaveChanges();
                }
                if (model.TaskList != null)
                {
                    _context.Lists
                    .Where(l => l.Id == id)
                    .ToList()
                    .ForEach(l => l.TaskList = model.TaskList);

                    _context.SaveChanges();
                }

                var patchedList = _context.Lists
                    .Where(l => l.Id == id)
                    .Single();

                PachedItemTaskList(patchedList);

                return patchedList;
            }
            return todoList;
        }
    }
}