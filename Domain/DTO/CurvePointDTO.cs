using System;

namespace WebApi3.Domain.DTO
{
    public class CurvePointDTO
    {
        public int Id { get; set; }

        public int CurveId { get; set; }

        public decimal Term { get; set; }

        public decimal Value { get; set; }

        public DateTime AsOfDate { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
