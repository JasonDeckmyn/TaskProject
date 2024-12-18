using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnet.Models;

namespace TaskProject.dto
{
    public class TodoReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public UrgencyLevel Urgency { get; set; }
        public bool IsComplete { get; set; }
    }
}