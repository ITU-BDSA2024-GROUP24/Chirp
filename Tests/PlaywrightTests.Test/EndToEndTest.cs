namespace PlaywrightTests.Test;

using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;


[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    [Test]
    public async Task HomepageHasPlaywrightInTitleAndGetStartedLinkLinkingtoTheIntroPage()
    {
        await Page.GotoAsync("https://playwright.dev");

        // Expect a title "to contain" a substring.
        await Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));

        // create a locator
        var getStarted = Page.Locator("text=Get Started");

        // Expect an attribute "to be strictly equal" to the value.
        await Expect(getStarted).ToHaveAttributeAsync("href", "/docs/intro");

        // Click the get started link.
        await getStarted.ClickAsync();

        // Expects the URL to contain intro.
        await Expect(Page).ToHaveURLAsync(new Regex(".*intro"));
    }

    [Test]
    public async Task OpenStartpage()
    {
        await Page.GotoAsync("https://bdsagroup024chirprazor.azurewebsites.net/?page=1");
    }

    [Test]
    public async Task StartpageButtonsClicked()
    {
        await Page.GotoAsync("https://bdsagroup024chirprazor.azurewebsites.net/?page=1");
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "public timeline" })).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "register" })).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "login" })).ToBeVisibleAsync();
    }
    




    [Test]
    public async Task GoToRegister()
    {
        await Page.GotoAsync("https://bdsagroup024chirprazor.azurewebsites.net/?page=1");
        await Page.GetByRole(AriaRole.Link, new() { Name = "register" }).ClickAsync();
    }

    [Test]
    public async Task GoToTestPersonTimeline()
    {
        
    }
    
    [Test]
    public async Task Register()
    {
        await Page.GotoAsync("https://bdsagroup024chirprazor.azurewebsites.net/?page=1");
        await Page.GetByRole(AriaRole.Link, new() { Name = "register" }).ClickAsync();
        await Page.GetByPlaceholder("Name", new() { Exact = true }).ClickAsync();
        await Page.GetByPlaceholder("Name", new() { Exact = true }).FillAsync("Playwright");
        await Page.GetByPlaceholder("Name", new() { Exact = true }).PressAsync("Tab");
        await Page.GetByPlaceholder("name@example.com").FillAsync("p@p.p");
        await Page.GetByPlaceholder("name@example.com").PressAsync("Tab");
        await Page.GetByLabel("Password", new() { Exact = true }).FillAsync("Qwe123?");
        await Page.GetByLabel("Password", new() { Exact = true }).PressAsync("Tab");
        await Page.GetByLabel("Confirm Password").FillAsync("Qwe123?");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "Click here to confirm your" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "public timeline" }).ClickAsync();
    }
    
    [Test]
    public async Task GoToLogin()
    {
        await Page.GotoAsync("https://bdsagroup024chirprazor.azurewebsites.net/?page=1");
        await Page.GetByRole(AriaRole.Link, new() { Name = "login" }).ClickAsync();
    }
    
    [Test]
    public async Task RegisterLoginGoToMytimeline()
    {
        await Page.GotoAsync("https://bdsagroup024chirprazor.azurewebsites.net/?page=1");
        await Page.GetByRole(AriaRole.Link, new() { Name = "register" }).ClickAsync();
        await Page.GetByPlaceholder("Name", new() { Exact = true }).ClickAsync();
        await Page.GetByPlaceholder("Name", new() { Exact = true }).FillAsync("Playwright");
        await Page.GetByPlaceholder("Name", new() { Exact = true }).PressAsync("Tab");
        await Page.GetByPlaceholder("name@example.com").FillAsync("p@p.p");
        await Page.GetByPlaceholder("name@example.com").PressAsync("Tab");
        await Page.GetByLabel("Password", new() { Exact = true }).FillAsync("Qwe123?");
        await Page.GetByLabel("Password", new() { Exact = true }).PressAsync("Tab");
        await Page.GetByLabel("Confirm Password").FillAsync("Qwe123?");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "Click here to confirm your" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "public timeline" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "login" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "public timeline" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "log in" }).ClickAsync();
        await Page.GetByPlaceholder("password").ClickAsync();
        await Page.GetByPlaceholder("password").FillAsync("Qwe123?");
        await Page.GetByPlaceholder("name@example.com").ClickAsync();
        await Page.GetByPlaceholder("name@example.com").FillAsync("p@p.p");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "my timeline" }).ClickAsync();
    }
    
    [Test]
    public async Task PostCheepOnMytimeline()
    {
        
    }
    
    [Test]
    public async Task RegisterLoginCheepLogout()
    {
        
    }
    
    
}