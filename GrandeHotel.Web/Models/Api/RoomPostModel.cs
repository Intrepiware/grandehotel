using System.ComponentModel.DataAnnotations;

namespace GrandeHotel.Web.Models.Api
{
    public class RoomPostModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Nightly rate must be greater than 0")]
        public decimal NightlyRate { get; set; }
    }
}
