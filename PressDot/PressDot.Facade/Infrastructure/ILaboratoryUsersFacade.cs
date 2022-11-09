using System.Collections.Generic;
using PressDot.Contracts.Request.LaboratoryUsers;
using PressDot.Contracts.Response.Laboratory;

namespace PressDot.Facade.Infrastructure
{
    public interface ILaboratoryUsersFacade
    {
        bool CreateLaboratoryUser(CreateLaboratoryUsersRequest request);

        ICollection<LaboratoryUserResponse> GetLaboratoryUsersByLaboratoryId(int laboratoryId);

        bool DeleteLaboratoryUser(int laboratoryUserId);
    }
}
