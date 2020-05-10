using System.ComponentModel.DataAnnotations;

namespace IdentitySystem.API.Models
{
    public class Phone
    {
        public long Id { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public long Number { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public long AreaCode { get; set; }
        [Required]
        public string CountryCode { get; set; }
    }
}