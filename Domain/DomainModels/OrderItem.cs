using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainModels
{
    public class OrderItem
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }

        [Required]
        public Guid BookId { get; set; }
        public Book? Book { get; set; }

        [Range(1, 99)]
        public int Quantity { get; set; }

        [Range(0, 9999)]
        public double UnitPrice { get; set; } // price at time of order
        public double TotalPrice => UnitPrice * Quantity;
    }
}
