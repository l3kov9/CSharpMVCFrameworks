namespace CameraBazaar.Data.Models
{
    using Enums;
    using System.ComponentModel.DataAnnotations;

    public class Camera
    {
        public int Id { get; set; }

        public CameraMake Make { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

        [Range(0, 100)]
        public int Quantity { get; set; }

        [Range(1, 30)]
        public int MinShutterSpeed { get; set; }

        [Range(2000, 8000)]
        public int MaxShutterSpeed { get; set; }

        public MinISO MinISO { get; set; }

        [Range(200, 409600)]
        public int MaxISO { get; set; }

        public bool IsFullFrame { get; set; }

        [MaxLength(15)]
        public string VideoResolution { get; set; }

        public LightMetering LightMetering { get; set; }

        [MaxLength(6000)]
        public string Description { get; set; }

        [MinLength(10)]
        [MaxLength(2000)]
        [RegularExpression(@"http(s)?:\/\/", ErrorMessage = "Image URL must start with http:// or https:// and must be between 10 and 2000 symbols.")]
        public string ImageUrl { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
