﻿using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController :ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        public async Task<IActionResult> GetAll()
        {
            var products = _repository.GetAllAsync();
            if (products == null) {return  NotFound();}
            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        public async Task<IActionResult> GetById(int id)
        {
            var product = _repository.GetByIdAsync(id);
            if (product == null) { return NotFound(); }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Product product)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var newProduct = await _repository.CreateAsync(product);
            return CreatedAtAction(nameof(GetById), new {id = newProduct.Id}, newProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Product product)
        {
            if (id != product.Id) { return BadRequest(); }
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) { return NotFound(); }
            await _repository.UpdateAsync(product);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) { return NotFound(); }
            await _repository.DeleteAsync(id);
            return Ok();
        }
    }
}
