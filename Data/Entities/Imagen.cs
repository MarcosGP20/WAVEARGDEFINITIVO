using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Imagenes")]
    public class Imagen
    {
        [Key]   
        public int Id { get; set; }
        public string Url { get; set; }
        public bool EsPrincipal { get; set; }

        //Clafe FK
        public int ProductoId {  get; set; }

        [ForeignKey("ProductoId")]
        public virtual Productos Producto { get; set; }
    }
}