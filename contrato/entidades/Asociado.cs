using System.ComponentModel.DataAnnotations;

namespace contrato.entidades
{        
    public class Asociado
    {
        public int Id { get; set; } 

        public string  Nombre { get; set; }

        public string  Apellido { get; set; }

        public bool  esDonante { get; set; }

        public bool medicamentos { get; set; }
        public bool enfermedades { get; set; }

        public string Correo { get; set; }
    }
}