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

        Dashboard dashboard = new Dashboard();
        DateTime today = DateTime.Today;

        public void GetTasksForToday()
        {
            foreach (TodoItem item in _context.Tasks)
            {
                if(item.DueDate == today)
                {
                    dashboard.TasksCountForToday += 1;
                }
            }
        }
    }
}