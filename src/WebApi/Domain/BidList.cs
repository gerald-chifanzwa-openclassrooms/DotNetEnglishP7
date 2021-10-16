using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dot.Net.WebApi.Domain
{
    [Table("BidList")]
    public class BidList
    {
        [Column("BidListId", TypeName = "INT"), Key]
        public int Id { get; set; }
        [Column("account", TypeName = "VARCHAR(30)")]
        public string Account { get; set; }
        [Column("type", TypeName = "VARCHAR(30)")]
        public string Type { get; set; }
        [Column("askQuantity", TypeName = "NUMERIC(8,2)")]
        public double AskQuantity { get; set; }
        [Column("bidQuantity", TypeName = "NUMERIC(8,2)")]
        public double BidQuantity { get; set; }
        [Column("bid", TypeName = "NUMERIC(8,2)")]
        public double BidAmount { get; set; }
        [Column("ask", TypeName = "NUMERIC(8,2)")]
        public double AskAmount { get; set; }
        [Column("benchmark", TypeName = "VARCHAR(125)")]
        public string Benchmark { get; set; }
        [Column("bidListDate", TypeName = "DATETIME2(7)")]
        public DateTime ListDate { get; set; }
        [Column("commentary", TypeName = "VARCHAR(125)")]
        public string Commentary { get; set; }
        [Column("security", TypeName = "VARCHAR(125)")]
        public string Security { get; set; }
        [Column("status", TypeName = "VARCHAR(10)")]
        public string Status { get; set; }
        [Column("trader", TypeName = "VARCHAR(125)")]
        public string Trader { get; set; }
        [Column("book", TypeName = "VARCHAR(125)")]
        public string Book { get; set; }
        [Column("creationName", TypeName = "VARCHAR(125)")]
        public string CreationName { get; set; }
        [Column("creationDate", TypeName = "DATETIME2(7)")]
        public DateTime CreationDate { get; set; }
        [Column("revisionName", TypeName = "VARCHAR(125)")]
        public string RevisionName { get; set; }
        [Column("revisionDate", TypeName = "DATETIME2(7)")]
        public DateTime RevisionDate { get; set; }
        [Column("dealName", TypeName = "VARCHAR(125)")]
        public string DealName { get; set; }
        [Column("dealType", TypeName = "VARCHAR(125)")]
        public string DealType { get; set; }
        [Column("sourceListId", TypeName = "VARCHAR(125)")]
        public string SourceListId { get; set; }
        [Column("side", TypeName = "VARCHAR(125)")]
        public string Side { get; set; }
    }
}