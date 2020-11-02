namespace DataLayer.Entities
{
    public class Country : BaseEntity
    {
        public string Iso { get; set; }
        public string Name { get; set; }
        public string NiceName { get; set; }
        public string Iso3 { get; set; }
        public short NumCode { get; set; }
        public int PhoneCode { get; set; }
    }
}
