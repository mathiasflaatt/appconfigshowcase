using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AzureAppConfigurationTest.Controllers;

[ApiController]
[Route("[controller]")]
public class SettingsController : ControllerBase
{
    private readonly AppSettings _settings;
    public SettingsController(IOptionsSnapshot<AppSettings> settings)
    {
        _settings = settings.Value;
    }
    
    
    // GET
    public IActionResult Index()
    {
        return Ok(_settings);
    }
}