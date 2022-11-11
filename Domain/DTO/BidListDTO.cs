using System;

namespace WebApi3.Domain.DTO
{
    public class BidListDTO
    {
        public int BidListId { get; set; }

        public string Account { get; set; }

        public string Type { get; set; }

        public decimal BidQuantity { get; set; }

        public DateTime BidListDate { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime RevisionDate { get; set; }
    }
}
