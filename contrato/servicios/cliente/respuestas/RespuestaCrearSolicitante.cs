namespace contrato.servicios.cliente.respuestas
{
    public class RespuestaCrearSolicitante
    {
        public contrato.entidades.Solicitante solicitante { get; set; }

        public string mensajeError { get; set; }
    }
}