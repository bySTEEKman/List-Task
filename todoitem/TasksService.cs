using System;
using System.Collections.Generic;
using todo_rest_api.Models;

namespace todo_rest_api
{
    public class TasksService
    {
        public List<TodoItem> GetAll(List<TodoList> list)
        {
            List<TodoItem> allTasks = new List<TodoItem>();
            foreach(TodoList taskList in list)
            {
                foreach(TodoItem item in taskList.TaskList)
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
            item.Id = list.TaskList.Count + 1;
            item.ListId = listId;
            list.TaskList.Add(item);
            return item;
        }
        public TodoItem DeleteTask(int id, TodoList list)
        {
            var todoTask = id <= list.TaskList.Count ? list.TaskList[--id] : null;

            if(todoTask != null)
            {
                list.TaskList.Remove(todoTask);
                foreach(TodoItem item in list.TaskList)
                {
                    if(item.Id > id)
                    {
                        item.Id--;
                    }
                }
            }

            return todoTask;
        }

        public TodoItem GetTaskById(int id, TodoList list)
        {
            var todoTask = id <= list.TaskList.Count ? list.TaskList[--id] : null;
            return todoTask;
        }

        public TodoItem PutTask(int id, TodoItem model, TodoList list, int listId)
        {
            var todoTask = id <= list.TaskList.Count ? model : null;
            int index = --id;

            if(todoTask != null)
            {
                todoTask.Id = id;
                todoTask.ListId = listId;
                list.TaskList[index] = todoTask;
            }

           return todoTask;
        }

        public TodoItem PatchTask(int id, TodoItem model, TodoList list)
        {
            var todoTask = id <= list.TaskList.Count ? model : null;
            int index = --id;
            
            if(todoTask != null)
            {
                if(todoTask.Title != null)
                {
                    list.TaskList[index].Title = todoTask.Title;
                }
                if(todoTask.Description != null)
                {
                    list.TaskList[index].Description = todoTask.Description;
                }
                if(todoTask.DueDate != null)
                {
                    list.TaskList[index].DueDate = todoTask.DueDate;
                }
                if(todoTask.Done != null)
                {
                    list.TaskList[index].Done = todoTask.Done;
                }
                var patchedTask = list.TaskList[index];
                return patchedTask;
            }
            return todoTask;
        }
    }
}