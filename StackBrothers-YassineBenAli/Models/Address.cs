using SQLitePCL;
using System.ComponentModel.DataAnnotations;

namespace StackBrothers_YassineBenAli.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Street is required.")]
        public string Street { get; set; }
        [Required(ErrorMessage = "HouseNumber is required.")]
        public int HouseNumber { get; set; }
        [Required(ErrorMessage = "ZipCode is required.")]
        public int ZipCode { get; set; }
        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }

        
            public override string ToString() {
            return " " + Street + " " + HouseNumber + " " + ZipCode + " " + City + " " + " " + Country;
            }

    
}
}
