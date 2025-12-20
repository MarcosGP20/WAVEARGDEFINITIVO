using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    [Table("ProductoVariantes")] 
    public class ProductoVariante
    {
        public int Id { get; set; }

        // --- Relación con el Padre ---
        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public virtual Productos Producto { get; set; }

        // --- Características Específicas ---
        [Required]
        public string Color { get; set; } // Ej: "Titanio Natural"

        [Required]
        public string Memoria { get; set; } // Ej: "256GB"

        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        public int Stock { get; set; } // Cantidad real disponible de ESTA variante

        // --- Lógica de Usados ---
        public bool EsUsado { get; set; }

        // Solo se llenan si EsUsado == true
        public string? DetalleEstado { get; set; }
        public string? FotoEstadoUrl { get; set; }
    }
}
