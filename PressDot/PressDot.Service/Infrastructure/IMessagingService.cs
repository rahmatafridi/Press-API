using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PressDot.Service.Infrastructure
{
    public interface IMessagingService
    {
        Task SendNotification(string token, string title, string body,int userId);
    }
}
