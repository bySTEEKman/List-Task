using System;
using System.Collections.Generic;

namespace todo_rest_api.Models
{
    public class TodoListDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int NotComplitedTasksCount { get; set; }
    }
}