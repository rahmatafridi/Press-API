using PressDot.Core.Data;
using PressDot.Core.Domain;
using PressDot.Service.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace PressDot.Service.Implementation
{
    class StateService : BaseService<States>, IStatesService
    {
        #region private members


        #endregion

        #region ctor

        public StateService(IRepository<States> repository) : base(repository)
        {

        }

        #endregion

        #region methods

        public ICollection<States> GetStates()
        {
            return Repository.Table.Where(s => !s.IsDeleted).ToList();
        }
        public ICollection<States> GetAppointmentStates()
        {
            return Repository.Table.Where(s => !s.IsDeleted && s.StateFor.Equals("Appointment")).ToList();
        }
        public ICollection<States> GetOrderStates()
        {
            return Repository.Table.Where(s => !s.IsDeleted && s.StateFor.Equals("Order")).ToList();
        }
        public States GetStatesByStateNameAndType(string stateName, string type)
        {
            return Repository.Table.FirstOrDefault(s => s.Value.Equals(stateName) && s.StateFor.Equals(type) && !s.IsDeleted);
        }

        #endregion
    }
}
