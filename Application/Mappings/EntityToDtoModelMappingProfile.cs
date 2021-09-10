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
            CreateMap<Domain.Entity.ClientEntity, Domain.DTOs.ClientDto>();
            CreateMap<Domain.Entity.CarEntity, Domain.DTOs.CarDto>();
        }
    }
}
