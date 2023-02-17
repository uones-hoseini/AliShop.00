﻿using AliShop.Application.Contexts.Interfaces;
using AliShop.Domain.Visitors;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliShop.Application.Visitors.GetTodayReport
{
    public interface IGetTodayReportService
    {
        ResultTodayReportDto Execute();
    }
    public class GetTodayReportService : IGetTodayReportService
    {
        private readonly IMongoDbContext<Visitor> _mongoDbContext;
        private readonly IMongoCollection<Visitor> visitorMongoCollection;

        public GetTodayReportService(IMongoDbContext<Visitor> mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
            visitorMongoCollection = _mongoDbContext.GetCollection();
        }

        public ResultTodayReportDto Execute()
        {
            DateTime start = DateTime.Now.Date;
            DateTime end = DateTime.Now.Date.AddDays(1);
            var TodayPageViewCount = visitorMongoCollection.AsQueryable()
                .Where(p => p.Time >= start && p.Time < end).LongCount();
            var TodayVisitorCount = visitorMongoCollection.AsQueryable()
                .Where(p => p.Time >= start && p.Time < end).GroupBy(p => p.VisitorId)
                .LongCount();
            var AllPageViewCount = visitorMongoCollection.AsQueryable().LongCount();
            var AllVisitorCount = visitorMongoCollection.AsQueryable()
                .GroupBy(p => p.VisitorId).LongCount();

            VisitCountDto visitPerHour = GetVisitPerHour(start, end);

            VisitCountDto visitoPerDay = GetVisitPerDay();

            var visitors = visitorMongoCollection.AsQueryable()
             .OrderByDescending(p => p.Time)
             .Take(10)
             .Select(p => new VisitorsDto
             {
                 Id = p.Id,
                 Browser = p.Browser.Family,
                 CurrentLink = p.CurrentLink,
                 Ip = p.Ip,
                 OperationSystem = p.OperationSystem.Family,
                 IsSpider = p.Device.IsSpider,
                 ReferrerLink = p.ReferrerLink,
                 Time = p.Time,
                 VisitorId = p.VisitorId
             }).ToList();

            return new ResultTodayReportDto
            {
                GeneralStats = new GeneralStatsDto
                {
                    TotalPageViews = AllPageViewCount,
                    TotalVisitors = AllVisitorCount,
                    PageViewsPerVisit = GetAvg(AllPageViewCount, AllVisitorCount),
                    VisitPerDay = visitoPerDay,
                },
                Today = new TodayDto
                {
                    PageViews = TodayPageViewCount,
                    Visitors = TodayVisitorCount,
                    ViewPerVisitor = GetAvg(TodayPageViewCount, TodayVisitorCount),
                    VisitPerhour = visitPerHour,
                },
                visitors=visitors,
            };
        }

        private VisitCountDto GetVisitPerHour(DateTime start, DateTime end)
        {
            var TodayPageViewList = visitorMongoCollection.AsQueryable().Where(p => p.Time >= start
            && p.Time < end).Select(p => new { p.Time }).ToList();

            VisitCountDto visitPerHour = new VisitCountDto()
            {
                Display = new string[24],
                Value = new int[24],
            };

            for (int i = 0; i <= 23; i++)
            {
                visitPerHour.Display[i] = $"h-{i}";
                visitPerHour.Value[i] = TodayPageViewList.Where(p => p.Time.Hour == i).Count();
            }

            return visitPerHour;
        }

        private VisitCountDto GetVisitPerDay()
        {
            DateTime MounthStart = DateTime.Now.AddDays(-30);
            DateTime MounthEnd = DateTime.Now.AddDays(1);
            var Mounth_PageViewList = visitorMongoCollection.AsQueryable()
                .Where(p => p.Time >= MounthStart && p.Time <= MounthEnd)
                .Select(p => new { p.Time })
                .ToList();
            VisitCountDto visitoPerDay = new VisitCountDto()
            {
                Display = new string[31],
                Value = new int[31],
            };
            for (int i = 0; i <= 30; i++)
            {
                var currectDay = DateTime.Now.AddDays(i * (-1));
                visitoPerDay.Display[i] = i.ToString();
                visitoPerDay.Value[i] = Mounth_PageViewList.Where(p => p.Time == currectDay.Date).Count();
            }

            return visitoPerDay;
        }

        private float GetAvg(long VisitPage, long Visitor)
        {
            if (Visitor == 0)
            {
                return 0;
            }
            else
            {
                return VisitPage / Visitor;
            }
        }
    }
    public class ResultTodayReportDto
    {
        public GeneralStatsDto GeneralStats { get; set; }
        public TodayDto Today { get; set; }
        public List<VisitorsDto> visitors { get; set; }
    }
    public class GeneralStatsDto
    {
        public long TotalPageViews { get; set; }
        public long TotalVisitors { get; set; }
        public float PageViewsPerVisit { get; set; }
        public VisitCountDto VisitPerDay { get; set; }
    }
    public class TodayDto
    {
        public long PageViews { get; set; }
        public long Visitors { get; set; }
        public float ViewPerVisitor { get; set; }
        public VisitCountDto VisitPerhour { get; set; }
    }

    public class VisitCountDto
    {
        public string[] Display { get; set; }
        public int[] Value { get; set; }
    }
    public class VisitorsDto
    {
        public string Id { get; set; }
        public string Ip { get; set; }
        public string CurrentLink { get; set; }
        public string ReferrerLink { get; set; }
        public string Browser { get; set; }
        public string OperationSystem { get; set; }
        public bool IsSpider { get; set; }
        public DateTime Time { get; set; }
        public string VisitorId { get; set; }

    }
}
