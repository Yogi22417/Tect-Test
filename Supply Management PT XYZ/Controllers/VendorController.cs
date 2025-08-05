using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supply_Management_PT_XYZ.Data;
using Supply_Management_PT_XYZ.Models.Entities;
using Supply_Management_PT_XYZ.ViewModels;

namespace Supply_Management_PT_XYZ.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class VendorController : Controller
    {

        private readonly MyContext _myContext;

        public VendorController(MyContext myContext)
        {
            _myContext = myContext;
        }

        [HttpGet("get_all_vendor")]
        public async Task<IActionResult> GeAllVendor()
        {

            var allVendor = await _myContext.vendor.ToListAsync();

            return Ok(allVendor);
        }

        [HttpPost("regis_vendor"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadGambar([FromForm] VendorRegis request)
        {

            var existingUser = await _myContext.akun
            .FirstOrDefaultAsync(a => a.Username == request.username);

            if (existingUser != null)
            {
                return BadRequest("Username sudah terdaftar, silakan pilih username lain.");
            }

            if (request.gambar == null && request.gambar.Length == 0)
            {
                return BadRequest("File tidak ada");
            }

            var folderName = Path.Combine("Resources", "GambarPerusahaan");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }

            var fileName = request.gambar.FileName;
            var fullPath = Path.Combine(pathToSave, fileName);
            var dbPath = Path.Combine(pathToSave, fileName);

            if (System.IO.File.Exists(fullPath))
            {
                return BadRequest("Nama Gambar Sudah Terpakai Mohon Untuk Mengganti Nama File Yang Anda Upload");
            }

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                request.gambar.CopyTo(stream);
            }

            var akun = new Akun
            {
                Username = request.username,
                Password = request.password,
                Roles = "vendor"
            };

            var vendor = new Vendor
            {
                Username = request.username,
                NamaPerusahaan = request.namaPerusahaan,
                Email = request.email,
                NoTelepon = request.noTelepon,
                NameGambar = fileName,
                Status = 0,
                CreatedDate = DateTime.Now
            };

            _myContext.akun.Add(akun);
            _myContext.vendor.Add(vendor);
            await _myContext.SaveChangesAsync();

            return Ok(new
            {
                message = "Registrasi berhasil",
            });
        }


        [HttpPut("update-data")]
        public async Task<IActionResult> UpdateDataVendor([FromForm] VendorDataLengkap request)
        {
            var akun = await _myContext.akun
                .FirstOrDefaultAsync(a => a.Username == request.Username && a.Password == request.Password);

            if (akun == null)
            {
                return BadRequest("Username atau password salah.");
            }

            var vendor = await _myContext.vendor
                .FirstOrDefaultAsync(v => v.Username == request.Username);

            if (vendor == null)
            {
                return NotFound("Vendor tidak ditemukan.");
            }

            if (vendor.Status == 10)
            {
                return BadRequest("Permintaan tidak dapat diproses karena permintaan registrasi telah ditolak");
            }

            if (vendor.Status == 0)
            {
                return BadRequest("Mohon untuk menunggu approval dari Admin");
            }

            if (vendor.Status == 2)
            {
                return BadRequest("Tidak bisa melakukan edit data saat approval Manager Logistik");
            }

            if (vendor.Status == 1)
            {
                vendor.BidangUsaha = request.BidangUsaha;
                vendor.JenisPerusahaan = request.JenisPerusahaan;
                vendor.Status = 2;
                await _myContext.SaveChangesAsync();

                return Ok(new
                {
                    message = "Data berhasil diperbarui, menunggu approval Manager Logistik.",
                    status_vendor = vendor.Status
                });
            }

            // Apabila Status Tidak Ada atau Diluar Kondisi Yang Ada
            return BadRequest("Data tidak dapat diubah pada status saat ini.");
        }

        [HttpPost("test_submit_proyek")]
        public async Task<IActionResult> SubmitProyek([FromForm] CekUploadTender request)
        {

            var vendor = await _myContext.vendor
            .FirstOrDefaultAsync(v => v.Username == request.username);

            if (vendor == null)
                return NotFound("User Vendor tidak ditemukan.");

            if (vendor.Status != 3)
            {
                return BadRequest("Mohon Untuk Menyelesaikan Pendataran Sebagai Vendor Terlebih Dahulu");
            }

            return Ok(new
            {
                message = "Selamat Anda Sudah Bisa Melakukan Submit Tender Proyek",
            });
        }


    }

}
