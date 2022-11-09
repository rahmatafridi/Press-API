using PressDot.Contracts.Response.Saloon;
using PressDot.Contracts.Response.State;
using PressDot.Contracts.Response.Users;
using System;

namespace PressDot.Contracts.Response.Appointment
{
    public class AppointmentResponse : BasePressDotEntityModel
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public UsersResponse Customer { get; set; }
        public int? DoctorId { get; set; }
        public UsersResponse Doctor { get; set; }
        public int? SaloonId { get; set; }
        public SaloonResponse Saloon { get; set; }
        public string Symptoms { get; set; }
        public int? StateId { get; set; }
        public StateResponse State { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string StartTimeString { get; set; }
        public string EndTimeString { get; set; }
    }
    public class ProducatResponse
    {
        public string id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public string category { get; set; }
        public string inventoryStatus { get; set; }
        public string image { get; set; }
        public int rating { get; set; }
        public string link { get; set; }
    }
}
