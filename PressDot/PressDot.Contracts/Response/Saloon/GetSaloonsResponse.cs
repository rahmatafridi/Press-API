using PressDot.Contracts.Response.Location;

namespace PressDot.Contracts.Response.Saloon
{
    public class GetSaloonsResponse : BasePressDotEntityModel
    {
        public int Id { get; set; }

        public string SaloonName { get; set; }

        public int CountryId { get; set; }

        public int CityId { get; set; }

        public int SaloonTypeId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public LocationResponse Country { get; set; }

        public LocationResponse City { get; set; }

        public SaloonTypeResponse SaloonType { get; set; }
    }
}
