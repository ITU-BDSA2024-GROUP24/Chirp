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
    public async Task GoToLogin()
    {
       
    }
    
    [Test]
    public async Task RegisterLoginGoToMytimeline()
    {
        
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