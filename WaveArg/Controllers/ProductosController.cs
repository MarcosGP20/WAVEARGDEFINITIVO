using Data.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using WaveArg.Interfaces;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace WaveArg.Controllers
{
    [Route("api/[controller]")] //Esto define la URL
    [ApiController]
    public class ProductosController : Controller
    {
        private readonly IProductoService _productoService;

        // Inyectamos el servicio
        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        // POST: api/productos
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ProductoCreateDto dto)
        {
            try
            {
                // Llamamos al servicio. Si el precio es negativo, el servicio tirará error.
                await _productoService.CrearProducto(dto);

                return Ok(new { message = "iPhone creado con éxito" });
            }
            catch (Exception ex)
            {
                // Si algo falló (validación o base de datos), devolvemos error 400
                return BadRequest(new { error = ex.Message });
            }
        }

        // GET: api/productos
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos([FromQuery] bool soloDisponibles = false)
        {
            try
            {
                var productos = await _productoService.ObtenerProductos(soloDisponibles);

                return Ok(productos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        // PUT: api/productos
        [HttpPut]
        public async Task<IActionResult> Modificar([FromBody] ProductoUpdateDto dto)
        {
            // Validamos que el ID sea válido
            if (dto.Id <= 0)
                return BadRequest("El ID del producto es obligatorio.");

            var resultado = await _productoService.ModificarProducto(dto);

            if (!resultado)
            {
                return NotFound("No se encontró el producto con ese ID.");
            }

            return Ok("Producto actualizado correctamente.");

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> BorrarProducto(int id)
        {
            var resultado = await _productoService.EliminarProducto(id);

            if (!resultado)
            {
                return NotFound("El producto no existe.");
            }

            return Ok("Producto y sus variantes eliminados correctamente.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerProducto(int id)
        {
            var producto = await _productoService.ObtenerProductoPorId(id);

            if (producto == null)
            {
                return NotFound("El producto no existe.");
            }

            return Ok(producto);
        }
    }
}
