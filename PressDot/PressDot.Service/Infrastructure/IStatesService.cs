using PressDot.Core.Domain;
using System.Collections.Generic;

namespace PressDot.Service.Infrastructure
{
    public interface IStatesService : IService<States>
    {
        /// <summary>
        /// Returns all possible states that an appointment can have.
        /// </summary>
        /// <returns></returns>
        ICollection<States> GetStates();

        ICollection<States> GetAppointmentStates();
        ICollection<States> GetOrderStates();
        States GetStatesByStateNameAndType(string stateName, string type);

    }
}
