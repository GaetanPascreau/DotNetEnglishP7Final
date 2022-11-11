using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi3.Domain.DTO
{
    public class TradeDTO
    {
        public int TradeId { get; set; }

        public string Account { get; set; }

        public string Type { get; set; }

        public decimal? BuyQuantity { get; set; }

        public decimal? SellQuantity { get; set; }

        public decimal? BuyPrice { get; set; }

        public decimal? SellPrice { get; set; }

        public DateTime TradeDate { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime RevisionDate { get; set; }
    }
}
