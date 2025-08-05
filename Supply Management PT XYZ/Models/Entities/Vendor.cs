using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Supply_Management_PT_XYZ.Models.Entities
{
    public class Vendor
    {
        public int Id { get; set; }
        [Column("nama_perusahaan")]
        public string? NamaPerusahaan { get; set; } = string.Empty;

        [Column("bidang_usaha")]
        public string? BidangUsaha { get; set; } = string.Empty;

        [Column("email_perusahaan")]
        public string? Email { get; set; }= string.Empty;

        [Column("nomor_telp")]
        public string? NoTelepon { get; set; }

        [Column("foto")]
        public string? NameGambar { get; set; } 

        [Column("status_vendor")]
        public int? Status { get; set; }

        [Column("jenis_perusahaan")]
        public string? JenisPerusahaan { get; set; }

        [Column("created_date")]
        public DateTime? CreatedDate { get; set; }

        [Column("username")]
        public string? Username { get; set; }


        [NotMapped, JsonIgnore]
        public IFormFile? Gambar { get; set; }
    }
}   