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
        Task<GenericResponse<GetTagDto>> Create(CreateTagDto dto);
        Task<GenericResponse<IEnumerable<GetTagDto>>> Get();
        Task<GenericResponse<GetTagDto>> GetById(int id);
        Task<GenericResponse<GetTagDto>> Update(UpdateTagDto dto);
        Task<GenericResponse> HardDelete(int id);
        Task<GenericResponse> SoftDelete(int id);
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

        async Task<GenericResponse<GetTagDto>> ITagRepository.Create(CreateTagDto dto)
        {
            TagEntity entity = _mapper.Map<TagEntity>(dto);

            EntityEntry<TagEntity>? i = await _context.AddAsync(entity);
            return new GenericResponse<GetTagDto>(_mapper.Map<GetTagDto>(i.Entity));
        }


        Task<GenericResponse<IEnumerable<GetTagDto>>> ITagRepository.Get()
        {
            throw new NotImplementedException();
        }

        Task<GenericResponse<GetTagDto>> ITagRepository.GetById(int id)
        {
            throw new NotImplementedException();
        }

        Task<GenericResponse> ITagRepository.HardDelete(int id)
        {
            throw new NotImplementedException();
        }

        Task<GenericResponse> ITagRepository.SoftDelete(int id)
        {
            throw new NotImplementedException();
        }

        Task<GenericResponse<GetTagDto>> ITagRepository.Update(UpdateTagDto dto)
        {
            throw new NotImplementedException();
        }
    }

}
