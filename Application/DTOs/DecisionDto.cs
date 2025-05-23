namespace Application.DTOs
{
    public class DecisionDto
    {
        public bool IsApproved { get; set; }
        public string Comment { get; set; }
        public DateTime DecidedAt { get; set; }
    }
}
