namespace Domain;

public class AccessRequest
{
    public Guid Id { get; set; }
    public string DocumentId { get; set; }
    public string UserId { get; set; }
    public AccessType RequestedAccess { get; set; }
    public string Reason { get; set; }
    public RequestStatus Status { get; set; } = RequestStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Decision Decision { get; set; }
}

public enum AccessType { Read, Edit }
public enum RequestStatus { Pending, Approved, Rejected }