using SauceDemoTests.Pages;

namespace SauceDemoTests.Tests;

public class CheckoutTests : BaseTest
{

    public async Task User_Can_Complete_Checkout_Flow()
    {
        var loginPage = new LoginPage(Page);
        var inventoryPage = new InventoryPage(Page);

        await loginPage.GotoAsync();
        await loginPage.LoginAsync("standard_user", "secret_sauce");

        await inventoryPage.ExpectOnInventoryPage();
        await inventoryPage.AddItemToCartAsync("Sauce Labs Backpack");
        await inventoryPage.AddItemToCartAsync("Sauce Labs Bike Light");

        Assert.That(await inventoryPage.GetCartCountAsync(), Is.EqualTo(2));
        await inventoryPage.GoToCartAsync();
        await Expect(Page).ToHaveURLAsync("**/cart.html");
        await Expect(Page.GetByText("Sauce Labs Backpack")).ToBeVisibleAsync();
    }

    public async Task Locked_User_Shows_Error()
    {
        var loginPage = new LoginPage(Page);
        await loginPage.GotoAsync();
        await loginPage.LoginAsync("locked_out_user", "secret_sauce");
        await Expect(Page.GetByText("Sorry, this user has been locked out.")).ToBeVisibleAsync();
    }
}