using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnet.Models;

namespace TaskProject.dto
{
    public class TodoWriteDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Urgency { get; set; }
        public UrgencyLevel UrgencyLevel
        {
            get
            {
                return (UrgencyLevel)Enum.Parse(typeof(UrgencyLevel), Urgency);
            }
        }
    }
}