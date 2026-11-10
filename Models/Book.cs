using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Damsa_Damaris_Lab2.Models
{
    public class Book
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Titlul cartii este obligatoriu.")]
        [StringLength(150, MinimumLength = 3,
     ErrorMessage = "Titlul trebuie sa contina intre 3 si 150 de caractere.")]
        public string Title { get; set; }
        public string Author { get; set; }
        public int? AuthorID { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("AuthorID")]
        public Author? AuthorDetails { get; set; }

        [Range(typeof(decimal), "0.01", "500")]
        public decimal Price { get; set; }
        [DataType(DataType.Date)]
        public DateTime? PublishingDate { get; set; }
        public int? PublisherID { get; set; }
        public Publisher? Publisher { get; set; }

        public ICollection<BookCategory>? BookCategories { get; set; }

        public ICollection<Borrowing>? Borrowings { get; set; }

    }

}
