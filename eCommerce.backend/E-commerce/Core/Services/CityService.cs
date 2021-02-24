using Data.DbContext;
using Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services
{
    public class CityService : ICityService
    {
        private E_commerceDB _context;
        public CityService(E_commerceDB context)
        {
            _context = context;
        }
        public City AddCity(City city)
        {
            _context.City.Add(city);
            _context.SaveChanges();
            return city;
        }

        public void DeleteCity(int id)
        {
            City city = _context.City.First(x => x.CityID == id);
            _context.City.Remove(city);
            _context.SaveChanges();
        }

        public void EditCity(City city)
        {
            var editedCity = _context.City.First(x => x.CityID == city.CityID);
            editedCity.Name = city.Name;
            editedCity.PostalCode = city.PostalCode;
            _context.Entry(editedCity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public List<City> GetAll()
        {
            return _context.City.ToList();
        }

        public City GetCity(int id)
        {
            return _context.City.First(x => x.CityID == id);
        }
    }
}
