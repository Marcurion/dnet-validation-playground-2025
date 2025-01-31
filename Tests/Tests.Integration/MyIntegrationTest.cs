namespace Tests.Integration;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

public class MyIntegrationTests : IClassFixture<MyCustomWebFactory>
{
    private readonly HttpClient _client;
    private const string HomepageEndpoint = "/weatherforecast";

    public MyIntegrationTests(MyCustomWebFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Test_HomePage_ReturnsSuccess()
    {
        var response = await _client.GetAsync(HomepageEndpoint);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Test_HomePage_ContainsTemperature()
    {
        var response = await _client.GetAsync(HomepageEndpoint);
        // Read response content as string
        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("Temperature", content, StringComparison.CurrentCulture);
    }
    
    [Fact]
    public async Task Test_ServiceInjection_Works()
    {
        var response = await _client.GetAsync(HomepageEndpoint);
        // Read response content as string
        var content = await response.Content.ReadAsStringAsync();
        // MyCustomWebFactory replaces the MyRepository implementation which throws a
        // NotImplemented Exception with a Substitute, therefore there is no error output anymore
        Assert.DoesNotContain("NotImplemented", content, StringComparison.CurrentCulture);
    }
}