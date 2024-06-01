using Microsoft.EntityFrameworkCore;
using WebApiApp.Data;
using WebApiApp.Dtos.Stock;
using WebApiApp.Helpers;
using WebApiApp.Interfaces;
using WebApiApp.Migrations;
using WebApiApp.Models;

namespace WebApiApp.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateStockAsync(Stock stockDto)
        {
            await _context.Stock.AddAsync(stockDto);
            await _context.SaveChangesAsync();
            return stockDto;
        }
        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _context.Stock.FirstOrDefaultAsync(s => s.Symbol == symbol);
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            if(stockModel == null)
                return null;
            _context.Stock.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public  async Task<List<Stock>> GetAllStocksAsync(StockQueryObject query)
        {
            var stocks =  _context.Stock.Include(x => x.Comments).AsQueryable();
            if(!string.IsNullOrWhiteSpace(query.CompanyName))
                stocks = stocks.Where(x => x.CompanyName.Contains(query.CompanyName));
            if(!string.IsNullOrWhiteSpace(query.Symbol))
                stocks = stocks.Where(x => x.Symbol.Contains(query.Symbol));
            if(!string.IsNullOrWhiteSpace(query.SortBy))
                if(query.SortBy.Equals("Symbol", StringComparison.CurrentCultureIgnoreCase))
                    stocks = query.IsDec ? stocks.OrderByDescending(x => x.Symbol) : stocks.OrderBy(x => x.Symbol);

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            var stock = await _context.Stock.Include(c => c.Comments).FirstOrDefaultAsync(x => x.Id == id);
            if(stock == null)
                return null;
            else
                return stock;
        }

        public async Task<bool> StockExists(int id)
        {
            return await _context.Stock.AnyAsync(x => x.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            if(stockModel == null) return null;
            
            stockModel.Symbol = stockDto.Symbol;
            stockModel.CompanyName = stockDto.CompanyName;
            stockModel.Purchase = stockDto.Purchase;
            stockModel.LastDiv = stockDto.LastDiv;
            stockModel.Industry = stockDto.Industry;
            stockModel.MarketCap =  stockDto.MarketCap;

            await _context.SaveChangesAsync();
            return stockModel;
        }
    }
}