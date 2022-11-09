namespace PressDot.Contracts.Response.Users
{
    public class UsersRegistrationResponse : BasePressDotEntityModel
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }
        
        public bool IsActive { get; set; }
    }
}
