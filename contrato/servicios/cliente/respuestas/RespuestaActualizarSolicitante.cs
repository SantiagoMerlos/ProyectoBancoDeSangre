namespace contrato.servicios.cliente.respuestas
{
    public class RespuestaActualizarSolicitante
    {
        public contrato.entidades.Solicitante solicitante { get; set; }

        public string ErrorMensaje { get; set; }
    }
}