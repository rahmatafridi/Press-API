using System;
using System.Collections.Generic;
using System.Text;
using PressDot.Contracts;
using PressDot.Contracts.Response.Laboratory;
using PressDot.Core.Domain;

namespace PressDot.Facade.Infrastructure
{
    public interface ILaboratoryFacade
    {
        PressDotPageListEntityModel<LaboratoryResponse> GetLaboratories(string name, int pageIndex, int pageSize);

        bool DeleteLaboratory(int laboratoryId);

        bool UpdateLaboratory(Laboratory laboratory);
    }
}
