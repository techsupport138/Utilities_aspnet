using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.Event.Dtos;
using Utilities_aspnet.Job.Dtos;
using Utilities_aspnet.Utilities.Data;
using Utilities_aspnet.Utilities.Responses;

namespace Utilities_aspnet.Job.Data
{
    public interface IJobRepository
    {
        Task<GenericResponse<JobDto>> Get(Guid? Id);
        Task<GenericResponse<JobDto>> AddEdit(JobDto events);
        Task<GenericResponse<JobEntity>> Delete(Guid id);
    }
    public class JobRepository : BaseRepository, IJobRepository
    {
        private readonly IUploadRepository _Upload;
        public JobRepository(DbContext context, IMapper mapper, IUploadRepository Upload) : base(context, mapper)
        {
            _Upload = Upload;
        }
        public Task<GenericResponse<JobDto?>> Get(Guid? id)
        {
            if (id == null)
            {
                return Task.FromResult(new GenericResponse<JobDto?>
                    (new JobDto(), UtilitiesStatusCodes.NotFound, "NotFound"));
            }
            else
            {
                var JobEntity = _context.Set<JobEntity>()
                .Where(x => x.Id == id)
                .Select(x => new JobDto()
                {
                    Id = x.Id,
                    Description = x.Description,
                    CategoryId = x.CategoryId,
                    Title = x.Title,
                    UserId = x.UserId,
                    LanguageId = x.LanguageId,
                    TinyURL = x.TinyURL,
                    CityId = x.CityId,
                    CountryId = x.CountryId,
                    ProvinceId = x.ProvinceId,
                    Status = x.Status,
                    Enable = x.Enable,
                }).FirstOrDefault();
                if (JobEntity == null)
                {
                    return Task.FromResult(new GenericResponse<JobDto?>
                    (new JobDto(), UtilitiesStatusCodes.NotFound, "NotFound"));
                }
                return Task.FromResult(new GenericResponse<JobDto?>
                    (JobEntity, UtilitiesStatusCodes.Success, "Success"));
            }
        }


        public async Task<GenericResponse<JobDto>> AddEdit(JobDto job)
        {
            if (job.Id == null)
            {
                var model = new JobEntity()
                {
                    Id = Guid.NewGuid(),
                    Description = job.Description,
                    CategoryId = job.CategoryId,
                    Title = job.Title,
                    UserId = job.UserId,
                    LanguageId = job.LanguageId,
                    TinyURL = job.TinyURL,
                    CityId = job.CityId,
                    CountryId = job.CountryId,
                    ProvinceId = job.ProvinceId,
                    Status = job.Status,
                    Enable = job.Enable,
                };
                _context.Set<JobEntity>().Add(model);
                var up = await _Upload.UploadMedia(new UploadDto()
                {
                    UserId = job.UserId,
                    Files = job.files,
                    JobId = job.Id
                });
                return new GenericResponse<JobDto>
                    (job, UtilitiesStatusCodes.Success, "Success");
            }
            else
            {
                var old = _context.Set<JobEntity>().FirstOrDefault(x => x.Id == job.Id);
                if (old == null)
                    return new GenericResponse<JobDto>
                    (null, UtilitiesStatusCodes.NotFound, "NotFound");
                old.Id = Guid.NewGuid();
                old.Description = job.Description;
                old.CategoryId = job.CategoryId;
                old.Title = job.Title;
                old.UserId = job.UserId;
                old.LanguageId = job.LanguageId;
                old.TinyURL = job.TinyURL;
                old.CityId = job.CityId;
                old.CountryId = job.CountryId;
                old.ProvinceId = job.ProvinceId;
                old.Status = job.Status;
                old.Enable = job.Enable;
                old.UpdatedAt = DateTime.Now;
                _context.Set<JobEntity>().Update(old);
                var up = await _Upload.UploadMedia(new UploadDto()
                {
                    UserId = job.UserId,
                    Files = job.files,
                    JobId = job.Id
                });
                return new GenericResponse<JobDto>
                    (job, UtilitiesStatusCodes.Success, "Success");
            }
        }

        public async Task<GenericResponse<JobEntity>> Delete(Guid id)
        {
            var events = _context.Set<JobEntity>()
                .Include(x => x.MediaList)
                .Where(x => x.Id == id).First();
            if (events.MediaList != null)
                foreach (var item in events.MediaList)
                {
                    _Upload.DeleteMedia(item.Id);
                }
            _context.Set<JobEntity>().Remove(events);
            _context.SaveChanges();
            return new GenericResponse<JobEntity>
                    (events, UtilitiesStatusCodes.Success, "Success");
        }
    }
}
