using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supply_Management_PT_XYZ.Data;
using Supply_Management_PT_XYZ.ViewModels;

namespace Supply_Management_PT_XYZ.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManagerLogisticController : ControllerBase
    {
        private readonly MyContext _myContext;

        public ManagerLogisticController(MyContext myContext)
        {
            _myContext = myContext;
        }

        [HttpPut("approval")]
        public async Task<IActionResult> AdminApproval([FromForm] ApprovalAdmin input)
        {
            var vendor = await _myContext.vendor
            .FirstOrDefaultAsync(v => v.Username == input.UsernameVendor);

            if (vendor == null)
                return NotFound("User Vendor tidak ditemukan.");

            if (input.Persetujuan == null)
                return BadRequest("Persetujuan wajib diisi");

            if (vendor.Status == 10)
            {
                return BadRequest("Permintaan tidak dapat diproses karena permintaan registrasi telah ditolak");
            }

            if (vendor.Status == 0)
            {
                return BadRequest("Mohon vendor melakukan approval oleh admin");
            }

            if (vendor.Status == 1)
            {
                return BadRequest("Mohon untuk vendor melakukan update data Bidang Usaha dan Jenis Perusahaan terlebih dahulu");
            }


            if (vendor.Status == 2)
            {
                vendor.Status = (int)input.Persetujuan;

            await _myContext.SaveChangesAsync();

            return Ok(new
            {
                message = "Status vendor berhasil diperbarui.",
                username = vendor.Username,
                status_vendor = vendor.Status
            });
            }

            // Apabila Status Tidak Ada atau Diluar Kondisi Yang Ada
            return BadRequest("Data tidak dapat diubah pada status saat ini.");
        }
    }
}
