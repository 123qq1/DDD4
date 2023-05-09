using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DDD4.App.Pages
{
    public class LinkingKeyModel : PageModel
    {
        [BindProperty]
        public string Key { get; set; }

        public async Task<ActionResult> OnGet(string? key)
        {
            if (key == null) return NotFound();

            Key = key;

            return Page();

        }
    }
}
