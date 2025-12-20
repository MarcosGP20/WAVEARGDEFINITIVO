using Data.Dtos;
using Microsoft.AspNetCore.Mvc;
using WaveArg.Interfaces;

namespace WaveArg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VariantesController : Controller
    {
        private readonly IVarianteService _varianteService;

        public VariantesController(IVarianteService varianteService)
        {
            _varianteService = varianteService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearVariante([FromBody] ProductoVarianteCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validacion extra de negocio: Si es usado, obligar detalle
            if (dto.EsUsado && string.IsNullOrEmpty(dto.DetalleEstado))
            {
                return BadRequest("Si el producto es usado, debe incluir el detalle del estado.");
            }

            var resultado = await _varianteService.AgregarVariante(dto);
            return Ok(resultado);
        }

        [HttpPut]
        public async Task<IActionResult> ModificarVariante([FromBody] ProductoVarianteUpdateDto dto)
        {
            var resultado = await _varianteService.ModificarVariante(dto);

            if (!resultado) return NotFound("No se encontró la variante para editar.");

            return Ok("Variante actualizada correctamente.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarVariante(int id)
        {
            var resultado = await _varianteService.EliminarVariante(id);

            if (!resultado) return NotFound("No se encontró la variante.");

            return Ok("Variante eliminada.");
        }
    }
}
