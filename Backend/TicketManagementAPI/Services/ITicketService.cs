using TicketManagementAPI.Models;

namespace TicketManagementAPI.Services
{
    public interface ITicketService
    {
        Task<IEnumerable<Ticket>> GetAllTicketsAsync(PaginationFilter filter);
        Task<Ticket> GetTicketByIdAsync(int id);
        Task<Ticket> CreateTicketAsync(Ticket ticketDto);
        Task UpdateTicketAsync(int id, Ticket ticketDto);
        Task DeleteTicketAsync(int id);
    }
}
