using Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        // FK to User
        [Required]

        public string UserId { get; set; }
        public BookManagementAppUser? User { get; set; }

        // Many-to-many relation
        public virtual ICollection<BookInShoppingCart>? BookInShoppingCarts { get; set; }
    }
}
