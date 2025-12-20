using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    [Table("Productos")] //Asegura que se mapee a la tabla correcta
    public class Productos
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Modelo { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }


        // Relaciones
        public virtual ICollection<ProductoPrecio> Precios { get; set; } //NO SE USA MAS
        public virtual ICollection<ProductoVariante> Variantes { get; set; }
        public virtual ICollection <Imagen> Imagenes { get; set; }

    }
}
