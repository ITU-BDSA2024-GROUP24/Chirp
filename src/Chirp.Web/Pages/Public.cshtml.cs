
using Chirp.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Web.Pages.Shared.Model;


namespace Chirp.Web.Pages;

public class PublicModel : PageModel
{
    private readonly ICheepService _service;
    
    private readonly SignInManager<Author> _signInManager;
    required public List<CheepViewModel> Cheeps { get; set; }  = new List<CheepViewModel>();

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
}
