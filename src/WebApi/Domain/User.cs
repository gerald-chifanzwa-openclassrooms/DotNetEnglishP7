using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dot.Net.WebApi.Domain
{
    [Table("Users")]
    public class User
    {
        [Column("Id", TypeName = "INT"), Key]
		public int Id { get; set; }
        [Column("username", TypeName = "VARCHAR(125)")]
		public string UserName { get; set; }
        [Column("password", TypeName = "VARCHAR(125)")]
		public string Password { get; set; }
        [Column("fullname", TypeName = "VARCHAR(125)")]
		public string FullName { get; set; }
        [Column("role", TypeName = "VARCHAR(125)")]
		public string Role { get; set; }
    }
}