using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiApp.Models;

namespace WebApiApp.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetPortfolioAsync(ApplicationUser user);
        Task<Portfolio> CreateAsync(Portfolio portfolio);
        Task<Portfolio?> DeletePortfolio(ApplicationUser appUser, string symbol);

    }
}