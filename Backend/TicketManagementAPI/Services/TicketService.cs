using TicketManagementAPI.Exceptions;
using TicketManagementAPI.Models;
using TicketManagementAPI.Repositories;

namespace TicketManagementAPI.Services
{
    public class TicketService(ITicketRepository ticketRepository, ILogger<TicketService> logger) : ITicketService
    {
        private readonly ITicketRepository _ticketRepository = ticketRepository;
        private readonly ILogger<TicketService> _logger = logger;

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync(PaginationFilter filter)
        {
            try
            {
                return await _ticketRepository.GetAllTicketsAsync(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all tickets");
                throw;
            }
        }

        public async Task<Ticket> GetTicketByIdAsync(int id)
        {
            try
            {
                return await _ticketRepository.GetTicketByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving ticket with ID {id}");
                throw;
            }
        }

        public async Task<Ticket> CreateTicketAsync(Ticket ticket)
        {
            try
            {
                await _ticketRepository.AddTicketAsync(ticket);
                return ticket;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a new ticket");
                throw;
            }
        }

        public async Task UpdateTicketAsync(int id, Ticket ticket)
        {
            var existingTicket = await _ticketRepository.GetTicketByIdAsync(id)
                            ?? throw new NotFoundException();

            existingTicket.Description = ticket.Description;
            existingTicket.Status = ticket.Status;

            await _ticketRepository.UpdateTicketAsync(existingTicket);
        }

        public async Task DeleteTicketAsync(int id)
        {
            try
            {
                await _ticketRepository.DeleteTicketAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting ticket with ID {id}");
                throw;
            }
        }
    }
}
