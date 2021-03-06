﻿using CarShare.Domain.Entities;
using CarShare.Domain.ViewEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShare.Domain.Abstract
{
    public interface ICarRepository
    {
        Car GetCarByID(int carID);

        IEnumerable<Car> GetCarsOwnedBy(int userID);

        DetailsView GetCarAndAssociatedDetails(int carID);

        IEnumerable<Make> GetAllMakes();

        IEnumerable<Model> GetAllModels();

        IEnumerable<DetailsView> GetSearchResults(string kword, int model, int make, string location);
    }
}
