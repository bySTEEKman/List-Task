using System;
using System.Linq;
using System.Collections.Generic;
using todo_rest_api.Models;
using Npgsql;

namespace todo_rest_api
{
    public class DashboardService
    {
        private TodoListContext _context;

        public DashboardService(TodoListContext context)
        {
            this._context = context;
        }

        public Dashboard GetTasksForToday()
        {
            var connString = "Host=127.0.0.1;Username=todo_app;Password=secret;Database=todolist_http";

            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            Dashboard dashForToday = new Dashboard();

            dashForToday.TasksCountForToday = GetTodayTasksCount(conn);

            dashForToday.NotComplitedTasks = GetFalseTaskLists(conn);

            return dashForToday;
        }

        private List<TodoListDTO> GetFalseTaskLists(NpgsqlConnection conn)
        {
            List<TodoListDTO> fullList = new List<TodoListDTO>();

            using (var cmd = new NpgsqlCommand("SELECT lists.id, lists.title, COUNT(tasks.list_id) FROM lists LEFT JOIN tasks ON lists.id = tasks.list_id WHERE due_date = CURRENT_DATE AND done = false OR lists.id = tasks.list_id ISNULL GROUP BY (lists.id)", conn))
            {
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        TodoListDTO listInfo = new TodoListDTO();
                        listInfo.Id = reader.GetInt32(0);
                        listInfo.Title = reader.GetString(1);
                        listInfo.NotComplitedTasksCount = reader.GetInt32(2);
                        fullList.Add(listInfo);
                    }
            }

            return fullList;
        }
        private int GetTodayTasksCount(NpgsqlConnection conn)
        {
            int tasksCount = _context.Tasks
                .Where(t => t.DueDate == DateTime.Today)
                .ToList()
                .Count;

            return tasksCount;
        }
    }
}