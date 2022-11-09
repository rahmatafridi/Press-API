using PressDot.Contracts;
using PressDot.Contracts.Response.Notification;
using PressDot.Facade.Infrastructure;
using System;
using System.Collections.Generic;
using PressDot.Service.Infrastructure;
using System.Text;
using PressDot.Facade.Framework.Extensions;
using System.Linq;

namespace PressDot.Facade.Implementation
{
    public class NotificationFacade : INotificationFacade
    {
        private readonly INotificationService _notificationService;
        private readonly IUsersService _usersService;

        public NotificationFacade (IUsersService usersService,INotificationService notificationService)
        {
            _notificationService = notificationService;
            _usersService= usersService;    
        }
        //public ICollection<NotificationResponse> GetNotifications(int? id)
        //{
        //    throw new NotImplementedException();
        //}

        public PressDotPageListEntityModel<NotificationResponse> GetNotifications(int id, int pageIndex, int pageSize)
        {
            try
            {

            
            if (id != 0)
            {
                var notification = _notificationService.GetNotifications(id, pageIndex, pageSize);
                var userList = new PressDotPageListEntityModel<NotificationResponse>()
                {
                    TotalCount = notification.TotalCount,
                    Data = notification.ToModel<NotificationResponse>(),
                    HasNextPage = notification.HasNextPage,
                    HasPreviousPage = notification.HasPreviousPage,
                    TotalPages = notification.TotalPages,
                    PageIndex = notification.PageIndex,
                    PageSize = notification.PageSize

                };
                return userList;
            }
            else
            {
                var notification = _notificationService.GetNotificationsAdmin(pageIndex, pageSize);
                  //  var roles = _userRoleService.Get(users.Select(x => x.UserRoleId).ToArray());
                  var data=  _usersService.Get(notification.Select(x => x.UserId).ToArray());

                    foreach (var user in notification)
                    {
                        user.Users = data.FirstOrDefault(x => x.Id == user.UserId);
                    }

                    var userList = new PressDotPageListEntityModel<NotificationResponse>()
                {
                    TotalCount = notification.TotalCount,
                    Data = notification.ToModel<NotificationResponse>(),
                    HasNextPage = notification.HasNextPage,
                    HasPreviousPage = notification.HasPreviousPage,
                    TotalPages = notification.TotalPages,
                    PageIndex = notification.PageIndex,
                    PageSize = notification.PageSize

                };
                return userList;
            }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
