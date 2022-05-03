using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.Ads.Dtos;
using Utilities_aspnet.Utilities.Data;
using Utilities_aspnet.Utilities.Responses;

namespace Utilities_aspnet.Ads.Data
{
    public interface IAdsRepository
    {
        Task<GenericResponse<AdsDto>> Get(Guid? Id);
        Task<GenericResponse<AdsDto>> AddEdit(AdsDto ads);
        Task<GenericResponse<AdsEntity>> Delete(Guid id);
    }
    public class AdsRepository : BaseRepository, IAdsRepository
    {
        private readonly IUploadRepository _Upload;
        public AdsRepository(DbContext context, IMapper mapper, IUploadRepository Upload) : base(context, mapper)
        {
            _Upload = Upload;
        }

        public Task<GenericResponse<AdsDto?>> Get(Guid? id)
        {
            if (id == null)
            {
                return Task.FromResult(new GenericResponse<AdsDto?>
                    (new AdsDto(), UtilitiesStatusCodes.New, "New"));
            }
            else
            {
                var adsEntity = _context.Set<AdsEntity>()
                .Where(x => x.Id == id)
                .Select(x => new AdsDto()
                {
                    Id = x.Id,
                    SpecialExpireDateTime = x.SpecialExpireDateTime,
                    Amount = x.Amount,
                    Body = x.Body,
                    CategoryId = x.CategoryId,
                    CityId = x.CityId,
                    CountryId = x.CountryId,
                    ExpireDateTime = x.ExpireDateTime,
                    HomePageExpireDateTime = x.HomePageExpireDateTime,
                    LanguageId = x.LanguageId,
                    Lid = x.Lid,
                    NumberOfLikes = x.NumberOfLikes,
                    ProvinceId = x.ProvinceId,
                    Publish = x.Publish,
                    Status = x.Status,
                    Title = x.Title,
                    UserId = x.UserId
                }).FirstOrDefault();
                if (adsEntity == null)
                {
                    return Task.FromResult(new GenericResponse<AdsDto?>
                    (new AdsDto(), UtilitiesStatusCodes.NotFound, "NotFound"));
                }
                return Task.FromResult(new GenericResponse<AdsDto?>
                    (adsEntity, UtilitiesStatusCodes.Success, "Success"));
            }
        }


        public async Task<GenericResponse<AdsDto>> AddEdit(AdsDto ads)
        {
            if (ads.Id == null)
            {
                var l = _context.Set<LanguageEntity>().Find(ads.LanguageId);
                var model = new AdsEntity()
                {
                    
                    Amount = ads.Amount,
                    Body = ads.Body,
                    CategoryId = ads.CategoryId,
                    CityId = ads.CityId,
                    CountryId = ads.CountryId,
                    //ExpireDateTime=ads.
                    Id = Guid.NewGuid(),
                    LanguageId = ads.LanguageId,
                    Lid = ads.Lid,
                    NumberOfLikes = 0,
                    ProvinceId = ads.ProvinceId,
                    Publish = ads.Publish,
                    SpecialExpireDateTime = ads.SpecialExpireDateTime,
                    Status = ads.Status,
                    UserId = ads.UserId,
                    //TinyURL= 
                    Title = ads.Title,
                };
                //model.LanguageNavigation = l;
                //model.Category.LanguageNavigation = l;
                ///todo:Relation Model null سینا
                     _context.Set<AdsEntity>().Add(model);
                await _context.SaveChangesAsync();
                var up = await _Upload.UploadMedia(new UploadDto()
                {
                    UserId = ads.UserId,
                    Files = ads.files,
                    AdsId = ads.Id
                });
                return new GenericResponse<AdsDto>
                    (ads, UtilitiesStatusCodes.Success, "Success");
            }
            else
            {
                var old = _context.Set<AdsEntity>().FirstOrDefault(x => x.Id == ads.Id);
                if (old == null)
                    return new GenericResponse<AdsDto>
                    (null, UtilitiesStatusCodes.NotFound, "NotFound");
                old.Amount = ads.Amount;
                old.Body = ads.Body;
                old.CategoryId = ads.CategoryId;
                old.CityId = ads.CityId;
                old.CountryId = ads.CountryId;
                old.LanguageId = ads.LanguageId;
                old.Lid = ads.Lid;
                old.NumberOfLikes = ads.NumberOfLikes ?? 0;
                old.ProvinceId = ads.ProvinceId;
                old.Publish = ads.Publish;
                old.SpecialExpireDateTime = ads.SpecialExpireDateTime;
                old.Status = ads.Status;
                old.UserId = ads.UserId;
                old.Title = ads.Title;
                old.UpdatedAt = DateTime.Now;
                _context.Set<AdsEntity>().Update(old);
                var up = await _Upload.UploadMedia(new UploadDto()
                {
                    UserId = ads.UserId,
                    Files = ads.files,
                    AdsId = ads.Id
                });
                return new GenericResponse<AdsDto>
                    (ads, UtilitiesStatusCodes.Success, "Success");
            }
        }

        public async Task<GenericResponse<AdsEntity>> Delete(Guid id)
        {
            var ads = _context.Set<AdsEntity>()
                .Include(x => x.MediaList)
                .Where(x => x.Id == id).First();
            if (ads.MediaList != null)
                foreach (var item in ads.MediaList)
                {
                    _Upload.DeleteMedia(item.Id);
                }
            _context.Set<AdsEntity>().Remove(ads);
            _context.SaveChanges();
            return new GenericResponse<AdsEntity>
                    (ads, UtilitiesStatusCodes.Success, "Success");
        }
    }
}
