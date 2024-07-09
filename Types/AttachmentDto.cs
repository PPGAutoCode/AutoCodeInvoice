
namespace ProjectName.Types
{
    public class AttachmentDto
    {
        public Guid DocumentId { get; set; }
        public string DocumentType { get; set; }
        public string FileType { get; set; }
        public string FileContent { get; set; }
    }
}
