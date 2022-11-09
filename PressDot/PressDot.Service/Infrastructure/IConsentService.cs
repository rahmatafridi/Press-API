using PressDot.Contracts.Request.Contsent;
using PressDot.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Service.Infrastructure
{
    public interface IConsentService : IService<Core.Domain.Consent>
    {
        Consent CreateConsent(Consent consent);
        Consent GetConsentById(int id);
        bool UpdateConsent(UpdateConsentRequest updateConsentRequest);
    }
}
