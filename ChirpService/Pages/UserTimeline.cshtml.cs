using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Razor.Pages;

public class UserTimelineModel : PageModel
{
    private readonly ICheepService _service;
    public List<CheepViewModel> Cheeps { get; set; }

    public UserTimelineModel(ICheepService service)
    {
        _service = service;
    }

    public ActionResult OnGet(string author, [FromQuery] int page)
    {
        page = page > 0 ? page - 1 : 1;
        int pagesize = 32;
        int skip = (page - 1) * pagesize;
        //Cheeps = _service.GetCheeps(skip,pagesize); // to determine how many should be skipped for the pages
        Cheeps = _service.GetCheepsFromAuthor(skip, author);
        return Page();
    }
}
