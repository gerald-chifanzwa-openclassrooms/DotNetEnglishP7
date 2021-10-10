using System;

namespace WebApi.Models
{
    public class CurvePointModel
    {
        public int CurveId { get; set; }
        public DateTime AsOfDate { get; set; }
        public double Term { get; set; }
        public double Value { get; set; }
        public double CreationDate { get; set; }
    }
}
