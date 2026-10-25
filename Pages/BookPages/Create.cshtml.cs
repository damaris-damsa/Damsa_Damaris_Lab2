using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Damsa_Damaris_Lab2.Models;
using Microsoft.EntityFrameworkCore;

namespace Damsa_Damaris_Lab2.Pages.BookPages;

public class CreateModel : PageModel
{
    private readonly Damsa_Damaris_Lab2Context _context;

    public CreateModel(Damsa_Damaris_Lab2Context context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        Categories = await _context.Category.ToListAsync();
        return Page();
    }

    [BindProperty]
    public Book Book { get; set; } = default!;
    public List<Category> Categories { get; set; } = new List<Category>();

    [BindProperty]
    public List<int> SelectedCategoryIds { get; set; } = new List<int>();

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Categories = await _context.Category.ToListAsync();
            return Page();
        }

        _context.Book.Add(Book);
        await _context.SaveChangesAsync();
        foreach (var categoryId in SelectedCategoryIds)
        {
            _context.BookCategory.Add(new BookCategory
            {
                BookID = Book.ID,
                CategoryID = categoryId
            });
        }

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
