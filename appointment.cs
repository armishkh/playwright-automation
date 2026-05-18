using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using Allure.NUnit;
using Allure.Net.Commons.Attributes;
using NUnit.Framework;

namespace playwright
{
    [AllureNUnit]
    public class Tests
    {
        IPlaywright playwright=null!;
        IPage page=null!;
        IBrowser playwrightBrowser=null!;

        [Test]
        [AllureStep]
        public async Task DocAppointmentTest()
        {
            // Launch Playwright
            playwright = await Playwright.CreateAsync();

            playwrightBrowser = await playwright.Chromium.LaunchAsync(
                new BrowserTypeLaunchOptions
                {
                    Headless = false
                });

            // Open new page
            page = await playwrightBrowser.NewPageAsync();

            // Open Website
            await page.GotoAsync("https://katalon-demo-cura.herokuapp.com/");

            // Click Make Appointment
            await page.ClickAsync("#btn-make-appointment");

            // Login
            await page.FillAsync("#txt-username", "John Doe");
            await page.FillAsync("#txt-password", "ThisIsNotAPassword");
            await page.ClickAsync("#btn-login");

            // Verify Login Successful
            string actual =
                await page.InnerTextAsync("//h2[contains(text(),'Make Appointment')]");

            Assert.That(actual.Trim(), Is.EqualTo("Make Appointment"));

            // Select Facility
            await page.SelectOptionAsync("#combo_facility", "Tokyo CURA Healthcare Center");

            // Hospital Readmission Checkbox
            await page.CheckAsync("#chk_hospotal_readmission");

            // Select Medicaid Program
            await page.CheckAsync("#radio_program_medicaid");

            // Select Visit Date
            await page.ClickAsync("#txt_visit_date");
            await page.Keyboard.TypeAsync("20/05/2026");

            // Add Comment
            await page.FillAsync(
                "#txt_comment",
                "Appointment booked through Playwright automation");

            // Click Book Appointment
            await page.ClickAsync("#btn-book-appointment");

            // Verify Appointment Confirmation
            string confirmation =
                await page.InnerTextAsync(
                    "//h2[contains(text(),'Appointment Confirmation')]");


            Assert.That(confirmation.Trim(), Is.EqualTo("Appointment Confirmation"));


            // Close Browser
            await playwrightBrowser.CloseAsync();
        }
    }
}
