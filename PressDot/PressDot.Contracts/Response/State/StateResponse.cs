namespace PressDot.Contracts.Response.State
{
    public class StateResponse : BasePressDotEntityModel
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string StateFor { get; set; }
    }
}
