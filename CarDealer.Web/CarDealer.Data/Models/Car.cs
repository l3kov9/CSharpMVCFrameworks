﻿namespace CarDealer.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class Car
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Make { get; set; }

        [Required]
        [MaxLength(50)]
        public string Model { get; set; }

        [Range(0, long.MaxValue)]
        public long TravelledDistance { get; set; }

        public IEnumerable<Sale> Sales { get; set; } = new List<Sale>();

        public IEnumerable<PartCar> Parts { get; set; } = new List<PartCar>();
    }
}