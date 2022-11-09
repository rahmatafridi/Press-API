using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PressDot.Core.Data;
using PressDot.Core.Domain;
using PressDot.Service.Infrastructure;

namespace PressDot.Service.Implementation
{
    public class UserDevicesService : BaseService<UserDevices>, IUserDevicesService
    {

        #region properties



        #endregion


        #region ctor

        public UserDevicesService(IRepository<UserDevices> userDevicesRepository) : base(userDevicesRepository)
        {
            
        }

        #endregion

        #region Methods
        
        
        public bool DeleteAllUserDeviceToken(int userId)
        {
            var userDevices = GetUserDevicesByUserId(userId);

            if (userDevices == null || userDevices.Count == 0)
                return false;
            foreach (var userDevice in userDevices)
            {
                userDevice.IsDeleted = true;
                userDevice.DeletedDate = DateTime.Now;
            }
            Repository.Delete(userDevices);
            return true;
        }

        public ICollection<UserDevices> GetUserDevicesByUserId(int userId)
        {
            return Repository.Table.Where(x => x.UserId == userId).ToList();
        }

        public UserDevices GetUserDeviceByUserIdAndDeviceId(int userId, string deviceId)
        {
            return Repository.Table.FirstOrDefault(x => x.UserId == userId && x.DeviceId.Equals(deviceId));
        }


        #endregion
    }
}
