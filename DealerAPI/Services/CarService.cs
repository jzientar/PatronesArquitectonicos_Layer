using AutoMapper;
using DealerAPI.Data.Entities;
using DealerAPI.Data.Repository;
using DealerAPI.Exceptions;
using DealerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealerAPI.Services
{
    public class CarService : ICarService
    {
        private IDealerRepository repository;
        private IMapper mapper;

        public CarService(IDealerRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public CarModel CreateCar(int DealerId, CarModel newCar)
        {
            ValidateDealer(DealerId);
            var carEntity = repository.CreateCar(mapper.Map<CarEntity>(newCar));
            return mapper.Map<CarModel>(carEntity);
        }

        public bool DeleteCar(int dealerId, int id)
        {
            var car = GetCar(dealerId, id);
            if (car == null)
            {
                throw new NotFoundException($"The car with {id} does not exist in the dealership.");
            }
            return repository.DeleteCar(id);
        }

        public CarModel GetCar(int DealerId, int id)
        {
            ValidateDealer(DealerId);
            var car = repository.GetCar(id);
            if (car == null || car.DealerId != DealerId)
            {
                throw new NotFoundException($"The id :{id} doesn't exist.");
            }

            return mapper.Map<CarModel>(car);
        }

        public IEnumerable<CarModel> GetCars(int dealerId)
        {
            ValidateDealer(dealerId);
            return mapper.Map<IEnumerable<CarModel>>(repository.GetCars(dealerId));
        }

        public bool UpdateCar(int dealerId, int id, CarModel car)
        {
            car.Id = id;
            GetCar(dealerId, id);

            return repository.UpdateCar(mapper.Map<CarEntity>(car));

        }

        private void ValidateDealer(int id)
        {
            var dealerEntity = repository.GetDealer(id);
            if (dealerEntity == null)
            {
                throw new NotFoundException($"the id :{id} not exist for dealer");
            }
        }
    }
}
