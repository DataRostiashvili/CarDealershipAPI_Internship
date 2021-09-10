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
            CreateMap<Domain.DTOs.ClientDto, Domain.APIModels.ClientRequestResponse>();
            CreateMap<Domain.DTOs.CarDto, Domain.APIModels.CarRequestResponse>();
        }
    }
}
