namespace TicketManagementAPI.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public required string Description { get; set; } = string.Empty;
        public required Status Status { get; set; }
        public required DateTime DateCreated { get; set; }
    }
}