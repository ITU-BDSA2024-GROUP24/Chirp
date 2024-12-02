using Chirp.Core;
using Chirp.Web.Pages.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages;

public class UserTimelineModel : PageModel
{
    private readonly ICheepService _service;
    private readonly SignInManager<Author> _signInManager;
    required public List<CheepViewModel> Cheeps { get; set; } = new List<CheepViewModel>();

    [BindProperty] public CheepFormatMessage Input { get; set; } = new();

    public UserTimelineModel(ICheepService service, SignInManager<Author> signInManager)
    {
        _signInManager = signInManager;
        _service = service;
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
            Author? author = await _signInManager.UserManager.GetUserAsync(User);
            if (author == null)
            {
                return Forbid("Please sign in");
            }

            _service.AddCheep(author, Input.Message ?? throw new NullReferenceException());
        }

        // missing query 
        return RedirectToPage("page=1");
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
}

