namespace VanRental.Web.Models
{
    public class Van
    {
        public int Id { get; set; }
        public required string Model { get; set; }
        public required string Tipo { get; set; }
        public required string Alimentazione { get; set; }
        public decimal Price { get; set; }
        public required bool IsAvailable { get; set; }
    }
}