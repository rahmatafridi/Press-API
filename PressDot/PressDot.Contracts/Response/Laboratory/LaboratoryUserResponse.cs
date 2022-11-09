using PressDot.Contracts.Response.Users;

namespace PressDot.Contracts.Response.Laboratory
{
    public class LaboratoryUserResponse : BasePressDotEntityModel
    {
        public int Id { get; set; }

        public int LaboratoryId { get; set; }

        public int UserId { get; set; }

        public bool IsEmailReceiver { get; set; }

        public LaboratoryResponse Laboratory { get; set; }

        public UsersResponse User { get; set; }
    }
}
