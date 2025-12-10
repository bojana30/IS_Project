using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainModels
{
    public class BookInShoppingCart
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid BookId { get; set; }
        public Book? Book { get; set; }
        [Required]

        public Guid ShoppingCartId { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
        [Range(1, 99)]
        public int Quantity { get; set; }
    }
}
