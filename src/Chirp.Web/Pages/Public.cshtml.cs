
using System.Diagnostics;
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
    public List<FollowerDto> Following { get; set; } = new List<FollowerDto>();

    [BindProperty] 
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

        Debug.Assert(User.Identity != null, "User.Identity != null");
        if (User.Identity.IsAuthenticated)
        {
            var loggedInUser = User.Identity.Name;
            Debug.Assert(loggedInUser != null, nameof(loggedInUser) + " != null");
            Following = await _followService.GetsFollowed(loggedInUser)?? new List<FollowerDto>();
        }

        return Page();
    }
    
    

    public async Task<IActionResult> OnPostAsync()
    {
        {
            if (string.IsNullOrWhiteSpace(CheepMessage.Message))
            {
                ModelState.AddModelError("Message", "Cheep cannot be empty.");
                return Page();
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Debug.Assert(User.Identity != null, "User.Identity != null");
            Debug.Assert(User.Identity.Name != null, "User.Identity.Name != null");
            Author author = await _service.GetAuthorByName(User.Identity.Name);
            await _service.AddCheep(author, CheepMessage.Message  ?? throw new NullReferenceException());
        }
        return RedirectToPage("Public");
    }

    public async Task<IActionResult> OnPostFollowAsync(string followedUser)
    {
        if (!User.Identity?.IsAuthenticated ?? true || string.IsNullOrEmpty(followedUser))
        {
            return RedirectToPage();
        }
        var loggedInUser = User.Identity.Name;

        Debug.Assert(loggedInUser != null, nameof(loggedInUser) + " != null");
        var existingFollowers = await _followService.GetFollowers(loggedInUser);
        if (existingFollowers.Any(f => f.Followers == followedUser))
        {
            await _followService.Unfollow(loggedInUser, followedUser);
            TempData["Message"] = $"You have unfollowed {followedUser}.";
        }
        else
        {
            await _followService.AddFollower(loggedInUser, followedUser);
            TempData["Message"] = $"YAY!You are now following {followedUser}! - To unfollow go to my timeline";
        }

        return RedirectToPage();
    }
}
