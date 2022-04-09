using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using UAParser;
using Utilities_aspnet.Statistic.Dtos;
using Utilities_aspnet.Statistic.Entities;
using Utilities_aspnet.User.Entities;

namespace Utilities_aspnet.Statistic.Data
{
    public interface IStatisticRepository
    {
        Task<bool> Add(ActionExecutingContext context);
        Task<AdminStatisticDto> Show();
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
            IPAddress? ip = context.HttpContext.Connection.RemoteIpAddress;
            string? urlReferrer = context.HttpContext.Request.Headers["Referer"].ToString();
            StringValues userAgent = context.HttpContext.Request.Headers["User-Agent"];
            Parser? uaParser = Parser.GetDefault();
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
                //todo:افزودن به لیست بازدید
                DateTime data;
                string? key = $"ip_{ip}_{c.Device.Family}";

                if (!_cache.TryGetValue(key, out data))
                {
                    DateTime dt = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                    _cache.Set(key, data, dt);
                    try
                    {
                        VisitorEntity? newv = new VisitorEntity()
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
                    StatisticEntity? news = new StatisticEntity()
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

        public async Task<AdminStatisticDto> Show()
        {
            AdminStatisticDto? model = new AdminStatisticDto();
            model.CountOfTodayVisitor = _dbContext.Set<VisitorEntity>().Count(x => x.Date == DateTime.Now.Date);
            model.CountOfTodayPageVisit = _dbContext.Set<StatisticEntity>().Count(x => x.Date == DateTime.Now.Date);
            model.CountOfTodayBot = _dbContext.Set<StatisticEntity>()
                .Count(x => x.Date == DateTime.Now.Date
                && (x.UserAgentFamily.Contains("bot") || x.UserAgentFamily.Contains("Bot")));


            model.CountOfYesterdayVisitor = _dbContext.Set<VisitorEntity>().Count(x => x.Date == DateTime.Now.Date.AddDays(-1));
            model.CountOfYesterdayPageVisit = _dbContext.Set<StatisticEntity>().Count(x => x.Date == DateTime.Now.Date.AddDays(-1));
            model.CountOfYesterdayBot = _dbContext.Set<StatisticEntity>()
                .Count(x => x.Date == DateTime.Now.Date.AddDays(-1)
                            && (x.UserAgentFamily.Contains("bot") || x.UserAgentFamily.Contains("Bot")));

            //model.CountTotalComment = _dbContext.Set<Comment>().Count();
            //model.CountNewComment = _dbContext.Set<Comment>().Count(x => x.CommentPublish == false);

            model.VisitorDic = _dbContext.Set<VisitorEntity>().GroupBy(x => x.Date).Select(y => new { name = y.Key, count = y.Count() })
                .ToDictionary(k => k.name, i => i.count);


            model.UserAgentTodayDic = _dbContext.Set<StatisticEntity>()
                .Where(x => x.Date == DateTime.Now.Date && (!x.UserAgentFamily.Contains("bot") && !x.UserAgentFamily.Contains("Bot")))
                .GroupBy(x => x.UserAgentFamily).Select(y => new { name = y.Key, count = y.Count() })
                    .OrderByDescending(x => x.count)
                    .ToDictionary(k => k.name, i => i.count);

            model.BotAgentTodayDic = _dbContext.Set<StatisticEntity>()
                .Where(x => x.Date == DateTime.Now.Date && (x.UserAgentFamily.Contains("bot") || x.UserAgentFamily.Contains("Bot")))
                .GroupBy(x => x.UserAgentFamily).Select(y => new { name = y.Key, count = y.Count() })
                .OrderByDescending(x => x.count)
                .ToDictionary(k => k.name, i => i.count);



            model.UserAgentDic = _dbContext.Set<StatisticEntity>()
                .Where(x => !x.UserAgentFamily.Contains("bot") && !x.UserAgentFamily.Contains("Bot"))
                .GroupBy(x => x.UserAgentFamily).Select(y => new { name = y.Key, count = y.Count() })
                .OrderByDescending(x => x.count)
                .ToDictionary(k => k.name, i => i.count);

            model.BotAgentDic = _dbContext.Set<StatisticEntity>()
                .Where(x => x.UserAgentFamily.Contains("bot") || x.UserAgentFamily.Contains("Bot"))
                .GroupBy(x => x.UserAgentFamily).Select(y => new { name = y.Key, count = y.Count() })
                .OrderByDescending(x => x.count)
                .ToDictionary(k => k.name, i => i.count);


            model.TopUrlTodayDic = _dbContext.Set<StatisticEntity>()
                .Where(x => x.Date == DateTime.Now.Date && !x.UserAgentFamily.Contains("bot") && !x.UserAgentFamily.Contains("Bot"))
                .GroupBy(x => x.Url).Select(y => new { name = y.Key, count = y.Count() })
                .OrderByDescending(x => x.count).Take(10)
                .ToDictionary(k => k.name, i => i.count);

            model.TopUrlDic = _dbContext.Set<StatisticEntity>()
                .Where(x => !x.UserAgentFamily.Contains("bot") && !x.UserAgentFamily.Contains("Bot"))
                .GroupBy(x => x.Url).Select(y => new { name = y.Key, count = y.Count() })
                .OrderByDescending(x => x.count).Take(10)
                .ToDictionary(k => k.name, i => i.count);

            model.OsDicToday = _dbContext.Set<StatisticEntity>()
                .Where(x => x.Date == DateTime.Now.Date && !x.UserAgentFamily.Contains("bot") && !x.UserAgentFamily.Contains("Bot"))
                .GroupBy(x => x.Osfamily).Select(y => new { name = y.Key, count = y.Count() })
                .OrderByDescending(x => x.count)
                .ToDictionary(k => k.name, i => i.count);

            model.OsDic = _dbContext.Set<StatisticEntity>()
                .Where(x => !x.UserAgentFamily.Contains("bot") && !x.UserAgentFamily.Contains("Bot"))
                .GroupBy(x => x.Osfamily).Select(y => new { name = y.Key, count = y.Count() })
                .OrderByDescending(x => x.count)
                .ToDictionary(k => k.name, i => i.count);

            model.HoursDic = _dbContext.Set<StatisticEntity>()
                .Where(x => !x.UserAgentFamily.Contains("bot") && !x.UserAgentFamily.Contains("Bot"))
                .GroupBy(x => x.Hours)
                .Select(y => new { name = y.Key, count = y.Count() })
                .OrderBy(x => x.name)
                .ToDictionary(k => k.name, i => i.count);

            model.UserCount = _dbContext.Set<UserEntity>().Count();

            return model;
        }
    }
}
