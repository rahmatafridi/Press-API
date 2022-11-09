using PressDot.Core.Data;
using PressDot.Core.Domain;
using PressDot.Service.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace PressDot.Service.Implementation
{
    public class DayOfWeekService : BaseService<DaysOfWeek>, IDaysOfWeekService
    {
        #region private members


        #endregion

        #region ctor

        public DayOfWeekService(IRepository<DaysOfWeek> repository) : base(repository)
        {

        }

        #endregion

        #region methods

        public List<DaysOfWeek> GetDays()
        {
            return Repository.Table.ToList();
        }


        public string GetDayById(int id)
        {
            var day = Repository.Table.FirstOrDefault(x => x.Id == id);
            return day != null ? day.Day : "";
        }
        #endregion

    }
}
