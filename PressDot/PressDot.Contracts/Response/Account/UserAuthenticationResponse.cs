namespace PressDot.Contracts.Response.Account
{
    public class UserAuthenticationResponse
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public int UserRoleId { get; set; }

        public string UserRole { get; set; }
        public string Token { get; set; }
    }
}
