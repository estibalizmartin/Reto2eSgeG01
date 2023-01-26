﻿using AutoMapper;
using Reto2eSgeG01.Core.Entities;
using Reto2eSgeG01.Core.Models;

namespace Reto2eSgeG01.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        { 
            CreateMap<Employee, EmployeeViewModel>();
        }
    }
}
