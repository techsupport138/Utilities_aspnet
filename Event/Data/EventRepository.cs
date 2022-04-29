using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.Event.Dtos;
using Utilities_aspnet.Utilities.Data;
using Utilities_aspnet.Utilities.Responses;

namespace Utilities_aspnet.Event.Data
{
    public interface IEventRepository
    {
        Task<GenericResponse<EventDto>> Get(Guid? Id);
        Task<GenericResponse<EventDto>> AddEdit(EventDto events);
        Task<GenericResponse<EventEntity>> Delete(Guid id);
    }
    public class EventRepository : BaseRepository, IEventRepository
    {
        private readonly IUploadRepository _Upload;
        public EventRepository(DbContext context, IMapper mapper, IUploadRepository Upload) : base(context, mapper)
        {
            _Upload = Upload;
        }
        public Task<GenericResponse<EventDto?>> Get(Guid? id)
        {
            if (id == null)
            {
                return Task.FromResult(new GenericResponse<EventDto?>
                    (new EventDto(), UtilitiesStatusCodes.NotFound, "NotFound"));
            }
            else
            {
                var eventEntity = _context.Set<EventEntity>()
                .Where(x => x.Id == id)
                .Select(x => new EventDto()
                {
                    Id = x.Id,
                    Body = x.Body,
                    CategoryId = x.CategoryId,
                    Lid = x.Lid,
                    NumberOfLikes = x.NumberOfLikes,
                    Status = x.Status,
                    Title = x.Title,
                    UserId = x.UserId,
                    Author = x.Author,
                    EventDate = x.EventDate,
                    EventLocation = x.EventLocation,
                    LanguageId = x.LanguageId,
                    TinyURL = x.TinyURL,
                    UseCase = x.UseCase,
                    CityId=x.CityId,
                    CountryId=x.CountryId,
                    ProvinceId=x.ProvinceId,
                }).FirstOrDefault();
                if (eventEntity == null)
                {
                    return Task.FromResult(new GenericResponse<EventDto?>
                    (new EventDto(), UtilitiesStatusCodes.NotFound, "NotFound"));
                }
                return Task.FromResult(new GenericResponse<EventDto?>
                    (eventEntity, UtilitiesStatusCodes.Success, "Success"));
            }
        }


        public async Task<GenericResponse<EventDto>> AddEdit(EventDto events)
        {
            if (events.Id == null)
            {
                var model = new EventEntity()
                {
                    Id = Guid.NewGuid(),
                    Body = events.Body,
                    CategoryId = events.CategoryId,
                    Lid = events.Lid,
                    NumberOfLikes = events.NumberOfLikes ?? 0,
                    Status = events.Status,
                    Title = events.Title,
                    UserId = events.UserId,
                    Author = events.Author,
                    EventDate = events.EventDate,
                    EventLocation = events.EventLocation,
                    LanguageId = events.LanguageId,
                    TinyURL = events.TinyURL,
                    UseCase = events.UseCase,
                    CityId = events.CityId,
                    CountryId = events.CountryId,
                    ProvinceId = events.ProvinceId,
                };
                _context.Set<EventEntity>().Add(model);
                //var up = await _Upload.UploadMedia(new UploadDto()
                //{
                //    UserId = events.UserId,
                //    Files = events.files,
                //    eventsId = events.Id
                //});
                return new GenericResponse<EventDto>
                    (events, UtilitiesStatusCodes.Success, "Success");
            }
            else
            {
                var old = _context.Set<EventEntity>().FirstOrDefault(x => x.Id == events.Id);
                if (old == null)
                    return new GenericResponse<EventDto>
                    (null, UtilitiesStatusCodes.NotFound, "NotFound");
                old.Body = events.Body;
                old.CategoryId = events.CategoryId;
                old.Lid = events.Lid;
                old.NumberOfLikes = events.NumberOfLikes ?? 0;
                old.Status = events.Status;
                old.Title = events.Title;
                old.UserId = events.UserId;
                old.Author = events.Author;
                old.EventDate = events.EventDate;
                old.EventLocation = events.EventLocation;
                old.LanguageId = events.LanguageId;
                old.TinyURL = events.TinyURL;
                old.UseCase = events.UseCase;
                old.UpdatedAt = DateTime.Now;
                old.CityId = events.CityId;
                old.CountryId = events.CountryId;
                old.ProvinceId = events.ProvinceId;
                _context.Set<EventEntity>().Update(old);
                //var up = await _Upload.UploadMedia(new UploadDto()
                //{
                //    UserId = events.UserId,
                //    Files = events.files,
                //    eventsId = events.Id
                //});
                return new GenericResponse<EventDto>
                    (events, UtilitiesStatusCodes.Success, "Success");
            }
        }

        public async Task<GenericResponse<EventEntity>> Delete(Guid id)
        {
            var events = _context.Set<EventEntity>()
                .Include(x => x.MediaList)
                .Where(x => x.Id == id).First();
            if (events.MediaList != null)
                foreach (var item in events.MediaList)
                {
                    _Upload.DeleteMedia(item.Id);
                }
            _context.Set<EventEntity>().Remove(events);
            _context.SaveChanges();
            return new GenericResponse<EventEntity>
                    (events, UtilitiesStatusCodes.Success, "Success");
        }
    }
}
