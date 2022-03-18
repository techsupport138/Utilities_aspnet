using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using Utilities_aspnet.Tag.Dtos;
using Utilities_aspnet.Tag.Entities;
using Utilities_aspnet.Utilities.Responses;

namespace Utilities_aspnet.Tag.Data
{
    public interface ITagRepository
    {
        Task<ApiResponse<GetTagDto>> Create(CreateTagDto dto);
        Task<ApiResponse<IEnumerable<GetTagDto>>> Get();
        Task<ApiResponse<GetTagDto>> GetById(int id);
        Task<ApiResponse<GetTagDto>> Update(UpdateTagDto dto);
        Task<ApiResponse> HardDelete(int id);
        Task<ApiResponse> SoftDelete(int id);
    }

    public class TagRepository : ITagRepository
    {
        private readonly DbContext _context;
        private readonly IMapper _mapper;

        public TagRepository(DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        async Task<ApiResponse<GetTagDto>> ITagRepository.Create(CreateTagDto dto)
        {
            TagEntity entity = _mapper.Map<TagEntity>(dto);

            EntityEntry<TagEntity>? i = await _context.AddAsync(entity);
            return new ApiResponse<GetTagDto>(_mapper.Map<GetTagDto>(i.Entity));
        }


        Task<ApiResponse<IEnumerable<GetTagDto>>> ITagRepository.Get()
        {
            throw new NotImplementedException();
        }

        Task<ApiResponse<GetTagDto>> ITagRepository.GetById(int id)
        {
            throw new NotImplementedException();
        }

        Task<ApiResponse> ITagRepository.HardDelete(int id)
        {
            throw new NotImplementedException();
        }

        Task<ApiResponse> ITagRepository.SoftDelete(int id)
        {
            throw new NotImplementedException();
        }

        Task<ApiResponse<GetTagDto>> ITagRepository.Update(UpdateTagDto dto)
        {
            throw new NotImplementedException();
        }
    }

}
