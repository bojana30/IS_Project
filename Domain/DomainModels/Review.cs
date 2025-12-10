using Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainModels
{
    public class Review : BaseEntity
    {
        [Range(1, 5)]
        public int Rating { get; set; } // 1-5
        [StringLength(500)]
        public string? Comment { get; set; }

        // Relations
        [Required]
        public Guid BookId { get; set; }
        public Book? Book { get; set; }

        [Required]
        public string? UserId { get; set; }
        public BookManagementAppUser? User { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
