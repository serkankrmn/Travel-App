using System.ComponentModel.DataAnnotations;

namespace TravelApp.Models
{
    public class ContactFormModel
    {
        [Required(ErrorMessage = "Bu alan zorunludur")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur")]
        [EmailAddress]
        public string Email { get; set; }

        public string? Description { get; set; }


        public int RelatedEmlakId { get; set; }
    }
}
