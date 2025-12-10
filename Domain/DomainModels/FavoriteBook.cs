using Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainModels
{
    public class FavoriteBook : BaseEntity
    {
        [Required]
        public string? UserId { get; set; }
        public BookManagementAppUser? User { get; set; }

        [Required]
        public Guid BookId { get; set; }
        public Book? Book { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
