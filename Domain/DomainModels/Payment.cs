using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainModels
{
    public class Payment
    {
        [Key]
        public Guid Id { get; set; }

        public PaymentStatus Status { get; set; }
        public PaymentMethod Method { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relations
        [Required]
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
    }
    public enum PaymentStatus
    {
        Pending = 0,
        Paid = 1,
        Failed = 2
    }

    public enum PaymentMethod
    {
        CreditCard = 0,
        CashOnDelivery = 1
    }
}
