using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using TicketManagementAPI;
using TicketManagementAPI.Models;
using Newtonsoft.Json;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.TestHost;

[TestFixture]
public class TicketsControllerTests
{
    private HttpClient _client;

    [SetUp]
    public void Setup()
    {
        var appFactory = new WebApplicationFactory<Program>();
        _client = appFactory.CreateClient();
    }



    [Test]
    public async Task GetTickets_ShouldReturnOk()
    {
        // Act
        var response = await _client.GetAsync("/api/tickets");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.That(response.Content.Headers.ContentType?.ToString(), Is.EqualTo("application/json; charset=utf-8"));
    }

    [Test]
    public async Task PostTicket_ShouldCreateTicket()
    {
        // Arrange
        var ticket = new Ticket { Description = "New Ticket", Status = Status.Open, DateCreated = DateTime.UtcNow };
        var content = new StringContent(JsonConvert.SerializeObject(ticket), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/tickets", content);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.That(response.Content.Headers.ContentType?.ToString(), Is.EqualTo("application/json; charset=utf-8"));
    }

    [TearDown]
    public void Teardown()
    {
        _client.Dispose();
    }
}
