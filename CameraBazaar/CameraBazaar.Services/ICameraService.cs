namespace CameraBazaar.Services
{
    using Data.Models.Enums;
    using Models;
    using System.Collections.Generic;

    public interface ICameraService
    {
        void Create(
            CameraMake make,
            string model,
            decimal price,
            int quantity,
            int minShutterSpeed,
            int maxShutterSpeed,
            MinISO minISO,
            int maxISO,
            bool isFullFrame,
            string videoResolution,
            LightMetering lightMetering,
            string description,
            string imageUrl,
            string userId);

        IEnumerable<CameraServiceModel> All();

        CameraDetailsServiceModel ById(int id);

        void Edit(int id, CameraMake make, string model, string description, string imageUrl, bool isFullFrame, LightMetering lightMetering, int maxISO, MinISO minISO, int minShutterSpeed, int maxShutterSpeed, decimal price, int quantity, string userId, string videoResolution);

        void DeleteById(int id);
    }
}
