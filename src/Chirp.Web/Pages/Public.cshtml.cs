
using Chirp.Core;
using Chirp.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Web.Pages.Shared.Model;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace Chirp.Web.Pages;

public class PublicModel : PageModel
{
    private readonly ICheepService _service;
    
    private readonly SignInManager<Author> _signInManager;
    public List<CheepViewModel> Cheeps { get; set; }  = new List<CheepViewModel>();

    [BindProperty] public CheepFormatMessage Input { get; set; } = new();
    
    
    
    
    public PublicModel(ICheepService service, SignInManager<Author> signInManager)
    
    {
        _service = service;
        _signInManager = signInManager;
    }

    public ActionResult OnGet([FromQuery] int page)
    {
        if (page < 1)
        {
            return Redirect($"{Request.Path}?page=1");
        }
        

        Cheeps = _service.GetCheeps(page);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            //missing query
            return Page();
        }
        else
        {
            Author ? author = await _signInManager.UserManager.GetUserAsync(User);
            if( author == null)
            {
                return Forbid("Please sign in");
            }
            _service.AddCheep(author, Input.Message ?? throw new NullReferenceException());
        }
        // missing query 
        return RedirectToPage("page=1");
    }
}
