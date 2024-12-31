using System.ComponentModel.DataAnnotations;

namespace CIT_Web.Models.Dto.Police
{
    public class PoliceCreateDTO
    {
        public string Name { get; set; }
        [Required(ErrorMessage = "Contact Number is required.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Contact Number must be numeric.")]
        public string ContactNumber { get; set; }
        public string Address { get; set; }
       
    }
}
