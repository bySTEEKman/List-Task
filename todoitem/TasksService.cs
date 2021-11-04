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

        private void Add(TodoItem item)
        {
            _context.Tasks.Add(item);
            _context.SaveChanges();
        }


        public List<TodoItem> GetAll(List<TodoList> list)
        {
            List<TodoItem> allTasks = new List<TodoItem>();
            foreach (TodoList taskList in list)
            {
                foreach (TodoItem item in taskList.TaskList)
                {
                    allTasks.Add(item);
                }
            }
            return allTasks;
        }

        public List<TodoItem> GetAllTaskByTodoListId(TodoList list)
        {
            return list.TaskList;
        }

        public TodoItem CreateTask(TodoItem item, TodoList list, int listId)
        {
            ++lastIndex;
            item.Id = lastIndex;
            item.ListId = listId;
            list.TaskList.Add(item);
            return item;
        }
        public TodoItem DeleteTask(int id, List<TodoList> list)
        {
            var todoItem = new TodoItem();


            foreach (TodoList taskList in list)
            {
                foreach (TodoItem item in taskList.TaskList)
                {
                    if (item.Id == id)
                    {
                        todoItem = item;
                    }
                }
            }
            if (todoItem.ListId != 0)
            {
                int listIndex = --todoItem.ListId;
                list[listIndex].TaskList.Remove(todoItem);
            }
            else
            {
                todoItem = null;
            }

            return todoItem;
        }

        internal TodoItem GetTaskByIdAndListId(int id, TodoList todoList)
        {
            var todoItem = new TodoItem();
            foreach (TodoItem item in todoList.TaskList)
            {
                if (item.Id == id)
                {
                    todoItem = item;
                    return todoItem;
                }
                else
                {
                    todoItem = null;
                }
            }

            return todoItem;
        }

        public TodoItem PatchTaskWithListId(int id, TodoItem model, TodoList todoList)
        {
            int index = --id;
            var todoItem = todoList.TaskList[index] != null ? model : null;

            if (todoItem != null)
            {
                if (todoItem.Title != null)
                {
                    todoList.TaskList[index].Title = todoItem.Title;
                }
                if (todoItem.Description != null)
                {
                    todoList.TaskList[index].Description = todoItem.Description;
                }
                if (todoItem.DueDate != null)
                {
                    todoList.TaskList[index].DueDate = todoItem.DueDate;
                }
                if (todoItem.Done != null)
                {
                    todoList.TaskList[index].Done = todoItem.Done;
                }
                var patchedTask = todoList.TaskList[index];
                return patchedTask;
            };
            return todoItem;
        }

        public TodoItem DeleteTaskByListId(int id, TodoList todoList)
        {
            int index = --id;
            var todoItem = todoList.TaskList[index] != null ? todoList.TaskList[index] : null;

            if (todoItem != null)
            {
                todoList.TaskList.Remove(todoItem);
            }

            return todoItem;
        }

        public TodoItem PutTaskWithListId(int id, TodoItem model, TodoList todoList)
        {
            int index = --id;
            var todoItem = todoList.TaskList[index] != null ? model : null;

            if (todoItem != null)
            {
                todoList.TaskList[index] = todoItem;
            }

            return todoItem;
        }

        public TodoItem GetTaskById(int id, List<TodoList> list)
        {
            var todoItem = new TodoItem();
            foreach (TodoList todoList in list)
            {
                foreach (TodoItem item in todoList.TaskList)
                {
                    if (item.Id == id)
                    {
                        todoItem = item;
                        return todoItem;
                    }
                    else
                    {
                        todoItem = null;
                    }
                }
            }
            return todoItem;
        }

        public TodoItem PutTask(int id, TodoItem model, List<TodoList> list)
        {
            var todoItem = new TodoItem();
            int index = --id;

            foreach (TodoList todoList in list)
            {
                foreach (TodoItem item in todoList.TaskList)
                {
                    if (item.Id == id)
                    {
                        todoItem = item;
                        break;
                    }
                    else
                    {
                        todoItem = null;
                    }
                }
            }

            if (todoItem != null)
            {
                int listIndex = --todoItem.ListId;
                list[listIndex].TaskList[index] = model;
                todoItem = model;
            }

            return todoItem;
        }

        public TodoItem PatchTask(int id, TodoItem model, List<TodoList> list)
        {
            var todoItem = id <= lastIndex ? model : null;
            int index = --id;

            foreach (TodoList todoList in list)
            {
                foreach (TodoItem item in todoList.TaskList)
                {
                    if (item.Id == id && todoItem != null)
                    {
                        todoItem.ListId = item.ListId;
                    }
                }
            }

            if (todoItem != null)
            {
                int listIndex = --todoItem.ListId;
                if (todoItem.Title != null)
                {
                    list[listIndex].TaskList[index].Title = todoItem.Title;
                }
                if (todoItem.Description != null)
                {
                    list[listIndex].TaskList[index].Description = todoItem.Description;
                }
                if (todoItem.DueDate != null)
                {
                    list[listIndex].TaskList[index].DueDate = todoItem.DueDate;
                }
                if (todoItem.Done != null)
                {
                    list[listIndex].TaskList[index].Done = todoItem.Done;
                }
                var patchedTask = list[listIndex].TaskList[index];
                return patchedTask;
            }
            return todoItem;
        }
    }
}