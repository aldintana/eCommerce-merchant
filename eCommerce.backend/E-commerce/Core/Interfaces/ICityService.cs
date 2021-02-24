using Data.EntityModels;
using System;
using System.Collections.Generic;

namespace Core
{
    public interface ICityService
    {
        City AddCity(City city);
        City GetCity(int id);
        List<City> GetAll();
        void DeleteCity(int id);
        void EditCity(City city);
    }
}
