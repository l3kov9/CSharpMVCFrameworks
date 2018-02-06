namespace CarDealer.Services
{
    using Models.Cars;
    using System.Collections.Generic;

    public interface ICarService
    {
        IEnumerable<CarModel> ByMake(string make);

        IEnumerable<CarWithPartsModel> WithParts(int page = 1, int pageSize = 10);

        void Create(string make, string model, long travelledDistance, IEnumerable<int> parts);

        void Delete(int id);

        int Total();
    }
}
