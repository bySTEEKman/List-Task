using System.Collections.Generic;

namespace todo_rest_api.Models
{
    public class Dashboard
    {
        public int TasksCountForToday { get; set; }
        public List<TodoListDTO> NotCompletedTasks { get; set; }
    }
}