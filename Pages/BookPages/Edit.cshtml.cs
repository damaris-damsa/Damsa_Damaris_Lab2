using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Damsa_Damaris_Lab2.Models;

namespace Damsa_Damaris_Lab2.Pages.BookPages;

public class EditModel : PageModel
{
    private readonly Damsa_Damaris_Lab2Context _context;

    public EditModel(Damsa_Damaris_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = default!;
    public List<Category> Categories { get; set; } = new List<Category>();

    [BindProperty]
    public List<int> SelectedCategoryIds { get; set; } = new List<int>();

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var book = await _context.Book.FirstOrDefaultAsync(m => m.ID == id);
        if (book is null)
        {
            return NotFound();
        }
        Book = book;
        Categories = await _context.Category.ToListAsync();

        SelectedCategoryIds = await _context.BookCategory
            .Where(bc => bc.BookID == book.ID)
            .Select(bc => bc.CategoryID)
            .ToListAsync();
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Book).State = EntityState.Modified;
        var oldCategories = await _context.BookCategory
    .Where(bc => bc.BookID == Book.ID)
    .ToListAsync();

        _context.BookCategory.RemoveRange(oldCategories);

        foreach (var categoryId in SelectedCategoryIds)
        {
            _context.BookCategory.Add(new BookCategory
            {
                BookID = Book.ID,
                CategoryID = categoryId
            });
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookExists(Book.ID))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool BookExists(int id)
    {
        return _context.Book.Any(e => e.ID == id);
    }
}
