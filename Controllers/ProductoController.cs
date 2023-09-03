using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NEGOCIO.Models;
namespace NEGOCIO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        //Declaro un obj de dbcontext
        public readonly DBAPIWEBContext _dbcontext; //para gestionar las operaciones crud

        //constructor
        public ProductoController(DBAPIWEBContext _context)
        {
            _dbcontext = _context;  
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Producto> lista = new List<Producto>();

            try
            {
                lista = _dbcontext.Productos.Include(c => c.oCategoria).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }
        }


        [HttpGet]
        [Route("Obtener/{idProducto:int}")]
        public IActionResult Obtener(int idProducto)
        {
            Producto oProducto = _dbcontext.Productos.Find(idProducto);

            if (oProducto == null) 
            {
                return BadRequest("Producto no encontrado");

            }

            try
            {
                oProducto = _dbcontext.Productos.Include(c => c.oCategoria).Where(p => p.IdProducto == idProducto).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oProducto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = oProducto });
            }
        }

    }
}
