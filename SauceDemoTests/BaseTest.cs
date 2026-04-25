using Microsoft.Playwright.NUnit;

namespace SauceDemoTests;

public class BaseTest : PageTest
{
    public override BrowserNewContextOptions ContextOptions()
    {
        return new BrowserNewContextOptions()
        {
            BaseURL = "https://www.saucedemo.com",
            ViewportSize = new() { Width = 1280, Height = 720 },
            RecordVideoDir = "videos/",
        };
    }
}