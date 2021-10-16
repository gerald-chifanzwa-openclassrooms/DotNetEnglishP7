using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dot.Net.WebApi.Domain
{
    [Table("Rating")]
    public class Rating
    {
        [Column("Id", TypeName = "INT"), Key]
		public int Id { get; set; }
        [Column("moodysRating", TypeName = "VARCHAR(125)")]
		public string MoodysRating { get; set; }
        [Column("sandPRating", TypeName = "VARCHAR(125)")]
		public string SandPRating { get; set; }
        [Column("fitchRating", TypeName = "VARCHAR(125)")]
		public string FitchRating { get; set; }
        [Column("orderNumber", TypeName = "INT")]
		public int OrderNumber { get; set; }
    }
}