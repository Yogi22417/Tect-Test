using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supply_Management_PT_XYZ.Data;
using System.Threading.Tasks;

namespace Supply_Management_PT_XYZ.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AkunController : Controller
    {
        private readonly MyContext _myContext;

        public AkunController(MyContext myContext) {
            _myContext = myContext;
        }

        [HttpGet]
        public async Task<IActionResult> GeAllAkun()
        {

            var allAkun = await _myContext.akun.ToListAsync();

            return Ok(allAkun);
        }
    }
}
