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
    public interface ICategoryRepository : IBaseRepository
    {
        List<KVPCategoryVM> Get(CategoryFilter filter);
        Task<CategoryEntity> Get(Guid Id);
        Task<GenericResponse> NewCategory(NewCategoryDto newCategory);
        Task<GenericResponse> UpdateCategory(NewCategoryDto newCategory);
        Task<GenericResponse> DeleteCategory(Guid id);
    }
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {

        private readonly IUploadRepository _UploadRepository;

        public CategoryRepository(DbContext context, IMapper mapper, IUploadRepository uploadRepository)
            : base(context, mapper)
        {
            _UploadRepository = uploadRepository;
        }

        public List<KVPCategoryVM> Get(CategoryFilter filter)
        {
            List<KVPCategoryVM> content = _context.Set<CategoryEntity>()
                 .Where(x => x.LanguageId == filter.Language && x.CategoryFor == filter.CategoryFor)
                 .Include(x => x.Media).Include(x => x.Parent)
                 .Where(x => (!filter.OnlyParent) || (x.ParentId == null && filter.OnlyParent))
                 .Select(w => new KVPCategoryVM()
                 {
                     Key = w.CategoryId,
                     Image = w.Media,
                     Value = w.Title,
                     CategoryFor = w.CategoryFor,
                     LanguageId = w.LanguageId,
                     ParentId = w.ParentId,
                     Childs = w.InverseParent.Select(x => new KVPCategoryVM()
                     {
                         Key = x.CategoryId,
                         Image = x.Media,
                         Value = x.Title,
                         CategoryFor = x.CategoryFor,
                         LanguageId = x.LanguageId,
                         ParentId = x.ParentId,
                         ParentTitle = x.Parent.Title
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
            return new GenericResponse(UtilitiesStatusCodes.Success, $"Category {cat.Title} delete Success", id: cat.CategoryId);
        }

        public async Task<CategoryEntity> Get(Guid Id)
        {
            var cat = await _context.Set<CategoryEntity>()
                .Include(x => x.Media)
                .Include(x => x.InverseParent)
                .Include(x => x.Parent)
                .FirstOrDefaultAsync(x => x.CategoryId == Id);
            return cat;
        }

        public async Task<GenericResponse> UpdateCategory(NewCategoryDto category)
        {
            var cat = _context.Set<CategoryEntity>()
                .Where(x => x.CategoryId == category.CategoryId).First();
            if (category.File != null)
            {
                List<IFormFile> f = new List<IFormFile>() { category.File };
                var res = await _UploadRepository.UploadMedia(new UploadDto()
                {
                    Files = f,
                    UserId = null,
                });
                cat.MediaId = res.Id;
            }


            cat.Title = category.Title;
            cat.ParentId = category.ParentId;
            cat.LanguageId = category.LanguageId;
            _context.Set<CategoryEntity>().Update(cat);
            await _context.SaveChangesAsync();
            return new GenericResponse(UtilitiesStatusCodes.Success, $"Category {cat.Title} update Success", id: cat.CategoryId);
        }
    }
}
