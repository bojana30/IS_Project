using Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainModels
{
    public class Order : BaseEntity
    {
        [Required]
        public string? UserId { get; set; }               // FK to LibraryUser (Identity)
        public BookManagementAppUser? User { get; set; }

        public DateTime CreatedAt { get; set; }
        public OrderStatus Status { get; set; }         // Pending, Paid, Shipped, Cancelled, etc.

        public double TotalAmount { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public Payment? Payment { get; set; }
    }

    public enum OrderStatus
    {
        Pending = 0,
        Paid = 1,
        Shipped = 2,
        Completed = 3,
        Cancelled = 4
    }
}

