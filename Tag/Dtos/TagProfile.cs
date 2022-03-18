using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.Tag.Entities;

namespace Utilities_aspnet.Tag.Dtos
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<TagEntity, CreateTagDto>().ReverseMap();
            CreateMap<TagEntity, UpdateTagDto>().ReverseMap();
            CreateMap<TagEntity, GetTagDto>().ReverseMap();
        }
    }
}
