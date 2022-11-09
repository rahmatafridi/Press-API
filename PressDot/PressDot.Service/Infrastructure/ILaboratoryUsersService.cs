using System.Collections.Generic;
using PressDot.Core.Domain;

namespace PressDot.Service.Infrastructure
{
    public interface ILaboratoryUsersService : IService<LaboratoryUsers>
    {
        LaboratoryUsers CreateLaboratoryUser(LaboratoryUsers laboratoryUser);

        ICollection<LaboratoryUsers> GetLaboratoryUsersByLaboratoryId(int laboratoryId);
        List<LaboratoryUsers> GetLaboratoryUserById(int userId);
        bool CheckLaboratoryUserExist(int laboratoryId, int userId);
    }
}
