using Domain;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class CreateAccessRequestDto
    {
        [Required(ErrorMessage = "Document ID is required")]
        public string DocumentId { get; set; }

        [Required(ErrorMessage = "UserId is required")]
        public string UserId { get; set; }
        public AccessType RequestedAccess { get; set; }
        public string Reason { get; set; }
    }
}
