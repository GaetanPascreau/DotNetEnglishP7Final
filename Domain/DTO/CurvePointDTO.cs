using ServiceStack.FluentValidation.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using WebApi3.Validators;

namespace WebApi3.Domain.DTO
{
    //[Validator(typeof(CurvePointValidator))]
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
