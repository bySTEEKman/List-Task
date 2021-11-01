using System;
namespace todo_rest_api.Models
{
    public class TodoList
    {
        public int? Id { get; set; }
        public string OvnerName { get; set; }
        public List<Task>? TaskList { get; set; }
    }
}