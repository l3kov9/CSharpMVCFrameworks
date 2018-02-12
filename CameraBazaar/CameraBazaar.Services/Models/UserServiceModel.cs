namespace CameraBazaar.Services.Models
{
    using System.Collections.Generic;

    public class UserServiceModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int TotalCameras { get; set; }

        public IEnumerable<CameraServiceModel> Cameras { get; set; }
    }
}
