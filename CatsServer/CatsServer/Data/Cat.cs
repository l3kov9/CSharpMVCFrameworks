namespace CatsServer.Data
{
    using System.ComponentModel.DataAnnotations;

    public class Cat
    {
        private const int MinStringLength = 1;
        private const int MaxStringLength = 50;

        public int Id { get; set; }

        [Required]
        [MinLength(MinStringLength)]
        [MaxLength(MaxStringLength)]
        public string Name { get; set; }

        [Range(0,30)]
        public int Age { get; set; }

        [Required]
        [MinLength(MinStringLength)]
        [MaxLength(MaxStringLength)]
        public string Breed { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(2000)]
        public string ImageUrl { get; set; }
    }
}
