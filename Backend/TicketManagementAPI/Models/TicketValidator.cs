using FluentValidation;

namespace TicketManagementAPI.Models
{
    public class TicketValidator : AbstractValidator<Ticket>
    {
        public TicketValidator()
        {
            RuleFor(ticket => ticket.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(450).WithMessage("Description cannot exceed 450 characters.");

            RuleFor(ticket => ticket.Status)
                .IsInEnum().WithMessage("Status must be either Open or Closed.");

            RuleFor(ticket => ticket.DateCreated)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("DateCreated cannot be in the future.");
        }
    }
}
