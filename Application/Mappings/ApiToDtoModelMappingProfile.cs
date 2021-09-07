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
            CreateMap<Domain.APIModels.Car, Domain.DTOs.Car>();

            CreateMap<Domain.APIModels.Client, Domain.DTOs.Client>();
        } 
    }
}
