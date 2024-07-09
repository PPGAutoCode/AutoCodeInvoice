
namespace ProjectName.Types
{
    public class PRCAttachmentDto
    {
        public Guid DocumentId { get; set; }
        public string AttachmentDesignation { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string FileContent { get; set; }
        public Guid EntityId { get; set; }
    }
}
