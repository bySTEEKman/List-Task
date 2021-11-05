using System;
using NpgsqlTypes;

namespace todo_rest_api.Models
{
    public class TodoItemDTO
    {
        public ListDTO ListDTO { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool Done { get; set; }
    }
}