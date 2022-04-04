using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.Product.Dto;
using Utilities_aspnet.Product.Entities;
using Utilities_aspnet.Utilities.Responses;

namespace Utilities_aspnet.Product.Data
{
    public interface IPostRepository
    {
        Task<GenericResponse<IEnumerable<PostDto?>>> GetAllPosts();
        Task<GenericResponse<PostDto?>> GetPostById(Guid id);
        Task<GenericResponse<PostDto?>> AddPost(PostDto post);
        Task<GenericResponse<PostDto?>> UpdatePost(PostDto post);
        Task<GenericResponse<PostDto?>> DeletePost(Guid id);

        
    }
    public class PostRepository : IPostRepository
    {
        private readonly DbContext _context;
        private readonly IMapper _mapper;

        public PostRepository(DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<GenericResponse<PostDto?>> AddPost(PostDto post)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<PostDto?>> DeletePost(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<GenericResponse<IEnumerable<PostDto?>>> GetAllPosts()
        {
            List<PostEntity> Content = await _context.Set<PostEntity>()
               .Where(c => c.DeletedAt == null)
               .ToListAsync();
            List<PostDto> postDtos = _mapper.Map<List<PostDto>>(Content);

            return new GenericResponse<IEnumerable<PostDto?>>(postDtos, Utilities.Enums.UtilitiesStatusCodes.Success,
            message: "Success");

        }

        public async Task<GenericResponse<PostDto?>> GetPostById(Guid id)
        {
            PostEntity? Content = await _context.Set<PostEntity>()
                .FirstOrDefaultAsync(c => c.Id == id);

            PostDto postDtos = _mapper.Map<PostDto>(Content);

            return new GenericResponse<PostDto?>(postDtos, Utilities.Enums.UtilitiesStatusCodes.Success,
            message: "Success");
        }

        public Task<GenericResponse<PostDto?>> UpdatePost(PostDto post)
        {
            throw new NotImplementedException();
        }
    }
}
