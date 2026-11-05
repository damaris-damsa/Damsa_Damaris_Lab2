using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Damsa_Damaris_Lab2.Models;
using Damsa_Damaris_Lab2.ViewModels;

namespace Damsa_Damaris_Lab2.Pages.PublisherPages
{
    public class IndexModel : PageModel
    {
        private readonly Damsa_Damaris_Lab2Context _context;

        public IndexModel(Damsa_Damaris_Lab2Context context)
        {
            _context = context;
        }

        public PublisherIndexData PublisherData { get; set; } = new PublisherIndexData();

        public int PublisherID { get; set; }

        public async Task OnGetAsync(int? id)
        {
            PublisherData = new PublisherIndexData();

            PublisherData.Publishers = await _context.Publisher
                .Include(p => p.Books)
                    .ThenInclude(b => b.AuthorDetails)
                .OrderBy(p => p.PublisherName)
                .ToListAsync();

            if (id != null)
            {
                PublisherID = id.Value;

                var selectedPublisher = PublisherData.Publishers
                    .Where(p => p.ID == id.Value)
                    .SingleOrDefault();

                if (selectedPublisher != null)
                {
                    PublisherData.Books = selectedPublisher.Books ?? new List<Book>();
                }
            }
        }
    }
}
