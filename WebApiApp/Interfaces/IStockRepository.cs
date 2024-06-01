using WebApiApp.Dtos.Stock;
using WebApiApp.Helpers;
using WebApiApp.Models;

namespace WebApiApp.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllStocksAsync(StockQueryObject query);

        Task<Stock> CreateStockAsync(Stock stockDto);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock?> DeleteAsync(int id);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);

        Task<bool> StockExists(int id);
    }
}