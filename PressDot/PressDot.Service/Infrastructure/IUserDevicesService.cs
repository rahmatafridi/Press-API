using System;
using System.Collections.Generic;
using System.Text;
using PressDot.Core.Domain;

namespace PressDot.Service.Infrastructure
{
    public interface IUserDevicesService : IService<UserDevices>
    {
        UserDevices GetUserDeviceByUserIdAndDeviceId(int userId,string deviceId);

        ICollection<UserDevices> GetUserDevicesByUserId(int userId);

        bool DeleteAllUserDeviceToken(int userId);
    }
}
