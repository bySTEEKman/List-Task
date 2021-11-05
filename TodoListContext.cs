using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using todo_rest_api.Models;

namespace todo_rest_api
{
    public class TodoListContext : DbContext
    {
        public DbSet<TodoList> Lists { get; set; }
        public DbSet<TodoItem> Tasks { get; set; }

        public TodoListContext(DbContextOptions options): base(options){

        }
    }
}