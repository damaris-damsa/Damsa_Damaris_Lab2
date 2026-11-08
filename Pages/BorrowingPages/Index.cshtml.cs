using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Damsa_Damaris_Lab2.Models;

namespace Damsa_Damaris_Lab2.Pages.BorrowingPages;

public class IndexModel : PageModel
{
    private readonly Damsa_Damaris_Lab2Context _context;

    public IndexModel(Damsa_Damaris_Lab2Context context)
    {
        _context = context;
    }

    public IList<Borrowing> Borrowing { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Borrowing = await _context.Borrowing
            .Include(b => b.Member)
            .Include(b => b.Book)
            .ToListAsync();
    }

}
