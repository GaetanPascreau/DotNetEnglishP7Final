using System;

namespace WebApi3.Domain
{
    public class CurvePoint
    {
        // TODO: Map columns in data table CURVEPOINT with corresponding fields
        public int Id { get; set; }
        public int CurveId { get; set; }
        public DateTime AsOfDate { get; set; }
        public decimal Term { get; set; }
        public decimal Value { get; set; }
        public DateTime CreationDate { get; set; }
    }
}