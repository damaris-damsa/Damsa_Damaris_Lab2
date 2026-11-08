using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Damsa_Damaris_Lab2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Damsa_Damaris_Lab2.Pages.BorrowingPages;

public class CreateModel : PageModel
{
    private readonly Damsa_Damaris_Lab2Context _context;

    public CreateModel(Damsa_Damaris_Lab2Context context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        ViewData["MemberID"] = new SelectList(
            _context.Member
                .Select(m => new
                {
                    m.ID,
                    FullName = m.FirstName + " " + m.LastName
                }),
            "ID",
            "FullName"
        );

        ViewData["BookID"] = new SelectList(
            _context.Book,
            "ID",
            "Title"
        );

        return Page();
    }

    [BindProperty]
    public Borrowing Borrowing { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Borrowing.Add(Borrowing);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
