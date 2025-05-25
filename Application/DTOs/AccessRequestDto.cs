using Domain;

namespace Application.DTOs
{
    public class AccessRequestDto
    {
        public Guid Id { get; set; }
        public string DocumentId { get; set; }
        public string UserId { get; set; }
        public AccessType RequestedAccess { get; set; }
        public string Reason { get; set; }
        public RequestStatus Status { get; set; }
        public DecisionDto Decision { get; set; }
    }
}
