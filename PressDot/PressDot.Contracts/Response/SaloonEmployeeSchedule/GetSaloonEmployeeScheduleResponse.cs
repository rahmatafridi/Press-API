namespace PressDot.Contracts.Response.SaloonEmployeeSchedule
{
    public class GetSaloonEmployeeScheduleResponse : BasePressDotEntityModel
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public int SaloonId { get; set; }
        public int Day { get; set; }
        public string DayName { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }
    }
}
