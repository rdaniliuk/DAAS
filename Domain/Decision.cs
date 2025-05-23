namespace Domain

{
    public class Decision
    {
        public Guid Id { get; set; }
        public Guid AccessRequestId { get; set; }
        public string ApproverId { get; set; }
        public bool IsApproved { get; set; }
        public string Comment { get; set; }
        public DateTime DecidedAt { get; set; } = DateTime.UtcNow;
        public AccessRequest AccessRequest { get; set; }
    }
}
