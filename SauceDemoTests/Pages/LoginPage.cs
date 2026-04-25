using Microsoft.Playwright;

namespace SauceDemoTests.Pages;

public class LoginPage
{
    private readonly IPage _page;
    private ILocator _usernameInput => _page.GetByPlaceholder("Username");
    private ILocator _passwordInput => _page.GetByPlaceholder("Password");
    private ILocator _loginButton => _page.GetByRole(AriaRole.Button, new() { Name = "Login" });

    public LoginPage(IPage page) => _page = page;

    public async Task GotoAsync() => await _page.GotoAsync("/");
    public async Task LoginAsync(string user, string pass)
    {
        await _usernameInput.FillAsync(user);
        await _passwordInput.FillAsync(pass);
        await _loginButton.ClickAsync();
    }
}