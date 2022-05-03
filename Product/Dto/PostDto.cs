using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.Product.Entities;

namespace Utilities_aspnet.Product.Dto {
    public class PostDto {
        public Guid Id { get; } = Guid.NewGuid();
        public string? Title { get; set; }
        public string? SubTitle { get; set; }
        public string? Description { get; set; }
        public string? Tags { get; set; }
        public bool IsSell { get; set; }
        public decimal Price { get; set; } = 0;
    }

    public class AutoMapperPost : Profile {
        public AutoMapperPost() {
            CreateMap<ProductEntity, PostDto>().ReverseMap();
        }
    }
}