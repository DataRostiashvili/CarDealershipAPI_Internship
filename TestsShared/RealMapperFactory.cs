using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Application.Mappings;


namespace TestsShared
{
    public static  class RealMapperFactory
    {
        public static IMapper Create()
        {
            var realMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(new Profile[] { new ApiToDtoModelMappingProfile(),
                    new DtoToApiModelMappingProfile(), new DtoToEntityModelMappingProfile(), new EntityToDtoModelMappingProfile() });
            });
            var mapper = realMapper.CreateMapper();

            return mapper;
        }
    }
}
