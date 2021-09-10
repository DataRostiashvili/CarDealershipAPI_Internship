using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Application.Mappings
{
    public class ApiToDtoModelMappingProfile : Profile
    {
        public ApiToDtoModelMappingProfile() 
        {
            CreateMap<Domain.APIModels.CarRequestResponse, Domain.DTOs.CarDto>();

            CreateMap<Domain.APIModels.ClientRequestResponse, Domain.DTOs.ClientDto>();
        } 
    }
}
