using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class ProductoVarianteCreateDto
    {
        [Required]
        public int ProductoId { get; set; } // El ID del "Padre" (iPhone 15)

        [Required]
        public string Color { get; set; }

        [Required]
        public string Memoria { get; set; }

        [Required]
        public decimal Precio { get; set; }

        public int Stock { get; set; }

        public bool EsUsado { get; set; }
        public string? DetalleEstado { get; set; }
        public string? FotoEstadoUrl { get; set; }
    }
}
