namespace CameraBazaar.Web.Models.Cameras
{
    using Data.Models.Enums;
    using System.ComponentModel.DataAnnotations;

    public class AddCameraViewModel
    {
        public CameraMake Make { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

        [Range(0, 100)]
        public int Quantity { get; set; }

        [Range(1, 30)]
        [Display(Name = "Min Shutter Speed")]
        public int MinShutterSpeed { get; set; }

        [Range(2000, 8000)]
        [Display(Name = "Max Shutter Speed")]
        public int MaxShutterSpeed { get; set; }

        [Display(Name = "Min ISO")]
        public MinISO MinISO { get; set; }

        [Range(200, 409600)]
        [Display(Name = "Max ISO")]
        public int MaxISO { get; set; }

        [Display(Name = "Is Full Frame")]
        public bool IsFullFrame { get; set; }

        [MaxLength(15)]
        [Display(Name = "Video Resolution")]
        public string VideoResolution { get; set; }

        [Display(Name = "Light Metering")]
        public LightMetering LightMetering { get; set; }

        [MaxLength(6000)]
        public string Description { get; set; }

        [MinLength(10)]
        [MaxLength(2000)]
        [Display(Name = "Image URL")]
        [RegularExpression(@"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$", ErrorMessage = "Image URL must start with http:// or https:// and must be between 10 and 2000 symbols.")]
        public string ImageUrl { get; set; }
    }
}
