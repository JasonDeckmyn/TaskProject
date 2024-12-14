using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskViewer.Models
{
    public class TodoModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public UrgencyLevel Urgency { get; set; }
    }

    public enum UrgencyLevel
    {
        Low,
        Medium,
        High
    }
}
