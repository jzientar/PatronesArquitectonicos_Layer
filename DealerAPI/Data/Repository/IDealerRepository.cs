using DealerAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealerAPI.Data.Repository
{
    public interface IDealerRepository
    {
        DealerEntity GetDealer(int id, bool showCars = false);
        IEnumerable<DealerEntity> GetDealers(string orderBy, bool showCars = false);
        DealerEntity CreateDealer(DealerEntity newDealer);
        bool UpdateDealer(DealerEntity dealer);
        bool DeleteDealer(int id);

        CarEntity GetCar(int id);
        IEnumerable<CarEntity> GetCars(int dealerId);
        CarEntity CreateCar(CarEntity newCar);
        bool UpdateCar(CarEntity Car);
        bool DeleteCar(int id);
    }
}
