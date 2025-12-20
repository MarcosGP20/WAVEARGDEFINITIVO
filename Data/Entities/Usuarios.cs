using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Contraseña { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Rolid { get; set; }
        public Roles Rol { get; set; }
        public string Provider {  get; set; }
        public DateTime? FechaRegistro {  get; set; }
    }
}
