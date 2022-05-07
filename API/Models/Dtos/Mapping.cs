using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace API.Models.Dtos
{
    public class Mapping : Profile
    {
        public Mapping() 
        {
            CreateMap<Employee,EmployeeDto>()
                .ForMember(d => d.EmployeeType , o => o.MapFrom(s => s.EmployeeType.Name) );
        }
    }
}