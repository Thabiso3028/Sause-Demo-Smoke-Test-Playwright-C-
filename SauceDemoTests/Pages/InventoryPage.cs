using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace SauceDemoTests.Pages;

public class InventoryPage
{
    private readonly IPage _page;
    private ILocator _cartBadge => _page.Locator(".shopping_cart_badge");
    private ILocator _cartLink => _page.Locator(".shopping_cart_link");

    public InventoryPage(IPage page) => _page = page;

    public async Task AddItemToCartAsync(string itemName)
    {
        await _page.Locator(".inventory_item")
           .Filter(new() { HasText = itemName })
           .GetByRole(AriaRole.Button, new() { Name = "Add to cart" })
           .ClickAsync();
    }

    public async Task<int> GetCartCountAsync()
    {
        if (!await _cartBadge.IsVisibleAsync()) return 0;
        return int.Parse(await _cartBadge.TextContentAsync() ?? "0");
    }

    public async Task GoToCartAsync() => await _cartLink.ClickAsync();
    public async Task ExpectOnInventoryPage() =>
        await Expect(_page).ToHaveURLAsync("https://www.saucedemo.com/inventory.html");
}