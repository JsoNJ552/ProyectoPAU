using Microsoft.AspNetCore.Mvc;



namespace CRUDCategoria.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {
        private static List<Categoria> _categorias = new List<Categoria>();

        // GET /categoria
        [HttpGet]
        public IEnumerable<Categoria> Get()
        {
            return _categorias;
        }

        // GET /categoria/{id}
        [HttpGet("{id}")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _categorias.FirstOrDefault(c => c.Id_Categoria == id);
            if (categoria == null)
            {
                return NotFound();
            }
            return categoria;
        }

        // POST /categoria
        [HttpPost]
        public ActionResult<Categoria> Post(Categoria categoria)
        {
            categoria.Id_Categoria = _categorias.Count + 1; // Genera un nuevo ID
            _categorias.Add(categoria);
            return CreatedAtAction(nameof(Get), new { id = categoria.Id_Categoria }, categoria);
        }

        // PUT /categoria/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, Categoria categoria)
        {
            var existingCategoria = _categorias.FirstOrDefault(c => c.Id_Categoria == id);
            if (existingCategoria == null)
            {
                return NotFound();
            }

            existingCategoria.Nombre = categoria.Nombre;

            return NoContent();
        }

        // DELETE /categoria/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var categoria = _categorias.FirstOrDefault(c => c.Id_Categoria == id);
            if (categoria == null)
            {
                return NotFound();
            }

            _categorias.Remove(categoria);

            return NoContent();
        }
    }

    public class Categoria
    {
        public int Id_Categoria { get; set; }
        public string Nombre { get; set; }
    }
}
