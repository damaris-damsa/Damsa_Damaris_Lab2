using Damsa_Damaris_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Damsa_Damaris_Lab2.Pages.BookPages;

public class IndexModel : PageModel
{
    private readonly Damsa_Damaris_Lab2Context _context;

    public IndexModel(Damsa_Damaris_Lab2Context context)
    {
        _context = context;
    }

    public IList<Book> Book { get; set; } = default!;

    public string TitleSort { get; set; } = "";
    public string AuthorSort { get; set; } = "";

    [BindProperty(SupportsGet = true)]
    public string? SearchString { get; set; }


    public async Task OnGetAsync(string? sortOrder)
    {
        TitleSort = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
        AuthorSort = sortOrder == "author" ? "author_desc" : "author";

        IQueryable<Book> books = _context.Book
            .Include(b => b.BookCategories)
            .ThenInclude(bc => bc.Category);

        if (!string.IsNullOrEmpty(SearchString))
        {
            books = books.Where(b =>
                b.Title.Contains(SearchString) ||
                b.Author.Contains(SearchString));
        }

        switch (sortOrder)
        {
            case "title_desc":
                books = books.OrderByDescending(b => b.Title);
                break;
            case "author":
                books = books.OrderBy(b => b.Author);
                break;
            case "author_desc":
                books = books.OrderByDescending(b => b.Author);
                break;
            default:
                books = books.OrderBy(b => b.Title);
                break;
        }

        Book = await books.ToListAsync();

    }
}

