using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class ProductoUpdateDto
    {
        [Required]
        public int Id { get; set; } // Fundamental para identificar cuál editamos

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        public string Modelo { get; set; }

        public string Descripcion { get; set; }

        // NOTA: Se eliminaron CantidadStock, PrecioMinorista y PrecioMayorista.
        // Ahora esos datos se administran editando la Variante específica (Formulario 2).
    }
}
