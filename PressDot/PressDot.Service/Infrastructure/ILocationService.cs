using PressDot.Core;
using PressDot.Core.Domain;
using System.Collections.Generic;

namespace PressDot.Service.Infrastructure
{
    public interface ILocationService : IService<Location>
    {
        /// <summary>
        /// Check if location name already exist
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool CheckLocationExist(string name);

        /// <summary>
        /// Get child locations
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        ICollection<Location> GetLocationsByParentId(int locationId);


        /// <summary>
        /// Get parent locations
        /// </summary>
        /// <returns></returns>
        ICollection<Location> GetParentLocations();

        /// <summary>
        /// Get Location Id by location name
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        int? GetLocationIdByName(string location);


        /// <summary>
        /// Get location list by location name
        /// </summary>
        /// <param name="locationName"></param>
        /// <returns>List of location</returns>
        ICollection<Location> GetLocationByName(string locationName);

        /// <summary>
        /// Get list of locations
        /// </summary>
        /// <param name="locationName">location name optional</param>
        /// <param name="pageIndex">start page. By default it is zero</param>
        /// <param name="pageSize">number of record to be displayed on screen. By defult it is 10</param>
        /// <returns>List of location</returns>
        IPagedList<Location> GetLocations(string locationName, int pageIndex, int pageSize);
    }
}
