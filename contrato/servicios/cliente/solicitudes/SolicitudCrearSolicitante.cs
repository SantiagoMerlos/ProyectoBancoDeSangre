using System.ComponentModel.DataAnnotations;

namespace contrato.servicios.cliente.solicitudes
{
    public class SolicitudCrearSolicitante
    {
        [Required]
        public int Id { get; set; } 

        public string cuil { get; set; }
        [Required]

        public string nombre { get; set; }

        public string apellido { get; set; }
    }
}
