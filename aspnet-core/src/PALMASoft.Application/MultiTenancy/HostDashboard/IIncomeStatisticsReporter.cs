using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PALMASoft.MultiTenancy.HostDashboard.Dto;

namespace PALMASoft.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}