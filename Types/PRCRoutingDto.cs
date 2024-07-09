
namespace ProjectName.Types
{
    public class PRCRoutingDto
    {
        public Guid STRServiceId { get; set; }
        public Guid STRTenantIdFrom { get; set; }
        public Guid STRTenantIdTo { get; set; }
        public Guid STREntityId { get; set; }
        public Guid DocumentId { get; set; }
        public short Status { get; set; }
        public List<PRCRoutingLogDto> RoutingLogs { get; set; }
    }
}
