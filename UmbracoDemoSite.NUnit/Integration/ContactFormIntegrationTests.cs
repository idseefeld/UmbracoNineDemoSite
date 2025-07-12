using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace UmbracoDemoSite.NUnit.Integration;
[TestFixture]
public class ContactFormIntegrationTests
{
    private HttpClient? _client;
    private WebApplicationFactory<Program>? _factory;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _factory = new WebApplicationFactory<Program>();
        WebApplicationFactoryClientOptions options = new()
        {
            AllowAutoRedirect = false, // We want to test the redirect behavior
            BaseAddress = new Uri("https://localhost:44372/") // Ensure this matches your application's base address
        };
        _client = _factory.CreateClient(options);
    }
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _client?.Dispose();
        _factory?.Dispose();
    }

    [Test]
    public async Task Post_FormModel_BindsCorrectly()
    {
        // Arrange
        var formData = new Dictionary<string, string>
        {
            { "formModel.Name", "John" },
            { "formModel.Email", "john@example.com" },
            { "formModel.Comment", "Hi" }
        };
        var content = new FormUrlEncodedContent(formData);

        // Act
        var response = await _client!.PostAsync("/Contact/", content);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Redirect));
    }
}