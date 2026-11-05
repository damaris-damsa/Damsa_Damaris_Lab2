namespace Damsa_Damaris_Lab2.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int? AuthorID { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("AuthorID")]
        public Author? AuthorDetails { get; set; }
        public decimal Price { get; set; }
        public int? PublisherID { get; set; }
        public Publisher? Publisher { get; set; }

        public ICollection<BookCategory>? BookCategories { get; set; }

    }

}
