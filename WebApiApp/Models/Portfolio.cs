using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiApp.Models
{
    [Table("Portfolios")]
    public class Portfolio
    {
        public string ApplicationUserId { get; set; } 
        public int StockId { get; set; }
        public ApplicationUser User { get; set; }
        public Stock Stock { get; set; }
    }
}