﻿namespace CarDealer.Services
{
    using Models.Sales;
    using System.Collections.Generic;

    public interface ISaleService
    {
        IEnumerable<SaleListModel> All();

        SaleDetailsModel Details(int id);

        IEnumerable<SaleListModel> Discounted();

        void Create(int carId, int customerId, double discount);
    }
}
