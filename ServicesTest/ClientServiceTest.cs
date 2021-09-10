using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using TestsShared;
using AutoMapper;


namespace ServicesTest
{
    public class ClientServiceTest
    {
        readonly IMapper _mapper = RealMapperFactory.Create();
    }
}
