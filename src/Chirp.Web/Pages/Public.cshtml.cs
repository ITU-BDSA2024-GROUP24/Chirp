
using Chirp.Core;
using Chirp.Infrastructure.ChirpServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Web.Pages.Shared;
using Microsoft.CodeAnalysis.CSharp.Syntax;



namespace Chirp.Web.Pages;

public class PublicModel : PageModel
{
    private readonly ICheepService _service;
    
    private readonly SignInManager<Author> _signInManager;
    
    private readonly IFollowService _followService;

    public List<CheepViewModel> Cheeps { get; set; }  = new List<CheepViewModel>();
    public List<FollowerDTO> Following { get; set; } = new List<FollowerDTO>();

    [BindProperty] 
    public string? Message { get; set; }
    public CheepFormatMessage CheepMessage { get; set; } = new CheepFormatMessage();

    public PublicModel(ICheepService service, IFollowService followService, SignInManager<Author> signInManager)
    {
        _service = service;
        _followService = followService;
        _signInManager = signInManager;
    }

    public async Task <ActionResult> OnGetAsync([FromQuery] int page)
    {
        if (page < 1)
        {
            return Redirect($"{Request.Path}?page=1");
        }
        
        Cheeps = _service.GetCheeps(page);

        if (User.Identity.IsAuthenticated)
        {
            var loggedInUser = User.Identity.Name;
            Following = await _followService.GetsFollowed(loggedInUser)?? new List<FollowerDTO>();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return NotFound();
        }
        else
        {
            Author author = await _service.GetAuthorByName(User.Identity.Name);
            if( author == null)
            {
                return RedirectToPage("Public");
            }
            await _service.AddCheep(author, Message ?? throw new NullReferenceException());
        }
        // missing query 
        return RedirectToPage("Public");
    }
}
