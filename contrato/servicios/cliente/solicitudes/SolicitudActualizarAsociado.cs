using System.ComponentModel.DataAnnotations;

namespace contrato.servicios.cliente.solicitudes
{
    public class SolicitudActualizarAsociado
    {
     public int Id { get; set; } 

        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public bool medicamentos { get; set; }
        [Required]
        public bool enfermedades { get; set; }

        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "El formato del correo es inválido")]
        public string Correo { get; set; }
    }
}
