using CameraBazaar.Data.Models.Enums;

namespace CameraBazaar.Web.Models.Cameras
{
    public class UserCameraViewModel
    {
        public CameraMake Make { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string ImageUrl { get; set; }
    }
}
