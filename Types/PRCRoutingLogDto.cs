
namespace ProjectName.Types
{
    public class PRCRoutingLogDto
    {
        public Guid PRCRoutingId { get; set; }
        public short Status { get; set; }
        public string Message { get; set; }
        public Guid? PRCAttachmentId { get; set; }
    }
}
