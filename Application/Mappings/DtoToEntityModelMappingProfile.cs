using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Application.Mappings
{
    public class DtoToEntityModelMappingProfile :  Profile
    {
        public DtoToEntityModelMappingProfile() 
        {
            CreateMap<Domain.DTOs.Client, Domain.Entity.Client>();
            CreateMap<Domain.DTOs.Car, Domain.Entity.Car>();
        }
    }
}
