namespace CarDealer.Services.Models.Cars
{
    using Models.Parts;
    using System.Collections.Generic;

    public class CarWithPartsModel
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public long TravelledDistance { get; set; }

        public IEnumerable<PartModel> Parts { get; set; }
    }
}
