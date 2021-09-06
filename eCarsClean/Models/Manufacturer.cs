using System.ComponentModel;

namespace eCarsClean.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        [DisplayName("Manufacturer")]
        public string ManufacturerName { get; set; }
        public string AddressLine1 { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
    }


}