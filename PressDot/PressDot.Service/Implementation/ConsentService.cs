using PressDot.Contracts.Request.Contsent;
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
    public class ConsentService : BaseService<Consent>, IConsentService
    {
        private readonly IEmailService _emailService;
        private readonly IUsersService _usersService;
        private readonly IUserRoleService _userRoleService;
        public ConsentService(IUserRoleService userRoleService,IUsersService usersService,IEmailService emailService,IRepository<Consent> repository) : base(repository)
        {
            _emailService = emailService;
            _usersService = usersService;
            _userRoleService = userRoleService;
        }

        public Consent CreateConsent(Consent consent)
        {
            if (consent != null)
            {
                Create(consent);
                return consent;
            }
            return null;
        }

        public Consent GetConsentById(int id)
        {
            return Repository.Table.FirstOrDefault(x =>x.UserId==id && x.IsSent !=true);
        }

        public bool UpdateConsent(UpdateConsentRequest request)
        {
            if (request == null) throw new PressDotException("Invalid request to update user information.");
            //var userModel = request.ToEntity<Consent>();

            var consent = GetConsentById(request.UserId);
            if (consent == null)
                throw new PressDotException("User record does not exist in system.");
            var user = _usersService.Get(request.UserId);
            var role = _userRoleService.GetRoleByName("Saloon Administrator");
            var adminUser = _usersService.GetUsersByRoleId(role.Id);
            _emailService.SendEmail("PressDots Consent", "Dear Your Doctor is "+request.note+"", user.Email);
           _emailService.SendEmail("PressDots Consent", "The User "+user.Firstname + " "+ user.Lastname +" Doctor is " + request.note + "", adminUser[0].Email);

            consent.IsSent = true;
            consent.UpdatedDate = DateTime.Now;
            return Update(consent);
        }
    }
}
