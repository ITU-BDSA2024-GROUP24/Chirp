using Chirp.Core;
using Chirp.Infrastructure;
using Chirp.Web.Pages.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages;

public class Infopage : PageModel
{
    
    private readonly ICheepService _service;
    private readonly SignInManager<Author> _signInManager;
    public required List<CheepViewModel> Cheeps { get; set; } = [];
    public required Task<Author> Author { get; set; }
    
    public Infopage(ICheepService service, SignInManager<Author> signInManager)
    {
        _signInManager = signInManager;
        _service = service;
    }
    
    public ActionResult OnGet(string author, [FromQuery] int page)
    {
        if (page < 1)
        {
            return Redirect($"{Request.Path}?page=1");
        }
        Cheeps = _service.GetCheepsFromAuthor(page, author);

        return Page();
    }
    public IActionResult OnPostMyMethod()
    {
        // Code to handle the button click goes here
        return RedirectToPage();
        
    }
    
}