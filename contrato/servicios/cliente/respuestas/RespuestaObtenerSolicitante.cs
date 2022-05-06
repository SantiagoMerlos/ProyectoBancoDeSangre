using System.Collections.Generic;

namespace contrato.servicios.cliente.respuestas
{
    public class RespuestaObtenerSolicitante
    {
        public List<contrato.entidades.Solicitante> solicitantes { get; set; }

       public contrato.entidades.Solicitante solicitante { get; set; }
    }
}
