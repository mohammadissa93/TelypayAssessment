using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Product : AuditableWithBaseEntity<int>
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public long Quantity { get; set; }
        public double Price { get; set; }
        [MaxLength(25)]
        public string Currency { get; set; }
        public int MinPurchaseQty { get; set; }
        public int MaxPurchaseQty { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
