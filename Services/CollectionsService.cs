using System;
using System.Linq;
using System.Collections.Generic;
using todo_rest_api.Models;
using Microsoft.EntityFrameworkCore;

namespace todo_rest_api
{
    public class CollectionsService
    {
        private TodoListContext _context;

        public CollectionsService(TodoListContext context)
        {
            this._context = context;
        }

        DateTime today = DateTime.Today;

        public List<TodoItemDTO> GetTaskCollectionForToday()
        {
            List<TodoItemDTO> answer = new List<TodoItemDTO>();

            List<TodoItem> list = _context.Tasks
                .Where(t => t.DueDate == today)
                .Include(t => t.TodoList)
                .ToList();

            foreach(TodoItem item in list)
            {
                TodoItemDTO dto = new TodoItemDTO();
                ListDTO listDTO = new ListDTO();

                listDTO.Id = item.ListId;
                listDTO.OwnerName = item.TodoList.Title;

                dto.ListDTO = listDTO;
                dto.Id = item.Id;
                dto.Title = item.Title;
                dto.Description = item.Description;
                dto.Done = item.Done;
                dto.DueDate = item.DueDate;

                answer.Add(dto);
            }

            return answer;
        }
    }
}