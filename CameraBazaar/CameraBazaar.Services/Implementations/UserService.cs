namespace CameraBazaar.Services.Implementations
{
    using Data;
    using Data.Models;
    using Models;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly CameraBazaarDbContext db;

        public UserService(CameraBazaarDbContext db)
        {
            this.db = db;
        }

        public UserServiceModel ByUsername(string username)
            => this.db
                  .Users
                  .Where(u => u.UserName == username)
                  .Select(u => new UserServiceModel
                  {
                      Username = u.UserName,
                      Email = u.Email,
                      Phone = u.PhoneNumber,
                      TotalCameras = u.Cameras.Count(),
                      Cameras = u.Cameras.Select(c => new CameraServiceModel
                      {
                          Id = c.Id,
                          Make = c.Make,
                          Model = c.Model,
                          Quantity = c.Quantity,
                          Price = c.Price,
                          ImageUrl = c.ImageUrl
                      })
                  })
                  .FirstOrDefault();
    }
}
