using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    //Este objeto define el JSON que el front enviara
    public class ProductoCreateDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } // Ej: "iPhone 15 Pro"

        public string Modelo { get; set; } // Ej: "A2890"

        public string Descripcion { get; set; } // Ej: "Cuerpo de titanio..."

        // Lista de URLs de imágenes (Fotos bonitas de marketing)
        public List<string> ImagenesUrls { get; set; }

    }
}
