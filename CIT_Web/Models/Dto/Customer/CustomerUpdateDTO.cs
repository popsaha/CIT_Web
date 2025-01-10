using System.ComponentModel.DataAnnotations;

namespace CIT_Web.Models.Dto.Customer
{
    public class CustomerUpdateDTO
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        [MaxLength(30)]
        public string? CustomerName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public int CustomerCode { get; set; }
        public string? ReferenceNo1 { get; set; }
        public string? ReferenceNo2 { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string TaxNumber { get; set; }
    }
}
