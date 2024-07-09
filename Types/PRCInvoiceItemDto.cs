
// PRCInvoiceItemDto.cs
using System;

namespace ProjectName.Types
{
    public class PRCInvoiceItemDto
    {
        public Guid PRCInvoiceId { get; set; }
        public string ExternalId { get; set; }
        public string VesselIMO { get; set; }
        public string VesselName { get; set; }
        public string ItemNumber { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string MeasurementUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? Discount { get; set; }
        public decimal Quantity { get; set; }
    }
}
