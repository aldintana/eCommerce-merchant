using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Data.EntityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace E_commerce.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly ILogger<BranchController> _logger;
        private IBranchService _branchService;
        public BranchController(ILogger<BranchController> logger, IBranchService branchService)
        {
            _logger = logger;
            _branchService = branchService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_branchService.GetAll());
        }

        [HttpGet("{id}", Name = "GetBranchById")]
        public IActionResult GetById(int id)
        {
            return Ok(_branchService.GetBranch(id));
        }
        [HttpPost]
        public IActionResult Create(Branch branch)
        {
            var newBranch = _branchService.AddBranch(branch);
            return CreatedAtRoute("GetBranchById", new { id = newBranch.ID }, newBranch);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _branchService.DeleteBranch(id);
            return Ok();
        }
        [HttpPut]
        public IActionResult Update([FromBody] Branch branch)
        {
            _branchService.EditBranch(branch);
            return Ok();
        }
    }
}
