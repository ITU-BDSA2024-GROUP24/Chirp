using Chirp.Core;
using Chirp.Infrastructure;
using Chirp.Infrastructure.ChirpServices;
using Chirp.Web.Pages.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Chirp.Web.Pages;

public class Infopage : PageModel
{
    
    private readonly ICheepService _service;
    private readonly SignInManager<Author> _signInManager;
    required public List<CheepViewModel> Cheeps { get; set; } = new List<CheepViewModel>();
    public required Task<Author> Author { get; set; }
    
    public Infopage(ICheepService service, SignInManager<Author> signInManager)
    {
        _signInManager = signInManager;
        _service = service;
    }
    
    [BindProperty]
    public string Message { get; set; }
    
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!User.Identity!.IsAuthenticated)
        {
            return NotFound();
        }
        
        Author? author = await _service.GetAuthorByName(User.Identity.Name);
        if (author == null)
        {
            return RedirectToPage("Aboutme");
        }

        await _service.AddCheep(author, Message ?? throw new NullReferenceException());

        return RedirectToPage("Aboutme");
    }
    
    public ActionResult OnGet(string author, [FromQuery] int page)
    {
        if (page < 1)
        {
            return Redirect($"{Request.Path}/?page=1");
        }
        Cheeps = _service.GetCheepsFromAuthor(page, author);

        return Page();
    }
    
    
}