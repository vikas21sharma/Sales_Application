namespace Sales_Application.DTO
{
    public class ShipperDto
    {
        public string CompanyName { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public int? RoleId { get; set; }
    }
}
