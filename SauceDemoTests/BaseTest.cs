using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace SauceDemoTests;

public class BaseTest : PageTest
{
    public override BrowserNewContextOptions ContextOptions()
    {
        var isCi =
            Environment.GetEnvironmentVariable("CI") == "true";

        return new BrowserNewContextOptions()
        {
            BaseURL = "https://www.saucedemo.com",

            ViewportSize = new()
            {
                Width = 1280,
                Height = 720
            },

            IgnoreHTTPSErrors = true,

            RecordVideoDir = isCi ? "videos/" : null
        };
    }

    [TearDown]
    public async Task Cleanup()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status
           == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            Directory.CreateDirectory("Screenshots");

            var fileName =
            $"{TestContext.CurrentContext.Test.Name}.png";

                await Page.ScreenshotAsync(new()
            {
                Path = $"Screenshots/{fileName}",
                                    FullPage = true
            });
        }
    }
}