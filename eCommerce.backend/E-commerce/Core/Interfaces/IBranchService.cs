using Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IBranchService
    {
        Branch AddBranch(Branch branch);
        Branch GetBranch(int id);
        List<Branch> GetAll();
        void DeleteBranch(int id);
        void EditBranch(Branch branch);
    }
}
