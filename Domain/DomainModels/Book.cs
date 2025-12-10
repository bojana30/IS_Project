using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainModels
{
    public class Book : BaseEntity 
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
        [Required]
        public string Genre { get; set; }
        [Range(1, 9999)]
        public int YearPublished { get; set; }

        public string? ImageUrl { get; set; }
        [Range(0, 9999)]
        public double Price { get; set; }

        // Relations
        public virtual ICollection<BookInShoppingCart>? BookInShoppingCarts { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<FavoriteBook>? FavoriteUsers { get; set; }

    }
}
