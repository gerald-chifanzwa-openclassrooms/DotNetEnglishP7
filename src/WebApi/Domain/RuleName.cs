using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dot.Net.WebApi.Domain
{
    [Table("RuleName")]
    public class RuleName
    {
        [Column("Id", TypeName = "INT"), Key]
		public int Id { get; set; }
        [Column("name", TypeName = "VARCHAR(125)")]
		public string Name { get; set; }
        [Column("description", TypeName = "VARCHAR(125)")]
		public string Description { get; set; }
        [Column("json", TypeName = "VARCHAR(125)")]
		public string Json { get; set; }
        [Column("template", TypeName = "VARCHAR(512)")]
		public string Template { get; set; }
        [Column("sqlStr", TypeName = "VARCHAR(125)")]
		public string SqlStr { get; set; }
        [Column("sqlPart", TypeName = "VARCHAR(125)")]
		public string SqlPart { get; set; }
    }
}