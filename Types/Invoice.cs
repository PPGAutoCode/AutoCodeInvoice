
// Invoice.cs
using System;
using System.Collections.Generic;

namespace ProjectName.Types
{
    public class Invoice
    {
        public string BuyerId { get; set; }
        public int PurchaseInvoiceId { get; set; }
        public string Number { get; set; }
        public string DocumentType { get; set; }
        public string OrderNumber { get; set; }
        public string VendorName { get; set; }
        public string VesselIMO { get; set; }
        public string VesselName { get; set; }
        public string InvoiceDate { get; set; }
        public decimal InvoiceAmount { get; set; }
        public string DueDate { get; set; }
        public string Currency { get; set; }
        public List<int> FileIds { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
    }
}
