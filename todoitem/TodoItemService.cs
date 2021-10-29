using System;
using System.Collections.Generic;
using todo_rest_api.Models;

namespace todo_rest_api
{
    public class TodoItemService
    {
        private List<TodoItem> todoItems = new List<TodoItem> {
                new TodoItem () { Id = 1, Title = "Implement read"},
                new TodoItem () { Id = 2, Title = "Implement create"}
             };
        private int lastId = 2;
        public List<TodoItem> GetAll()
        {
            return todoItems;
        }

        public TodoItem CreateItem(TodoItem item)
        {
            item.Id = ++lastId;
            todoItems.Add(item);
            return item;
        }
        public TodoItem DeleteItem(int id)
        {
            var todoItem = id <= todoItems.Count ? todoItems[--id] : null;

            if(todoItem != null)
            {
            todoItems.Remove(todoItem);
            foreach(TodoItem item in todoItems)
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

        public TodoItem GetTodoItemById(int id)
        {
            var todoItem = id <= todoItems.Count ? todoItems[--id] : null;
            return todoItem;
        }

        public TodoItem PutItem(int id, TodoItem model)
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

        public TodoItem PatchItem(int id, TodoItem model)
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