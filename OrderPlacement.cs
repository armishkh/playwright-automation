using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using Allure.NUnit;
using Allure.Net.Commons.Attributes;
using NUnit.Framework;
namespace PlaywrightSauceDemo
{
    [AllureNUnit]
    public class Tests
    {
        IPlaywright playwright=null!;
        IPage page=null!;
        IBrowser browser=null!;
        [Test]
        [AllureStep]
        public async Task PlacingOrder()
        {
            //launching browser
            playwright = await Playwright.CreateAsync();
            browser = await playwright.Chromium.LaunchAsync(
                new BrowserTypeLaunchOptions
                {
                    Headless = false
                });
            //new page
            page = await browser.NewPageAsync();
            //website
            await page.GotoAsync("https://www.saucedemo.com/");
            //logging in
            await page.FillAsync("#user-name", "standard_user");
            await page.FillAsync("#password", "secret_sauce");
            await page.ClickAsync("#login-button");
            //adding product to cart
            await page.ClickAsync("#add-to-cart-sauce-labs-backpack");
            //going to cart
            await page.ClickAsync("//a[@class='shopping_cart_link']");
            //checkout
            await page.ClickAsync("#checkout");
            //order details
            await page.FillAsync("#first-name", "ABC");
            await page.FillAsync("#last-name", "XYZ");
            await page.FillAsync("#postal-code", "0000");
            //placing order
            await page.ClickAsync("#continue");
            await page.ClickAsync("#finish");
            //confirmation
            string Text = await page.InnerTextAsync("//h2[@class='complete-header']");
            Assert.That(Text.Trim(),Is.EqualTo("Thank you for your order!"));

        }
       
    }
}
