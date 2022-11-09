using PressDot.Core;
using PressDot.Core.Domain;
using System.Collections.Generic;

namespace PressDot.Service.Infrastructure
{
    public interface ISaloonService : IService<Saloon>
    {
        IPagedList<Saloon> GetSaloons(string name, int? countryId, int? cityId, int pageIndex, int pageSize);
        List<Saloon> GetSaloonsByLocationId(int locId);
        /// <summary>
        /// Check saloon by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CheckSaloonById(int id);
        /// <summary>
        /// Update Saloon name and location.
        /// </summary>
        /// <param name="saloon"></param>

        /// <returns></returns>
        Saloon UpdateSaloon(Saloon saloon);
        /// <summary>
        /// Get Saloon by Saloon id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Saloon GetSaloonById(int id);
        /// <summary>
        /// Delete saloon
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool DeleteSaloon(Saloon entity);

        ICollection<Saloon> GetSaloons(string searchTerm);
    }
}
