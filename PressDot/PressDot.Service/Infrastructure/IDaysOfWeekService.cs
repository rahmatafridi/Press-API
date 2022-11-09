using PressDot.Core.Domain;
using System.Collections.Generic;

namespace PressDot.Service.Infrastructure
{
    public interface IDaysOfWeekService
    {
        List<DaysOfWeek> GetDays();
        string GetDayById(int id);
    }
}
