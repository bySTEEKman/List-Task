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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Console.WriteLine)
                .UseSnakeCaseNamingConvention()
                .UseNpgsql("Host=127.0.0.1;Username=todo_app;Password=secret;Database=todolist_http");
        }
    }
}