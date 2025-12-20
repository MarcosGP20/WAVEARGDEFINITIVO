using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("ProductoPrecios")]
    public class ProductoPrecio
    {
        [Key]
        public int Id { get; set; }
        public string TipoCliente { get; set; } // Ej: "Minorista" , "Mayorista"

        public decimal Precio { get; set; }
        
        //Clave Fk
        public int ProductoId {  get; set; }

        [ForeignKey("ProductoId")]
        public virtual Productos Producto { get; set; }
    }
}