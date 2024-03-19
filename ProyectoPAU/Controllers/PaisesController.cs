using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoPAU.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisesController : ControllerBase
    {

        [Authorize]
        [HttpGet]
        [Route("lista")]
        public async Task<IActionResult> Lista()
        {
            var listaPaises  = await Task.FromResult(new List<string> {"Francia", "Argentina", "Croacia", "Marruecos" });
            return Ok(listaPaises);
        }
    }
}
