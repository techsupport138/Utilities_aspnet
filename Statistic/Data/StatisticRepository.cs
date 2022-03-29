using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Caching.Memory;
using UAParser;
using Utilities_aspnet.Statistic.Entities;

namespace Utilities_aspnet.Statistic.Data
{
    public interface IStatisticRepository
    {
        Task<bool> Add(ActionExecutingContext context);
    }
    public class StatisticRepository : IStatisticRepository
    {
        private readonly IMemoryCache _cache;
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;

        public StatisticRepository(IMemoryCache cache, DbContext dbContext, IMapper mapper)
        {
            _cache = cache;
            _dbContext = dbContext;
            _mapper = mapper;
        }
        List<string> ignoreController = new List<string>(new string[] { "partial" });
        List<string> ignoreAction = new List<string>(new string[] { "read" });
        List<string> ignoreReferrer = new List<string>(new string[] { "localhost:" });
        List<string> ignoreAgent = new List<string>(new string[] { "DNTScheduler" });
        public async Task<bool> Add(ActionExecutingContext context)
        {
            string actionName = context.RouteData.Values["action"].ToString();
            string controllerName = context.RouteData.Values["controller"].ToString();
            string urlName = context.RouteData.Values.ContainsKey("url") ? context.RouteData.Values["url"].ToString() : "";
            var ip = context.HttpContext.Connection.RemoteIpAddress;
            var urlReferrer = context.HttpContext.Request.Headers["Referer"].ToString();
            var userAgent = context.HttpContext.Request.Headers["User-Agent"];
            var uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(userAgent);

            //Debug.WriteLine(c.UserAgent.Family); // => "Mobile Safari"
            //Debug.WriteLine(c.UserAgent.Major);  // => "5"
            //Debug.WriteLine(c.UserAgent.Minor);  // => "1"
            //Debug.WriteLine(c.OS.Family);        // => "iOS"
            //Debug.WriteLine(c.OS.Major);         // => "5"
            //Debug.WriteLine(c.OS.Minor);         // => "1"
            //Debug.WriteLine(c.Device.Family);    // => "iPhone"

            if (!ignoreController.Contains(controllerName.ToLower()) &&
                !ignoreAction.Contains(actionName.ToLower()) &&
                !ignoreReferrer.Contains(urlReferrer.ToLower()) &&
                !ignoreAgent.Contains(userAgent) &&
                !userAgent.Contains("DNTScheduler"))
            {
                ///todo:افزودن به لیست بازدید
                DateTime data;
                var key = $"ip_{ip}_{c.Device.Family}";

                if (!_cache.TryGetValue(key, out data))
                {
                    var dt = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                    _cache.Set(key, data, dt);
                    try
                    {
                        var newv = new VisitorEntity()
                        {
                            Date = DateTime.Now,
                            Hours = (byte)DateTime.Now.Hour,
                            Minutes = (byte)DateTime.Now.Minute,
                            Ip = ip.ToString(),
                            LandingPage = context.HttpContext.Request.Path,
                        };
                        EntityEntry<VisitorEntity> i =
                            await _dbContext.Set<VisitorEntity>()
                            .AddAsync(_mapper.Map<VisitorEntity>(newv));

                        _dbContext.SaveChanges();
                    }
                    catch { return false; }
                }

                try
                {
                    var news = new StatisticEntity()
                    {
                        //Url = urlName,
                        //Url = context.HttpContext.Request.Path + "/" + context.HttpContext.Request.QueryString,
                        Url = $"{context.HttpContext.Request.Scheme}://{context.HttpContext.Request.Host}{context.HttpContext.Request.Path}{context.HttpContext.Request.QueryString}",
                        Date = DateTime.Now,
                        Hours = (byte)DateTime.Now.Hour,
                        Minutes = (byte)DateTime.Now.Minute,
                        Ip = ip.ToString(),
                        Action = actionName,
                        Controller = controllerName,
                        Agent = userAgent,
                        DeviceFamily = c.Device.Family,
                        Osfamily = c.OS.Family,
                        Referrer = urlReferrer,
                        UserAgentFamily = c.UserAgent.Family
                    };
                    EntityEntry<StatisticEntity> i =
                            await _dbContext.Set<StatisticEntity>()
                            .AddAsync(_mapper.Map<StatisticEntity>(news));
                    _dbContext.SaveChanges();
                }
                catch { return false; }
                return true;
            }
            return false;
        }
    }
}
