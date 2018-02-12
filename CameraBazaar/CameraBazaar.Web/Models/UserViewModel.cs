namespace CameraBazaar.Web.Models
{
    using Models.Cameras;
    using System.Collections.Generic;

    public class UserViewModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int TotalCameras { get; set; }

        public IEnumerable<UserCameraViewModel> Cameras { get; set; }
    }
}
