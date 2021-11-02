using System;
using System.Collections.Generic;
using todo_rest_api.Models;

namespace todo_rest_api
{
    public class ListService
    {
        List<TodoList> todoLists = new List<TodoList>{
            new TodoList () {Id = 1, OwnerName = "Yan", TaskList = new List<TodoItem>()},
            new TodoList () {Id = 2, OwnerName = "Oleh", TaskList = new List<TodoItem>()}
        };
        int lastIndex = 2;

        public TodoList GetListByListId(int listId)
        {
            return todoLists[--listId];
        }
        public List<TodoList> GetAll()
        {
            return todoLists;
        }

        public TodoList CreateList(TodoList item)
        {
            ++lastIndex;
            item.Id = lastIndex;
            todoLists.Add(item);
            return item;
        }
        public TodoList DeleteList(int id)
        {
            int index = --id;
            var todoList = id <= todoLists.Count ? todoLists[index] : null;

            if(todoList != null)
            {
                todoLists.Remove(todoList);
            }

            return todoList;
        }

        public TodoList GetListById(int id)
        {
            var todoList = id <= todoLists.Count ? todoLists[--id] : null;
            return todoList;
        }

        public TodoList PutList(int id, TodoList model)
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

        public TodoList PatchList(int id, TodoList model)
        {
            var todoList = id <= todoLists.Count ? model : null;
            int index = --id;
            
            if(todoList!= null)
            {
                if(todoList.OwnerName != null)
                {
                    todoLists[index].OwnerName = todoList.OwnerName;
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