using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Application.Mappings
{
    public class EntityToDtoModelMappingProfile : Profile
    {
        public EntityToDtoModelMappingProfile()
        {
            CreateMap<Domain.Entity.Client, Domain.DTOs.Client>();
            CreateMap<Domain.Entity.Car, Domain.DTOs.Car>();
        }
    }
}
