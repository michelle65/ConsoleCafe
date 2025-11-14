namespace ConsoleCafe.Application.Dtos
{
    public class OrderResultDto
    {
        public Guid OrderId { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal Subtotal { get; set; }

        public decimal Total { get; set; }
    }
}
