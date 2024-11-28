using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CharityGame_Management.Pages
{
    public class FilterDataModel : PageModel
    {
        [BindProperty]
        public int MinStrength { get; set; }
        [BindProperty]
        public int MaxStrength { get; set; }
        [BindProperty]
        public int MinSpeed { get; set; }
        [BindProperty]
        public int MaxSpeed { get; set; }
        [BindProperty]
        public string SortOption { get; set; }

        public IActionResult OnPost()
        {
            TempData["MinStrength"] = MinStrength;
            TempData["MaxStrength"] = MaxStrength;
            TempData["MinSpeed"] = MinSpeed;
            TempData["MaxSpeed"] = MaxSpeed;

            if (SortOption == "dontSort")
            {
                TempData["Sorting"] = 0;
            }
            else if (SortOption == "sortAscending")
            {
                TempData["Sorting"] = 1;
            }
            else if (SortOption == "sortDescending")
            {
                TempData["Sorting"] = -1;
            }

            return RedirectToPage("/Index");
        }
    }
}
