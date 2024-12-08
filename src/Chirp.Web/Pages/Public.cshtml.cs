﻿using System.ComponentModel.DataAnnotations;
using Chirp.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages;

public class PublicModel : PageModel
{
    private readonly ICheepService _service;
    private readonly SignInManager<Author> _signInManager;

    public List<CheepViewModel> Cheeps { get; set; } = new List<CheepViewModel>();

    [Required]
    [StringLength(160, ErrorMessage = "Maximum length is 160 characters.")]
    [MinLength(1, ErrorMessage = "Cheep must be at least 1 characters long.")]
    [Display(Name = "Cheep")]
    [BindProperty]
    public string? Message { get; set; }

    public PublicModel(ICheepService service, SignInManager<Author> signInManager)
    {
        _service = service;
        _signInManager = signInManager;
    }

    public IActionResult OnGet([FromQuery] int page)
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
        if (string.IsNullOrWhiteSpace(Message))
        {
            ModelState.AddModelError("Message", "Cheep cannot be empty.");
            return Page();
        }
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (User.Identity?.Name == null)
        {
            return RedirectToPage("Public");
        }

        var author = await _service.GetAuthorByName(User.Identity.Name);
        if (author == null)
        {
            ModelState.AddModelError(string.Empty, "Author not found.");
            return Page();
        }

        await _service.AddCheep(author, Message!);
        return RedirectToPage("Public", new { page = Request.Query["page"] });
    }
}