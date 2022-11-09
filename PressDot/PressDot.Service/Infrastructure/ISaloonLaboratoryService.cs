using PressDot.Core.Domain;
using System.Collections.Generic;

namespace PressDot.Service.Infrastructure
{
    public interface ISaloonLaboratoryService : IService<SaloonLaboratory>
    {
        ICollection<SaloonLaboratory> GetSaloonLaboratoriesBySaloonId(int saloonId);

        ICollection<SaloonLaboratory> GetSaloonLaboratoriesByLaboratoryId(int laboratoryId);
        /// <summary>
        /// Change Saloon's defualt Laboratory.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        SaloonLaboratory ChangeSaloonDefaultLaboratory(SaloonLaboratory entity, bool isUpdate);

        bool DeleteSaloonLaboratory(int saloonLaboratoryId);

        SaloonLaboratory GetDefaultSaloonLaboratoryBySaloonId(int saloonId);

        ICollection<SaloonLaboratory> IsLaboratoryAssociatedWithSaloon(int laboratoryId);

        ICollection<SaloonLaboratory> GetSaloonLaboratoriesBySaloonIds(int[] saloonIds);
        List<SaloonLaboratory> GetSaloonLaboratry(int laboratoryId);
    }
}
