using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class ProductoVarianteDto
    {
        public int Id { get; set; }          // NECESARIO para el carrito
        public string Color { get; set; }    // NECESARIO para mostrar
        public string Memoria { get; set; }  // NECESARIO para mostrar
        public decimal Precio { get; set; }  // NECESARIO para mostrar
        public int Stock { get; set; }       // NECESARIO para validar disponibilidad
        public bool EsUsado { get; set; }    // NECESARIO para poner cartel "Usado"

        // Si quieres mostrar la foto específica del usado:
        public string? FotoEstadoUrl { get; set; }
    }
}
