using System;
using System.Collections.Generic;
using todo_rest_api.Models;

namespace todo_rest_api
{
    public class ListService
    {
        List<TodoList> todoLists = new List<TodoList>(
            new List () {Id = 1, OvnerName = "Yan", TaskList = new List<Task>()},
            new List () {Id = 2, OvnerName = "Oleh", TaskList = new List<Task>()}
        );
        private int lastId = 2;
        public List<TodoList> GetAll()
        {
            return todoLists;
        }

        public TodoList CreateItem(TodoList item)
        {
            item.Id = ++lastId;
            todoLists.Add(item);
            return item;
        }
        public TodoList DeleteItem(int id)
        {
            int index = --id;
            var todoList = id <= todoLists.Count ? todoLists[index] : null;

            if(todoList != null)
            {
                todoLists.Remove(todoList);
                foreach(TodoList item in todoLists)
                {
                    if(item.Id > id)
                    {
                        item.Id--;
                    }
                }
                lastId--;
            }

            return todoList;
        }

        public TodoList GetTodoItemById(int id)
        {
            var todoList = id <= todoLists.Count ? todoLists[--id] : null;
            return todoList;
        }

        public TodoList PutItem(int id, TodoList model)
        {
            var todoList = id <= todoLists.Count ? model : null;
            int index = --id;

            if(todoList != null)
            {
                todoList.Id = id;
                todoLists[index] = todoList;
            }

           return todoList;
        }

        public TodoList PatchItem(int id, TodoList model)
        {
            var todoList = id <= todoLists.Count ? model : null;
            int index = --id;
            
            if(todoList!= null)
            {
                if(todoList.OvnerName != null)
                {
                    todoLists[index].OvnerName = todoList.OvnerName;
                }
                if(todoList.TaskList != null)
                {
                    todoLists[index].TaskList = todoList.TaskList;
                }
                var patchedList = todoLists[index];
                return patchedList;
            }
            return todoList;
        }
    }
}