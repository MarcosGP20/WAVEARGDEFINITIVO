using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Stock")]
    public class Stock
    {
        [Key]
        public int Id { get; set; }
        public int Cantidad { get; set; }

        //Clave Fk
        public int ProductoId {  get; set; }

        [ForeignKey("ProductoId")]
        public virtual Productos Producto { get; set; }
    }
}