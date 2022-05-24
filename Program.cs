using Azure.Identity;
using AzureAppConfigurationTest;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

var builder = WebApplication.CreateBuilder(args);
// Get endpoint for where to fetch 
var endpoint = builder.Configuration.GetValue<string>("AppConfigEndpoint");
if (endpoint is null) throw new Exception("Missing configuration value for endpoint to AppConfig");
// Add services to the container.
builder.Host.ConfigureAppConfiguration(configurationBuilder =>
{
    configurationBuilder.AddAzureAppConfiguration(options =>
    {
        var credential = new DefaultAzureCredential();
        // Connect using endpoint and Applications managed identity
        options
            .Connect(new Uri(endpoint), credential)
            .Select("TestApp:*", LabelFilter.Null)
            .Select("TestApp:*", builder.Environment.EnvironmentName)
            .ConfigureKeyVault(kv => kv.SetCredential(credential))
            .ConfigureRefresh(refresh => { refresh.Register("TestApp:Sentinel", true); });
    });
});
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("TestApp:Settings"));
builder.Services.AddAzureAppConfiguration();
builder.Services.AddControllers();

var app = builder.Build();

app.UseAzureAppConfiguration();

app.UseAuthorization();

app.MapControllers();

app.Run();
