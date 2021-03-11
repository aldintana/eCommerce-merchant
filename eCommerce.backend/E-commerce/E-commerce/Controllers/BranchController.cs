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
            try
            {
                return Ok(_branchService.GetAll());
            }
            catch (Exception)
            {
                return BadRequest("Branch not found");
            }
        }

        [HttpGet("{id}", Name = "GetBranchById")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_branchService.GetBranch(id));
            }
            catch (Exception)
            {
                return BadRequest("Branch not found");
            }
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Branch branch)
        {
            try
            {
                var newBranch = _branchService.AddBranch(branch);
                return CreatedAtRoute("GetBranchById", new { id = newBranch.ID }, newBranch);
            }
            catch (Exception)
            {
                return BadRequest("Branch not found");
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                _branchService.DeleteBranch(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Branch not found");
            }
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromBody] Branch branch)
        {
            try
            {
                _branchService.EditBranch(branch);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Branch not found");
            }
        }
    }
}
