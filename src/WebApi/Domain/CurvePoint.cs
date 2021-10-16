using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dot.Net.WebApi.Domain
{
    [Table("CurvePoint)")]
    public class CurvePoint
    {
        [Column("Id", TypeName = "int"), Key]
        public int Id { get; set; }
        [Column("CurveId", TypeName = "int")]
        public int CurveId { get; set; }
        [Column("asOfDate", TypeName = "DATETIME2(7)")]
        public DateTime AsOfDate { get; set; }
        [Column("term", TypeName = "NUMERIC(8,2)")]
        public double Term { get; set; }
        [Column("value", TypeName = "NUMERIC(8,2)")]
        public double Value { get; set; }
        [Column("creationDate", TypeName = "DATETIME2(7)")]
        public DateTime CreationDate { get; set; }
    }
}