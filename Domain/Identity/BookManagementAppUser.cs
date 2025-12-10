using Domain.DomainModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Identity
{
    public class BookManagementAppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }

        public ShoppingCart? ShoppingCart { get; set; }
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<FavoriteBook>? FavoriteBooks { get; set; }


        // Секој корисник може да има повеќе нарачки (Orders)
        //public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
