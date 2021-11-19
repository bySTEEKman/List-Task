using System;
using System.Collections.Generic;

namespace todo_rest_api.Models
{
    public class ListDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ListDTO(TodoList list)
        {
            this.Id = list.Id;
            this.Title = list.Title;
        }
    }
}