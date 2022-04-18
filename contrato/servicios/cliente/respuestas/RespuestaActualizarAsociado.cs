namespace contrato.servicios.cliente.respuestas
{
    public class RespuestaActualizarAsociado
    {
        public contrato.entidades.Asociado Asociados { get; set; }

        public string ErrorMensaje { get; set; }
    }
}