using System;
using System.Collections.Generic;
using todo_rest_api.Models;

namespace todo_rest_api
{
    public class TaskService
    {
        private List<Task> todoItems = new List<Task> {
                new TodoItem () { Id = 1, Title = "Implement read"},
                new TodoItem () { Id = 2, Title = "Implement create"}
             };
        private int lastId = 2;
        public List<Task> GetAll()
        {
            return todoItems;
        }

        public Task CreateItem(Task item, List<Task> todoItems)
        {
            item.Id = ++lastId;
            todoItems.Add(item);
            return item;
        }
        public Task DeleteItem(int id, List<Task> todoItems)
        {
            var todoItem = id <= todoItems.Count ? todoItems[--id] : null;

            if(todoItem != null)
            {
            todoItems.Remove(todoItem);
            foreach(Task item in todoItems)
            {
                if(item.Id > id)
                {
                    item.Id--;
                }
            }
            lastId--;
            }

            return todoItem;
        }

        public Task GetTodoItemById(int id, List<Task> todoItems)
        {
            var todoItem = id <= todoItems.Count ? todoItems[--id] : null;
            return todoItem;
        }

        public Task PutItem(int id, Task model)
        {
            var todoItem = id <= todoItems.Count ? model : null;
            int index = --id;

            if(todoItem != null)
            {
                todoItem.Id = id;
                todoItems[index] = todoItem;
            }

           return todoItem;
        }

        public Task PatchItem(int id, Task model)
        {
            var todoItem = id <= todoItems.Count ? model : null;
            int index = --id;
            
            if(todoItem != null)
            {
                if(todoItem.Title != null)
                {
                    todoItems[index].Title = todoItem.Title;
                }
                if(todoItem.Description != null)
                {
                    todoItems[index].Description = todoItem.Description;
                }
                if(todoItem.DueDate != null)
                {
                    todoItems[index].DueDate = todoItem.DueDate;
                }
                if(todoItem.Done != null)
                {
                    todoItems[index].Done = todoItem.Done;
                }
                var patchedItem = todoItems[index];
                return patchedItem;
            }
            return todoItem;
        }
    }
}