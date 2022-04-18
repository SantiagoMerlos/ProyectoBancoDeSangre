namespace contrato.servicios.cliente.respuestas
{
    public class RespuestaEliminarAsociado
    {
        public contrato.entidades.Asociado Id { get; set;}
        public string MensajeError { get; set; }
        public string MensajeExito{ get; set; }
    }
}