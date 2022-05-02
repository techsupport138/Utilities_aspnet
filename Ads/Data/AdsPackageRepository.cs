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
    public interface IAdsPackageRepository
    {
        Task<GenericResponse<AdsPackageDto?>> Get(int? Id);
        Task<GenericResponse<AdsPackageDto>> AddEdit(AdsPackageDto ads);
        Task<GenericResponse<AdsPackageEntity>> Delete(int id);
    }
    public class AdsPackageRepository : BaseRepository, IAdsPackageRepository
    {
        private readonly IUploadRepository _Upload;
        public AdsPackageRepository(DbContext context, IMapper mapper, IUploadRepository Upload) : base(context, mapper)
        {
            _Upload = Upload;
        }

        public Task<GenericResponse<AdsPackageDto?>> Get(int? id)
        {
            if (id == null)
            {
                return Task.FromResult(new GenericResponse<AdsPackageDto?>
                    (new AdsPackageDto(), UtilitiesStatusCodes.New, "New"));
            }
            else
            {
                var adsEntity = _context.Set<AdsPackageEntity>()
                .Include(a => a.LanguageNavigation)
                .Where(x => x.AdsPackageId == id)
                .Select(x=>new AdsPackageDto()
                {
                    AdsPackageId=x.AdsPackageId,
                    Amount=x.Amount,
                    Details=x.Details,
                    Enable=x.Enable,
                    HomePageExpireDate=x.HomePageExpireDate,
                    LanguageId=x.LanguageId,
                    SpecialExpireDate=x.SpecialExpireDate,
                    Title=x.Title,
                    
                }).FirstOrDefault()
                ;
                if (adsEntity == null)
                {
                    return Task.FromResult(new GenericResponse<AdsPackageDto?>
                    (null, UtilitiesStatusCodes.NotFound, "NotFound"));
                }
                return Task.FromResult(new GenericResponse<AdsPackageDto?>
                    (adsEntity, UtilitiesStatusCodes.Success, "Success"));
            }
        }


        public async Task<GenericResponse<AdsPackageDto>>
            AddEdit(AdsPackageDto ads)
        {
            if (ads.AdsPackageId == null)
            {
                var model = new AdsPackageEntity()
                {
                    Details=ads.Details,
                    Enable=ads.Enable,
                    HomePageExpireDate=ads.HomePageExpireDate,
                    LanguageId=ads.LanguageId,
                    SpecialExpireDate=ads.SpecialExpireDate,
                    Amount = ads.Amount,
                    Title = ads.Title,
                };
                _context.Set<AdsPackageEntity>().Add(model);
                //var up = await _Upload.UploadMedia(new UploadDto()
                //{
                //    UserId = ads.UserId,
                //    //Files = ads.files,
                //    //adsp = ads.AdsPackageId
                //});
                return new GenericResponse<AdsPackageDto>
                    (ads, UtilitiesStatusCodes.Success, "Success");
            }
            else
            {
                var old = _context.Set<AdsPackageEntity>().FirstOrDefault(x => x.AdsPackageId == ads.AdsPackageId);
                if (old == null)
                    return new GenericResponse<AdsPackageDto>
                    (null, UtilitiesStatusCodes.NotFound, "NotFound");
                old.Details = ads.Details;
                old.Enable = ads.Enable;
                old.HomePageExpireDate = ads.HomePageExpireDate;
                old.LanguageId = ads.LanguageId;
                old.SpecialExpireDate = ads.SpecialExpireDate;
                old.Amount = ads.Amount;
                old.Title = ads.Title;
                _context.Set<AdsPackageEntity>().Update(old);
                //var up = await _Upload.UploadMedia(new UploadDto()
                //{
                //    UserId = ads.UserId,
                //    Files = ads.files,
                //    AdsId = ads.Id
                //});
                return new GenericResponse<AdsPackageDto>
                    (ads, UtilitiesStatusCodes.Success, "Success");
            }
        }

        public async Task<GenericResponse<AdsPackageEntity>> Delete(int id)
        {
            var ads = _context.Set<AdsPackageEntity>()
                //.Include(x => x.MediaList)
                .Where(x => x.AdsPackageId == id).First();
            //if (ads.MediaList != null)
            //    foreach (var item in ads.MediaList)
            //    {
            //        _Upload.DeleteMedia(item.Id);
            //    }
            _context.Set<AdsPackageEntity>().Remove(ads);
            _context.SaveChanges();
            return new GenericResponse<AdsPackageEntity>
                    (ads, UtilitiesStatusCodes.Success, "Success");
        }
    }
}
