using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Data.EntityModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace E_commerce.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private IItemService _itemService;
        public ItemController(ILogger<ItemController> logger, IItemService itemService)
        {
            _logger = logger;
            _itemService = itemService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_itemService.GetAll());
        }

        [HttpGet("{id}", Name = "GetItemById")]
        public IActionResult GetById(int id)
        {
            return Ok(_itemService.GetItem(id));
        }
        [HttpPost]
        public IActionResult Create(Item item)
        {
            var newItem = _itemService.AddItem(item);
            return CreatedAtRoute("GetItemById", new { id = newItem.ID }, newItem);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _itemService.DeleteItem(id);
            return Ok();
        }
        [HttpPut]
        public IActionResult Update([FromBody] Item item)
        {
            _itemService.EditItem(item);
            return Ok();
        }
    }
}
