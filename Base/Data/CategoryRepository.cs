using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.Base.Dtos;
using Utilities_aspnet.Utilities.Data;
using Utilities_aspnet.Utilities.Dtos;
using Utilities_aspnet.Utilities.Enums;
using Utilities_aspnet.Utilities.Responses;

namespace Utilities_aspnet.Base.Data
{
    public interface ICategoryRepository
    {
        List<KVPVM> Get(CategoryFilter filter);
        Task<GenericResponse> NewCategory(NewCategoryDto newCategory);
        Task<GenericResponse> DeleteCategory(Guid id);
    }
    public class CategoryRepository : ICategoryRepository
    {

        private readonly DbContext _context;
        private readonly IMapper _mapper;
        private readonly IUploadRepository _UploadRepository;

        public CategoryRepository(DbContext context, IMapper mapper, IUploadRepository uploadRepository)
        {
            _context = context;
            _mapper = mapper;
            _UploadRepository = uploadRepository;
        }

        public List<KVPVM> Get(CategoryFilter filter)
        {
            List<KVPVM> content = _context.Set<CategoryEntity>()
                 .Where(x => x.LanguageId == filter.Language && x.CategoryFor == filter.CategoryFor)
                 .Include(x => x.InverseParent).Include(x => x.Media)
                 .Where(x => x.ParentId == null)
                 .Select(w => new KVPVM()
                 {
                     Key = w.CategoryId,
                     Image = w.Media,
                     Value = w.Title,
                     Childs = w.InverseParent.Select(x => new KVPVM()
                     {
                         Key = x.CategoryId,
                         Image = x.Media,
                         Value = x.Title
                     }).ToList()
                 }).ToList();
            return content;
        }

        public async Task<GenericResponse> NewCategory(NewCategoryDto newCategory)
        {
            List<IFormFile> f = new List<IFormFile>() { newCategory.File };
            var res = await _UploadRepository.UploadMedia(new UploadDto()
            {
                Files = f,
                UserId = null,
            });
            var Cat = new CategoryEntity()
            {
                CategoryFor = newCategory.CategoryFor,
                LanguageId = newCategory.LanguageId,
                ParentId = newCategory.ParentId,
                MediaId = res.Ids[0],
                Title = newCategory.Title,
            };
            await _context.Set<CategoryEntity>().AddAsync(Cat);
            await _context.SaveChangesAsync();
            return new GenericResponse(UtilitiesStatusCodes.Success, $"Cat {Cat.Title} Created!", id: Cat.CategoryId);
        }

        public async Task<GenericResponse> DeleteCategory(Guid id)
        {
            var cat = _context.Set<CategoryEntity>()
                .Include(x => x.InverseParent)
                .Where(x => x.CategoryId == id).First();
            if (cat.MediaId != null)
                await _UploadRepository.DeleteMedia(cat.MediaId.Value);
            if (cat.InverseParent.Count != 0)
            {
                return new GenericResponse(UtilitiesStatusCodes.Unhandled, "Has Any Child");
            }
            _context.Set<CategoryEntity>().Remove(cat);
            await _context.SaveChangesAsync();
            return new GenericResponse(UtilitiesStatusCodes.Success, $"Category {cat.Title} delete Success", id = cat.id);
        }
    }
}
