using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class ProductoVarianteUpdateDto
    {
        [Required]
        public int Id { get; set; } // Necesitamos saber cuál variante editar

        public decimal Precio { get; set; }
        public int Stock { get; set; }

        // Opcionales (por si te equivocaste al cargar color/memoria)
        public string Color { get; set; }
        public string Memoria { get; set; }

        public bool EsUsado { get; set; }
        public string? DetalleEstado { get; set; }
    }
}
