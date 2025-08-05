using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supply_Management_PT_XYZ.Data;
using Supply_Management_PT_XYZ.ViewModels;

namespace Supply_Management_PT_XYZ.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {

        private readonly MyContext _myContext;

        public AdminController(MyContext myContext)
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

            vendor.Status = (int)input.Persetujuan;

            await _myContext.SaveChangesAsync();

            return Ok(new
            {
                message = "Status vendor berhasil diperbarui.",
                username = vendor.Username,
                status_vendor = vendor.Status
            });
        }
    }
}