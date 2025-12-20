using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Modelo { get; set; }
        public string Descripcion { get; set; }

        // Calculado: Suma del stock de todas las variantes
        public int StockTotal { get; set; }

        // Lista de imagenes (URLs)
        public List<string> Imagenes { get; set; }

        // --- CAMBIO IMPORTANTE ---
        // Eliminamos 'List<PrecioDto>' porque el precio depende de la variante.
        // En su lugar, devolvemos la lista de variantes disponibles.
        public List<ProductoVarianteDto> Variantes { get; set; }
    }
}
