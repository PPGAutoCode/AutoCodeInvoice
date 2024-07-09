
// PRCInvoiceDto.cs
using System;
using System.Collections.Generic;

namespace ProjectName.Types
{
    public class PRCInvoiceDto
    {
        public int BuyerId { get; set; }
        public string ExternalId { get; set; }
        public string Reference { get; set; }
        public string DocumentType { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string VesselIMO { get; set; }
        public string VesselName { get; set; }
        public string ExternalVendorId { get; set; }
        public string ExternalVendorName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; }
        public List<PRCInvoiceItemDto> Items { get; set; }
    }
}
