using PressDot.Core;
using PressDot.Core.Domain;

namespace PressDot.Service.Infrastructure
{
    public interface ILaboratoryService : IService<Laboratory>
    {
        IPagedList<Laboratory> GetLaboratories(string name, int pageIndex, int pageSize);
        bool CheckLaboratoryById(int id);

    }
}
