
// InvoiceAttachment.cs
using System;

namespace ProjectName.Types
{
    public class InvoiceAttachment
    {
        public Guid Id { get; set; }
        public Guid InvoiceId { get; set; }
        public Guid FileId { get; set; }
        public bool IsReceived { get; set; }
    }
}
