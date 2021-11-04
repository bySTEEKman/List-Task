using System;
using System.Collections.Generic;
namespace todo_rest_api.Models
{
    public class TodoList
    {
        public int Id { get; set; }
        public string OwnerName { get; set; }
        public List<TodoItem> TaskList { get; set; }
    }
}