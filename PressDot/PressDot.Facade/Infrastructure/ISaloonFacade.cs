using PressDot.Contracts;
using PressDot.Contracts.Request.Saloon;
using PressDot.Contracts.Request.SaloonLaboratory;
using PressDot.Contracts.Response.Saloon;
using PressDot.Contracts.Response.SaloonLaboratory;
using System;
using System.Collections.Generic;
using PressDot.Core.Domain;

namespace PressDot.Facade.Infrastructure
{
    public interface ISaloonFacade
    {
        PressDotPageListEntityModel<GetSaloonsResponse> GetSaloons(string name, int? countryId, int? cityId, int pageIndex, int pageSize);
        ICollection<GetSaloonsResponse> GetSaloonsByLocation(string location);

        ICollection<GetSaloonsResponse> GetSaloonsByLocationOrSaloonName(string location, string saloonName);

        ICollection<GetSaloonsResponse> GetSaloonsByLocationId(int locationId);

        ICollection<GetSaloonAppointmentsDto> GetSaloonAppointments(int saloonId, DateTime? date);
        /// <summary>
        /// Update saloon name and location
        /// </summary>
        /// <param name="UpdateSaloon"></param>
        /// <returns></returns>
        GetSaloonsResponse UpdateSaloon(SaloonUpdateRequest UpdateSaloon);
        /// <summary>
        /// delete gets and delete saloon by id if there is no pending appointment for saloon.
        /// </summary>
        /// <param name="saloonId"></param>
        /// <returns></returns>
        bool DeleteSaloon(int saloonId);
        /// <summary>
        /// Change Saloon Default Laboratory
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        SaloonLaboratoryResponse ChangeDefaultLaboratory(SaloonLaboratoryCreateRequest model);

        bool DeleteSaloonLaboratory(int saloonLaboratoryId);
        List<SaloonLaboratoryResponse> GetLaboratoriesBySaloonId(int saloonId);
        Saloon GetSaloonById(int saloonId);

        /// <summary>
        /// Get saloon list
        /// </summary>
        /// <param name="searchTerm"> Search Term</param>
        /// <returns></returns>
        ICollection<Saloon> GetSaloon(string searchTerm);
    }
}
