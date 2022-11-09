using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PressDot.Contracts.Request.Contsent;
using PressDot.Contracts.Request.Notification;
using PressDot.Facade.Infrastructure;
using PressDot.Service.Infrastructure;

namespace PressDot.Controllers
{
    [Route("api/v1/Notification")]
    public class NotificationController : AuthenticatedController
    {
        #region Private Members
        private readonly INotificationService _notification;
        private readonly INotificationFacade _notificationFacade;
        #endregion
        #region ctor

        public NotificationController(INotificationFacade notificationFacade, INotificationService notification)
        {
            _notification = notification;
            _notificationFacade = notificationFacade;
        }

        #endregion  

        [HttpGet]
        [Route("GetNotification")]
        public IActionResult GetNotification(int userId)
        {
            var result = _notification.GetNotificationById(userId);
            return Ok(result);
        }
        [HttpPut]
        [Route("UpdateNotification")]
        public IActionResult UpdateNotification(UpdateNotificationRequest updateConsentRequest)
        {
            var result = _notification.UpdateNotification(updateConsentRequest);
            return Ok(result);
        }
        [HttpGet]
        [Route("GetNotifications")]
        public IActionResult GetNotifications(int id, int pageIndex = 0, int pageSize = 10)
        {
            return Ok(_notificationFacade.GetNotifications(id, pageIndex, pageSize));
        }

    }
}
