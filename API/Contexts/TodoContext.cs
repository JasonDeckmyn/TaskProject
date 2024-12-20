using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnet.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskProject.Contexts
{
    public class TodoContext : DbContext
    {

        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
            
        }

        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>()
                .Property(t => t.IsComplete)
                .HasDefaultValue(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}