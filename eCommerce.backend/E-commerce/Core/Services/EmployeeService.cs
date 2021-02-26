using Core.Interfaces;
using Data.DbContext;
using Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private E_commerceDB _context;
        public EmployeeService(E_commerceDB context)
        {
            _context = context;
        }
        public void AddEmployee(Employee employee)
        {
            _context.Employee.Add(employee);
            _context.SaveChanges();
        }
    }
}
