﻿namespace CameraBazaar.Services.Implementations
{
    using System.Collections.Generic;
    using CameraBazaar.Data.Models.Enums;
    using CameraBazaar.Services.Models;
    using Data;
    using Data.Models;
    using System.Linq;

    public class CameraService : ICameraService
    {
        private readonly CameraBazaarDbContext db;

        public CameraService(CameraBazaarDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CameraServiceModel> All()
            => this.db
                .Cameras
                .Select(c => new CameraServiceModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Price = c.Price,
                    Quantity = c.Quantity,
                    ImageUrl = c.ImageUrl
                })
                .ToList();

        public CameraDetailsServiceModel ById(int id)
            => this.db
                .Cameras
                .Where(c => c.Id == id)
                .Select(c => new CameraDetailsServiceModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    Price = c.Price,
                    Quantity = c.Quantity,
                    Description = c.Description,
                    ImageUrl = c.ImageUrl,
                    IsFullFrame = c.IsFullFrame,
                    LightMetering = c.LightMetering,
                    MaxISO = c.MaxISO,
                    MinISO = c.MinISO,
                    MaxShutterSpeed = c.MaxShutterSpeed,
                    MinShutterSpeed = c.MinShutterSpeed,
                    VideoResolution = c.VideoResolution,
                    UserId = c.UserId,
                    Username = c.User.UserName
                })
                .FirstOrDefault();

        public void Create(
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
            string userId)
        {
            var camera = new Camera
            {
                Make = make,
                Model = model,
                Price = price,
                Quantity = quantity,
                MinShutterSpeed = minShutterSpeed,
                MaxShutterSpeed = maxShutterSpeed,
                MinISO = minISO,
                MaxISO = maxISO,
                IsFullFrame = isFullFrame,
                VideoResolution = videoResolution,
                LightMetering = lightMetering,
                Description = description,
                ImageUrl = imageUrl,
                UserId = userId
            };

            this.db.Add(camera);
            this.db.SaveChanges();
        }
    }
}
