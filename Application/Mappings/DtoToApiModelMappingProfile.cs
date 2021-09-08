using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Application.Mappings
{
    public class DtoToApiModelMappingProfile : Profile
    {
        public DtoToApiModelMappingProfile()
        {
            CreateMap<Domain.DTOs.Client, Domain.APIModels.Client>();
            CreateMap<Domain.DTOs.Car, Domain.APIModels.Car>();
        }
    }
}
