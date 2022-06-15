namespace Alberta.Models
{
    public class Package
    {
        public Guid Id { get; set; }
        public string? SourceAddress { get; set; }
        public string? TargetAddress { get; set; }
        public double? Width { get; set; } = 0;
        public double? Height { get; set; } = 0;
        public double? Length { get; set; } = 0;
        public double? Price { get; set; } = 0;
    }
}