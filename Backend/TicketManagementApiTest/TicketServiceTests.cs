using Moq;
using TicketManagementAPI.Services;
using TicketManagementAPI.Models;
using TicketManagementAPI.Repositories;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

[TestFixture]
public class TicketServiceTests
{
    private Mock<ITicketRepository> _ticketRepositoryMock;
    private Mock<ILogger<TicketService>> _loggerMock;
    private ITicketService _ticketService;

    [SetUp]
    public void Setup()
    {
        _ticketRepositoryMock = new Mock<ITicketRepository>();
        _loggerMock = new Mock<ILogger<TicketService>>();
        _ticketService = new TicketService(_ticketRepositoryMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetAllTicketsAsync_ShouldReturnTickets()
    {
        // Arrange
        var tickets = new List<Ticket> { new Ticket { TicketId = 1, Description = "Test Ticket", Status = Status.Open, DateCreated = DateTime.UtcNow } };
        _ticketRepositoryMock.Setup(repo => repo.GetAllTicketsAsync(It.IsAny<PaginationFilter>()))
                            .ReturnsAsync(tickets);

        // Act
        var result = await _ticketService.GetAllTicketsAsync(new PaginationFilter());

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(1));
    }

    [Test]
    public async Task GetTicketByIdAsync_ShouldReturnTicket_WhenTicketExists()
    {
        // Arrange
        var ticket = new Ticket { TicketId = 1, Description = "Test Ticket", Status = Status.Open, DateCreated = DateTime.UtcNow };
        _ticketRepositoryMock.Setup(repo => repo.GetTicketByIdAsync(1)).ReturnsAsync(ticket);

        // Act
        var result = await _ticketService.GetTicketByIdAsync(1);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.TicketId, Is.EqualTo(1));
    }
}
