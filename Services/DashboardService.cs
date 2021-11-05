using System;
using System.Linq;
using System.Collections.Generic;
using todo_rest_api.Models;

namespace todo_rest_api
{
    public class DashboardService
    {
        private TodoListContext _context;

        public DashboardService(TodoListContext context)
        {
            this._context = context;
        }

        DateTime today = DateTime.Today;

        public Dashboard GetTasksForToday()
        {
            Dashboard dashForToday = new Dashboard();

            dashForToday.TasksCountForToday = GetTodayTasksCount();

            dashForToday.NotComplitedTasks = GetFalseTaskLists();

            return dashForToday;
        }

        private List<TodoListDTO> GetFalseTaskLists()
        {
            List<TodoListDTO> falseList = new List<TodoListDTO>();

            List<TodoList> fullList = _context.Lists.ToList();

            foreach(TodoList list in fullList)
            {
                TodoListDTO dto = new TodoListDTO();

                dto.Id = list.Id;
                dto.Title= list.Title;
                dto.NotComplitedTasksCount = GetTodayTasksCountForList(list);

                falseList.Add(dto);
            }

            return falseList;
        }

        private int GetTodayTasksCountForList(TodoList list)
        {
            var taskCount = _context.Tasks
                .Where(t => t.ListId == list.Id && !t.Done)
                .ToList()
                .Count;

            return taskCount;
        }
        private int GetTodayTasksCount()
        {
            int tasksCount = _context.Tasks
                .Where(t => t.DueDate == today)
                .ToList()
                .Count;
            
            return tasksCount;
        }
    }
}