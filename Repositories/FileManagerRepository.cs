using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.Entities;

namespace Utilities_aspnet.Repositories
{
    public interface IFileManagerRepository
    {
        Task<GenericResponse<ResponseSaveFileDto>> Create(RequestSaveFileDto dto, CancellationToken ct);
        Task<GenericResponse<ResponseSaveFileDto>> SaveFile(RequestSaveFileDto dto);

    }
    public class FileManagerRepository : IFileManagerRepository
    {
        private readonly DbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileManagerRepository(DbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<GenericResponse<ResponseSaveFileDto>> Create(RequestSaveFileDto dto, CancellationToken ct)
        {
            var post = new FileManagerEntity
            {
                CreatedBy = dto.CreatedBy,
                Description = dto.Description,
                ImagePath = dto.ImagePath,
                CreatedAt = DateTime.Now,
            };
            await _dbContext.Set<FileManagerEntity>().AddAsync(post);
            var saveResponse = await _dbContext.SaveChangesAsync();
            if (saveResponse < 0) return new GenericResponse<ResponseSaveFileDto>(new ResponseSaveFileDto { Error = "File can't save in database", Success = false });
            return new GenericResponse<ResponseSaveFileDto>(new ResponseSaveFileDto { Success = true , FileId = post.Id});
        }

        public async Task<GenericResponse<ResponseSaveFileDto>> SaveFile(RequestSaveFileDto dto)
        {

            if (dto.File is null) return new GenericResponse<ResponseSaveFileDto>(new ResponseSaveFileDto { Success = false, Error = "File can't be null or empty" });

            var uniqueFileName = Utils.GetUniqueFileName(dto.File.FileName);

            var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "FileManager", dto.Procedure.ToString());

            var filePath = Path.Combine(uploads, uniqueFileName);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            await dto.File.CopyToAsync(new FileStream(filePath, FileMode.Create));

            var successfulySave = File.Exists(filePath);
            if (!successfulySave) return new GenericResponse<ResponseSaveFileDto>(new ResponseSaveFileDto { Error = "file doesn't saved", Success = false });
            return new GenericResponse<ResponseSaveFileDto>(new ResponseSaveFileDto { Success = true }); ;
        }
    }
}
