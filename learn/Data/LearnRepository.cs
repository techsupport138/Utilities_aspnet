using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.Event.Dtos;
using Utilities_aspnet.Learn.Dtos;
using Utilities_aspnet.Utilities.Data;
using Utilities_aspnet.Utilities.Responses;

namespace Utilities_aspnet.Learn.Data
{
    public interface ILearnRepository
    {
        Task<GenericResponse<LearnDto>> Get(Guid? Id);
        Task<GenericResponse<LearnDto>> AddEdit(LearnDto events);
        Task<GenericResponse<LearnEntity>> Delete(Guid id);
    }
    public class LearnRepository : BaseRepository, ILearnRepository
    {
        private readonly IUploadRepository _Upload;
        public LearnRepository(DbContext context, IMapper mapper, IUploadRepository Upload) : base(context, mapper)
        {
            _Upload = Upload;
        }
        public Task<GenericResponse<LearnDto?>> Get(Guid? id)
        {
            if (id == null)
            {
                return Task.FromResult(new GenericResponse<LearnDto?>
                    (new LearnDto(), UtilitiesStatusCodes.NotFound, "NotFound"));
            }
            else
            {
                var LearnEntity = _context.Set<LearnEntity>()
                .Where(x => x.Id == id)
                .Select(x => new LearnDto()
                {
                    Id = x.Id,
                    CategoryId = x.CategoryId,
                    Title = x.Title,
                    UserId = x.UserId,
                    LanguageId = x.LanguageId,
                    TinyURL = x.TinyURL,
                    Status = x.Status,
                    Enable = x.Enable,
                    Amount = x.Amount,
                    Confirmations = x.Confirmations,
                    Honors = x.Honors,
                    Publisher = x.Publisher,
                }).FirstOrDefault();
                if (LearnEntity == null)
                {
                    return Task.FromResult(new GenericResponse<LearnDto?>
                    (new LearnDto(), UtilitiesStatusCodes.NotFound, "NotFound"));
                }
                return Task.FromResult(new GenericResponse<LearnDto?>
                    (LearnEntity, UtilitiesStatusCodes.Success, "Success"));
            }
        }


        public async Task<GenericResponse<LearnDto>> AddEdit(LearnDto Learn)
        {
            if (Learn.Id == null)
            {
                var model = new LearnEntity()
                {
                    Id = Guid.NewGuid(),
                    CategoryId = Learn.CategoryId,
                    Title = Learn.Title,
                    UserId = Learn.UserId,
                    LanguageId = Learn.LanguageId,
                    TinyURL = Learn.TinyURL,
                    Status = Learn.Status,
                    Enable = Learn.Enable,
                    Amount = Learn.Amount,
                    Confirmations = Learn.Confirmations,
                    Honors = Learn.Honors,
                    Publisher = Learn.Publisher,
                };
                _context.Set<LearnEntity>().Add(model);
                var up = await _Upload.UploadMedia(new UploadDto()
                {
                    UserId = Learn.UserId,
                    Files = Learn.files,
                    LearnId = Learn.Id
                });
                return new GenericResponse<LearnDto>
                    (Learn, UtilitiesStatusCodes.Success, "Success");
            }
            else
            {
                var old = _context.Set<LearnEntity>().FirstOrDefault(x => x.Id == Learn.Id);
                if (old == null)
                    return new GenericResponse<LearnDto>
                    (null, UtilitiesStatusCodes.NotFound, "NotFound");
                old.Id = Guid.NewGuid();
                old.CategoryId = Learn.CategoryId;
                old.Title = Learn.Title;
                old.UserId = Learn.UserId;
                old.LanguageId = Learn.LanguageId;
                old.TinyURL = Learn.TinyURL;
                old.Status = Learn.Status;
                old.Enable = Learn.Enable;
                old.Amount = Learn.Amount;
                old.Confirmations = Learn.Confirmations;
                old.Honors = Learn.Honors;
                old.Publisher = Learn.Publisher;
                old.UpdatedAt = DateTime.Now;
                _context.Set<LearnEntity>().Update(old);
                var up = await _Upload.UploadMedia(new UploadDto()
                {
                    UserId = Learn.UserId,
                    Files = Learn.files,
                    LearnId = Learn.Id
                });
                return new GenericResponse<LearnDto>
                    (Learn, UtilitiesStatusCodes.Success, "Success");
            }
        }

        public async Task<GenericResponse<LearnEntity>> Delete(Guid id)
        {
            var events = _context.Set<LearnEntity>()
                .Include(x => x.MediaList)
                .Where(x => x.Id == id).First();
            if (events.MediaList != null)
                foreach (var item in events.MediaList)
                {
                    _Upload.DeleteMedia(item.Id);
                }
            _context.Set<LearnEntity>().Remove(events);
            _context.SaveChanges();
            return new GenericResponse<LearnEntity>
                    (events, UtilitiesStatusCodes.Success, "Success");
        }
    }
}
