using PressDot.Contracts;
using PressDot.Contracts.Request.Notification;
using PressDot.Contracts.Response.Notification;
using PressDot.Core;
using PressDot.Core.Data;
using PressDot.Core.Domain;
using PressDot.Core.Exceptions;
using PressDot.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PressDot.Service.Implementation
{
    public class NotificationService : BaseService<Notification>, INotificationService
    {

        public NotificationService(IRepository<Notification> repository) : base(repository)
        {
        }

        public Notification CheckMadeNotification(int userId)
        {
            return Repository.Table.FirstOrDefault(x => x.UserId == userId && x.Type==1);
        }

        public Notification CreateNotification(List<Notification> notification)
        {
            if (notification != null)
            {
                foreach (var item in notification)
                {
                    Create(item);

                }
                return notification.FirstOrDefault();
            }
            return null;
        }

        public Notification GetNotificationById(int id)
        {
            return Repository.Table.FirstOrDefault(x => x.Id == id);
        }

        public IPagedList<Notification> GetNotifications(int id, int pageIndex, int pageSize)
        {

            var query = Repository.Table.Where(x=>x.UserId==id);
          
            return new PagedList<Notification>(query, pageIndex, pageSize);
        }
             public IPagedList<Notification> GetNotificationsAdmin(int pageIndex, int pageSize)
        {

            var query = Repository.Table;
          
            return new PagedList<Notification>(query, pageIndex, pageSize);
        }

        public bool UpdateNotification(UpdateNotificationRequest request)
        {
            if (request == null) throw new PressDotException("Invalid request to update user information.");
            var notification = GetNotificationById(request.Id);

            notification.UpdatedDate = DateTime.Now;
            notification.IsAppointmentMadeStatus = request.IsAppointmentMadeStatus;
            return Update(notification);
        }
    }
}
