using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class ImagenDto
    {
        public int ProductoId { get; set; } // ¿A qué iPhone pertenece?
        public string Url { get; set; }     // El link de la foto
    }
}
