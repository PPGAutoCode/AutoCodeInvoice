
// InvoiceItem.cs
using System;

namespace ProjectName.Types
{
    public class InvoiceItem
    {
        public string Number { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string Unit { get; set; }
        public decimal UnitCost { get; set; }
        public decimal Discount { get; set; }
        public decimal Quantity { get; set; }
    }
}
