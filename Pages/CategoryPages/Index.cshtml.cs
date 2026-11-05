using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Damsa_Damaris_Lab2.Models;

namespace Damsa_Damaris_Lab2.Pages.CategoryPages;

public class IndexModel : PageModel
{
    private readonly Damsa_Damaris_Lab2Context _context;

    public IndexModel(Damsa_Damaris_Lab2Context context)
    {
        _context = context;
    }

    public IList<Category> Category { get; set; } = default!;

    public int CategoryID { get; set; }

    public IEnumerable<Book> Books { get; set; } = new List<Book>();

    public async Task OnGetAsync(int? id)
    {
        Category = await _context.Category
            .Include(c => c.BookCategories)
            .ThenInclude(bc => bc.Book)
            .ToListAsync();

        if (id != null)
        {
            CategoryID = id.Value;

            var selectedCategory = Category
                .FirstOrDefault(c => c.ID == id.Value);

            if (selectedCategory != null)
            {
                Books = selectedCategory.BookCategories!
                    .Select(bc => bc.Book)
                    .ToList();
            }
        }
    }

}
