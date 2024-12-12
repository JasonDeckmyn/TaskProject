using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnet.Models;
using AutoMapper;
using TaskProject.dto;

namespace TaskProject.Mappings
{
    public class TodoProfile:Profile
    {
        public TodoProfile()
        {
            CreateMap<Todo, TodoReadDto>();
        }
    }
}