using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiApp.Data;
using WebApiApp.Dtos.Stock;
using WebApiApp.Helpers;
using WebApiApp.Interfaces;
using WebApiApp.Mappers;

namespace WebApiApp.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockRepository _stockRepo;
        public StockController(ApplicationDbContext context, IStockRepository stockRepo)
        {
            _context = context;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] StockQueryObject query)
        {
            var stocks = await _stockRepo.GetAllStocksAsync(query);
            var result = stocks.Select(item => item.ToStockDto());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);
            if(stock == null)
                return NotFound();
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = await _stockRepo.CreateStockAsync(stockDto.ToStockFromCreateDto());
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] UpdateStockRequestDto stockDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = await _stockRepo.UpdateAsync(id, stockDto);
            if(stockModel == null)
                return NotFound();

            await _context.SaveChangesAsync();
            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var stockModel = await _stockRepo.DeleteAsync(id);
            if(stockModel == null)
                return NotFound();
            return Ok(new { Message = "Thank you!" });
        }
    }
}