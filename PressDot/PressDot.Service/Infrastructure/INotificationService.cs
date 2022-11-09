using PressDot.Contracts;
using PressDot.Contracts.Request.Notification;
using PressDot.Contracts.Response.Notification;
using PressDot.Core;
using PressDot.Core.Domain;
using System.Collections.Generic;

namespace PressDot.Service.Infrastructure
{
    public interface INotificationService : IService<Notification>
    {
        Notification CreateNotification(List<Notification> notification);
        Notification GetNotificationById(int id);
        Notification CheckMadeNotification(int userId);
        IPagedList<Notification> GetNotificationsAdmin(int pageIndex, int pageSize);

        IPagedList<Notification> GetNotifications(int id, int pageIndex, int pageSize);
        bool UpdateNotification(UpdateNotificationRequest updateConsentRequest);
       
    }
}
