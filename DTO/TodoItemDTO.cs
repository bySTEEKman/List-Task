using System;
using NpgsqlTypes;

namespace todo_rest_api.Models
{
    public class TodoItemDTO
    {
        public ListDTO List { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime? dueDate { get; set; }
        public bool? done { get; set; }

        public TodoItemDTO(TodoItem item)
        {
            this.List = new ListDTO(item.TodoList);
            this.id = item.Id;
            this.title = item.Title;
            this.description = item.Description;
            this.dueDate = item.DueDate;
            this.done = item.Done;
        }
    }
}