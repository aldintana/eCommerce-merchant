using Core.Interfaces;
using Data.DbContext;
using Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services
{
    public class BranchService : IBranchService
    {
        private E_commerceDB _context;
        public BranchService(E_commerceDB context)
        {
            _context = context;
        }
        public Branch AddBranch(Branch branch)
        {
            _context.Branch.Add(branch);
            _context.SaveChanges();
            return branch;
        }

        public void DeleteBranch(int id)
        {
            var branch = _context.Branch.First(x => x.ID == id);
            _context.Branch.Remove(branch);
            _context.SaveChanges();
        }

        public void EditBranch(Branch branch)
        {
            var editedBranch = _context.Branch.First(x => x.ID == branch.ID);
            editedBranch.CityId = branch.CityId;
            editedBranch.Adress = branch.Adress;
            editedBranch.Description = branch.Description;
            editedBranch.Name = branch.Name;
            editedBranch.WorkingHours = branch.WorkingHours;
            _context.Entry(editedBranch).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        public List<Branch> GetAll()
        {
            return _context.Branch.ToList();
        }

        public Branch GetBranch(int id)
        {
            return _context.Branch.First(x => x.ID == id);
        }
    }
}
