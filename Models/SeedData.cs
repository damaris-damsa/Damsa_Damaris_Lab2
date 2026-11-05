using System.Linq;

namespace Damsa_Damaris_Lab2.Models
{
    public class SeedData
    {
        public static void Initialize(Damsa_Damaris_Lab2Context context)
        {
            // Adaugam editurile daca nu exista deja
            if (!context.Publisher.Any(p => p.PublisherName == "Litera"))
            {
                context.Publisher.Add(new Publisher
                {
                    PublisherName = "Litera"
                });
            }

            if (!context.Publisher.Any(p => p.PublisherName == "Polirom"))
            {
                context.Publisher.Add(new Publisher
                {
                    PublisherName = "Polirom"
                });
            }

            context.SaveChanges();

            // Cautam editurile
            var litera = context.Publisher
                .First(p => p.PublisherName == "Litera");

            var polirom = context.Publisher
                .First(p => p.PublisherName == "Polirom");

            // Asociem cartile existente cu editurile
            var ion = context.Book.FirstOrDefault(b => b.Title == "Ion");

            if (ion != null && ion.PublisherID == null)
            {
                ion.PublisherID = litera.ID;
            }

            var gatsby = context.Book
                .FirstOrDefault(b => b.Title == "The Great Gatsby");

            if (gatsby != null && gatsby.PublisherID == null)
            {
                gatsby.PublisherID = polirom.ID;
            }

            context.SaveChanges();

            var rebreanu = context.Author
                .FirstOrDefault(a => a.FirstName == "Liviu"
                                  && a.LastName == "Rebreanu");

            if (rebreanu == null)
            {
                rebreanu = new Author
                {
                    FirstName = "Liviu",
                    LastName = "Rebreanu"
                };

                context.Author.Add(rebreanu);
            }

            var fitzgerald = context.Author
                .FirstOrDefault(a => a.FirstName == "F. Scott"
                                  && a.LastName == "Fitzgerald");

            if (fitzgerald == null)
            {
                fitzgerald = new Author
                {
                    FirstName = "F. Scott",
                    LastName = "Fitzgerald"
                };

                context.Author.Add(fitzgerald);
            }

            context.SaveChanges();

            if (ion != null)
            {
                ion.AuthorID = rebreanu.ID;
            }

            if (gatsby != null)
            {
                gatsby.AuthorID = fitzgerald.ID;
            }

            context.SaveChanges();

        }
    }
}
