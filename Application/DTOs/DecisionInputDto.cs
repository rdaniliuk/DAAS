using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class DecisionInput
    {
        [Required(ErrorMessage = "Approver ID is required")]
        public string ApproverId { get; set; }
        public bool IsApproved { get; set; }
        public string Comment { get; set; }
    }
}
