namespace CameraBazaar.Services
{
    using Data.Models.Enums;

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
    }
}
