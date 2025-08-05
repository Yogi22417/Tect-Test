using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supply_Management_PT_XYZ.Models.Entities
{
    public class Akun
    {
        public int Id { get; set; }
        [Column("username")]
        public string? Username { get; set; }
        [Column("password")]
        public string? Password { get; set; }
        [Column("roles")]
        public string? Roles { get; set; }
    }
}
