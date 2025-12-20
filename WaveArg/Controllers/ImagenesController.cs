using Data.Dtos;
using Microsoft.AspNetCore.Mvc;
using WaveArg.Interfaces;

namespace WaveArg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagenesController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ImagenesController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        // POST: api/Imagenes (Agregar foto extra)
        [HttpPost]
        public async Task<IActionResult> AgregarImagen([FromBody] ImagenDto dto)
        {
            var resultado = await _productoService.AgregarImagen(dto);

            if (!resultado)
                return BadRequest("El producto indicado no existe.");

            return Ok("Imagen agregada correctamente al carrusel.");
        }

        // DELETE: api/Imagenes/{id} (Borrar foto específica)
        [HttpDelete("{id}")]
        public async Task<IActionResult> BorrarImagen(int id)
        {
            var resultado = await _productoService.EliminarImagen(id);

            if (!resultado)
                return NotFound("La imagen no existe.");

            return Ok("Imagen eliminada.");
        }
    }
}
