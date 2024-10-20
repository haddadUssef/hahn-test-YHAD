using Microsoft.EntityFrameworkCore;
using TicketManagementAPI.Models;

namespace TicketManagementAPI.Repositories
{
    public class TicketRepository(TicketContext context) : ITicketRepository
    {
        private readonly TicketContext _context = context;

        public async Task AddTicketAsync(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteTicketAsync(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id).ConfigureAwait(false);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync(PaginationFilter filter)
        {
            return await _context.Tickets
                .AsNoTracking()
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Ticket> GetTicketByIdAsync(int id)
        {
            var ticket = await _context.Tickets.AsNoTracking()
                .FirstOrDefaultAsync(t => t.TicketId == id)
                .ConfigureAwait(false);

            return ticket ?? throw new InvalidOperationException($"Ticket with ID {id} not found.");
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
