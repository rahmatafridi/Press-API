namespace PressDot.Contracts.Request.Saloon
{
    public class SaloonCreateRequest : BasePressDotEntityModel
    {
        public string SaloonName { get; set; }

        public int CountryId { get; set; }

        public int CityId { get; set; }

        public int SaloonTypeId { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
