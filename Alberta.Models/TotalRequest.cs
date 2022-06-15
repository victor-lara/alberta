namespace Alberta.Models
{
    public class TotalRequest
    {
        public string ContactAddress { get; set; }
        public string WarehouseAddress { get; set; }
        public List<Package> Dimensions { get; set; }
    }
}
