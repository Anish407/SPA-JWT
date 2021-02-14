namespace Core5Ng.Models.Entities
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Country { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Unit { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Type { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
