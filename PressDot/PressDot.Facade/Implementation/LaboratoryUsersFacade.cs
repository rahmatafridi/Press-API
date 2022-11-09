using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PressDot.Contracts;
using PressDot.Contracts.Model;
using PressDot.Contracts.Response.Laboratory;
using PressDot.Facade.Framework.Extensions;
using PressDot.Facade.Infrastructure;
using PressDot.Service.Infrastructure;
using PressDot.Contracts.Request.LaboratoryUsers;
using PressDot.Core.Domain;
using PressDot.Core.Exceptions;

namespace PressDot.Facade.Implementation
{
    public class LaboratoryUsersFacade : ILaboratoryUsersFacade
    {
        #region private

        private readonly ILaboratoryUsersService _laboratoryUsersService;

        private readonly ILaboratoryService _laboratoryService;

        private readonly IUsersService _usersService;

        #endregion

        #region ctor

        public LaboratoryUsersFacade(ILaboratoryUsersService laboratoryUsersService,ILaboratoryService laboratoryService,IUsersService usersService)
        {
            _laboratoryUsersService = laboratoryUsersService;
            _laboratoryService = laboratoryService;
            _usersService = usersService;
        }

        #endregion

        #region methods
        public bool CreateLaboratoryUser(CreateLaboratoryUsersRequest request)
        {
            var laboratoryUser = request.ToEntity<LaboratoryUsers>();
            if(_laboratoryUsersService.CheckLaboratoryUserExist(laboratoryUser.LaboratoryId,laboratoryUser.UserId))
                throw new PressDotAlreadyExistException("User is already associated with laboratory.");
            _laboratoryUsersService.CreateLaboratoryUser(laboratoryUser);
            return true;
        }


        public ICollection<LaboratoryUserResponse> GetLaboratoryUsersByLaboratoryId(int laboratoryId)
        {
            var laboratoryUsers = _laboratoryUsersService.GetLaboratoryUsersByLaboratoryId(laboratoryId);

            if (laboratoryUsers == null)
                return null;
            var users = _usersService.Get(laboratoryUsers.Select(x => x.UserId).ToArray());
            foreach (var laboratoryUser in laboratoryUsers)
            {
                laboratoryUser.User = users.FirstOrDefault(x => x.Id == laboratoryUser.UserId);
            }

            return laboratoryUsers.ToModel<LaboratoryUserResponse>();
        }

        public bool DeleteLaboratoryUser(int laboratoryUserId)
        {
            var laboratoryUser = _laboratoryUsersService.Get(laboratoryUserId);
            if(laboratoryUser == null)
                throw new PressDotNotFoundException("Invalid request to delete from Laboratory User.");
            _laboratoryUsersService.Remove(laboratoryUser);
            return true;
        }

        #endregion
    }
}
