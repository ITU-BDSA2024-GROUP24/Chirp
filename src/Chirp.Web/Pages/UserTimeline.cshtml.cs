using Chirp.Core;
using Chirp.Infrastructure.ChirpServices;
using Chirp.Web.Pages.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages;

public class UserTimelineModel : PageModel
{
    private readonly ICheepService _service;
    private readonly SignInManager<Author> _signInManager;
    private readonly IFollowService _followService;
    required public List<CheepViewModel> Cheeps { get; set; } = new List<CheepViewModel>();

    public List<FollowerDTO> Followers { get; set; } = new List<FollowerDTO>();
    public List<FollowerDTO> Following { get; set; } = new List<FollowerDTO>();

    public UserTimelineModel(ICheepService service, SignInManager<Author> signInManager, IFollowService followService)
    {
        _signInManager = signInManager;
        _service = service;
        _followService = followService;

    }

    [BindProperty] public string Message { get; set; }

    [BindProperty] public string FollowedUser { get; set; }

    [BindProperty] public string FollowerUser { get; set; }




    public async Task<IActionResult> OnPostAsync()
    {
        if (!User.Identity!.IsAuthenticated)
        {
            return NotFound();
        }

        Author? author = await _service.GetAuthorByName(User.Identity.Name);
        if (author == null)
        {
            return RedirectToPage("UserTimeline");
        }

        await _service.AddCheep(author, Message ?? throw new NullReferenceException());

        return RedirectToPage("UserTimeline");
    }



    public async Task<IActionResult> OnGetAsync(string author, [FromQuery] int page)
    {
        if (page < 1)
        {
            return Redirect($"{Request.Path}?page=1");
        }
        Cheeps = _service.GetCheepsFromAuthor(page, author);
        Cheeps = _service.GetCheepsFromFollower(page,author);

        if (User.Identity!.IsAuthenticated)
        {
            Author? loggedInUser = await _service.GetAuthorByName(User.Identity.Name);
            if (loggedInUser != null)
            {
                Followers = await _followService.GetFollowers(loggedInUser.UserName) ?? new List<FollowerDTO>();
                Following = await _followService.GetsFollowed(loggedInUser.UserName) ?? new List<FollowerDTO>();
            }
            
        }

        return Page();
    }
  
    

    public async Task<IActionResult> OnPostFollowAsync()
    {
        if (!User.Identity!.IsAuthenticated)
        {
            return NotFound();
        }

        Author? loggedInUser = await _service.GetAuthorByName(User.Identity.Name);
        if (loggedInUser == null || string.IsNullOrEmpty(FollowedUser))
        {
            return RedirectToPage("UserTimeline");
        }

        var existingFollowers = await _followService.GetFollowers(loggedInUser.UserName);
        if (existingFollowers.Any(f => f.Followers == FollowedUser))
        {
            TempData["Message"] = $"You are already following {FollowedUser}.";
            return RedirectToPage("UserTimeline");
        }

        await _followService.AddFollower(loggedInUser.UserName, FollowedUser);
        TempData["Message"] = $"You are now following {FollowedUser}!";

        // Refresh the Following list
        Following = await _followService.GetsFollowed(loggedInUser.UserName) ?? new List<FollowerDTO>();

        return RedirectToPage("UserTimeline");
    }

    public async Task<IActionResult> OnPostUnfollowAsync()
    {
        if (!User.Identity!.IsAuthenticated)
        {
            return NotFound();
        }

        Author? loggedInUser = await _service.GetAuthorByName(User.Identity.Name);
        if (loggedInUser == null || string.IsNullOrEmpty(FollowedUser))
        {
            return RedirectToPage("UserTimeline");
        }

        await _followService.Unfollow(loggedInUser.UserName, FollowedUser);

        // Refresh the Following list
        Following = await _followService.GetsFollowed(loggedInUser.UserName) ?? new List<FollowerDTO>();

        return RedirectToPage("UserTimeline", new { author = FollowedUser });
    }
}