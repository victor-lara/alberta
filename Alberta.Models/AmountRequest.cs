namespace Alberta.Models
{
    public class AmountRequest
    {
        public string Consignee { get; set; }
        public string Consignor { get; set; }
        public List<Package> Cartons { get; set; }
    }
}
