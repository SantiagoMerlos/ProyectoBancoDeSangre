using System.ComponentModel.DataAnnotations;

namespace contrato.entidades
{        
    public class Solicitante
    {
        public int Id { get; set; } 

        public string cuil { get; set; }

        public string nombre { get; set; }
    }
}