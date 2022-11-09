using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PressDot.Contracts.Request.Contsent;
using PressDot.Service.Infrastructure;

namespace PressDot.Controllers
{
    [Route("api/v1/Consent")]
    public class ConsentController : AuthenticatedController
    {
        #region Private Members
        private readonly IConsentService _consentService;
        #endregion

        #region ctor

        public ConsentController(IConsentService consentService)
        {
           _consentService= consentService;
        }

        #endregion  
        [HttpGet]
        [Route("GetConsent")]
        public IActionResult GetConsent(int userId)
        {
            var result = _consentService.GetConsentById(userId);
            return Ok(result);
        }
        [HttpPut]
        [Route("UpdateConsent")]
        public IActionResult UpdateConsent(UpdateConsentRequest updateConsentRequest)
        {
            var result = _consentService.UpdateConsent(updateConsentRequest);
            return Ok(result);
        }

    }
}
