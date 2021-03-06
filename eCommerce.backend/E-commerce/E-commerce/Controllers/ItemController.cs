﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Data.EntityModels;
using Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        private IItemImageService _itemImageService;
        public ItemController(ILogger<ItemController> logger, IItemService itemService,
            IItemImageService itemImageService)
        {
            _logger = logger;
            _itemService = itemService;
            _itemImageService = itemImageService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_itemService.GetAll());
        }

        [HttpGet("{id}", Name = "GetItemById")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_itemService.GetItem(id));
            }
            catch (Exception ex)
            {

                return BadRequest("Item not found");
            }
           
        }
        [HttpPost]
        [Authorize ( Roles = "Admin")]
        public IActionResult Create([FromForm]ItemVM itemVM)
        {
            try
            {
                if(itemVM.Image.Length<=0)
                    return BadRequest("Image is null");
                var item = new Item
                {
                    BrandCategoryID=itemVM.BrandCategoryID,
                    GenderSubCategoryID=itemVM.GenderSubCategoryID,
                    Description=itemVM.Description,
                    Price=itemVM.Price,
                    Name=itemVM.Name
                };
                var newItem = _itemService.AddItem(item);
                var newItemImage = _itemImageService.Add(itemVM.Image, newItem.ID);
                return CreatedAtRoute("GetItemById", new { id = newItem.ID }, newItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Item is null");
            }
        }
        //[HttpPost("create")]
        //public IActionResult Create(Item item)
        //{
        //    try
        //    {
        //        var newItem = _itemService.AddItem(item);
        //        return CreatedAtRoute("GetItemById", new { id = newItem.ID }, newItem);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Item is null");
        //    }
        //}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                _itemService.DeleteItem(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Item not found");
            }
            
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromBody] Item item)
        {
            try
            {
                _itemService.EditItem(item);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest("Item not found");
            }
        }


        //itemImage part

        [HttpPost("Image")]
        [Authorize(Roles = "Admin")]
        public IActionResult Add([FromForm] ItemImageVM itemImageVM)
        {
            try
            {
                if (itemImageVM.Image.Length <= 0)
                    return BadRequest("Image is null");
                var newItemImage = _itemImageService.Add(itemImageVM.Image, itemImageVM.ItemID);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Item is null");
            }
        }
        [HttpDelete("Image/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteImage(int id)
        {
            try
            {                
                _itemImageService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Item is null");
            }
        }
        //lista slika
        [HttpGet("Images/{id}")]
        public IActionResult GetImages(int id)
        {
            try
            {
                List<ItemImage> list = _itemImageService.GetAll(id);
                if (list.Count == 0)
                    return BadRequest("Item is null");
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest("Item is null");
            }
        }
        [HttpGet("Images")]
        public IActionResult GetImages()
        {
            try
            {
                List<ItemImage> list = _itemImageService.GetAll();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest("Images not found");
            }
        }
        //slika
        [HttpGet("Image/{id}")]
        public IActionResult GetItemImage(int id)
        {
            try
            {
                ItemImage itemImage = _itemImageService.GetById(id);

                return Ok(itemImage);
            }
            catch (Exception ex)
            {
                return BadRequest("Item is null");
            }
        }

        private byte[] GetImage(string sBase64String)
        {
            byte[] bytes = null;
            if (!string.IsNullOrEmpty(sBase64String))
            {
                bytes = Convert.FromBase64String(sBase64String);
            }
            return bytes;
        }
    }
}
