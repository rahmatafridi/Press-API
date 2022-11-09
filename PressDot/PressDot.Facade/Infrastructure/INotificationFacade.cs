using PressDot.Contracts;
using PressDot.Contracts.Response.Notification;
using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Facade.Infrastructure
{
    public interface INotificationFacade
    {
       // ICollection<NotificationResponse> GetNotifications(int? id);
        PressDotPageListEntityModel<NotificationResponse> GetNotifications(int id, int pageIndex, int pageSize);

    }
}
