using System.Threading.Tasks;
using contrato.entidades;
using contrato.servicios.cliente.solicitudes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using contrato.servicios.cliente.respuestas;
using System.Linq;
using Newtonsoft.Json;

namespace capacitacion.Controllers
{
    [ApiController]
    [Route("/api/[Controller]")]
    public class AsociadoController : Controller
    {
        public List<Asociado> asociados { get; set; }
        public const string DIRECCION_CLIENTE = "db/clientes.json"; 
        public AsociadoController()
        {

        }

        private void GuardarAsociado(Asociado? asociado)
    {
        if(asociado != null) asociados.Add(asociado);
        string temp = JsonConvert.SerializeObject(asociados);        
        System.IO.File.WriteAllText(DIRECCION_CLIENTE, temp);
    }

    private void CargarAsociados()
    {
        string json = System.IO.File.ReadAllText(DIRECCION_CLIENTE);
        asociados = JsonConvert.DeserializeObject<List<Asociado>>(json);
    
        if(asociados == null) asociados = new List<Asociado>();
    
    }



        [HttpGet]
        public async Task<RespuestaObtenerAsociado> Get([FromQuery] SolicitudObtenerAsociado solicitud)
        {
            CargarAsociados();

            var respuesta = new RespuestaObtenerAsociado();

            respuesta.Asociados = this.asociados;

            if (!string.IsNullOrEmpty(solicitud.Apellido)) {

                respuesta.Asociados = respuesta.Asociados.Where(c => c.Apellido.ToLower().Contains(solicitud.Apellido.ToLower())).ToList();
            }
            if (solicitud.Id > 0) {
                respuesta.Asociados = respuesta.Asociados.Where(c => c.Id == solicitud.Id).ToList();
            }
            if (!string.IsNullOrEmpty(solicitud.Nombre)) {
                respuesta.Asociados = respuesta.Asociados.Where(c => c.Nombre.ToLower().Contains(solicitud.Nombre.ToLower())).ToList();
            }
/*             if (solicitud.esDonante > 0) {
                respuesta.Asociados = respuesta.Asociados.Where(c => c.esDonante == solicitud.esDonante).ToList();
            } */
/*             if (!string.IsNullOrEmpty(solicitud.Correo)) {
                respuesta.Asociados = respuesta.Asociados.Where(c => c.Correo.ToLower().Contains(solicitud.Correo.ToLower())).ToList();
            }*/

            return respuesta;
        }


        [HttpPost]

        public async Task<RespuestaCrearAsociado> Post([FromBody] SolicitudCrearAsociado solicitud)
        {
            CargarAsociados();

            RespuestaCrearAsociado respuesta = new RespuestaCrearAsociado();
            Asociado clienteNuevo = new Asociado();

            if( solicitud.medicamentos != true && solicitud.enfermedades != true ){
                
                clienteNuevo.esDonante = true;

            }else{
                clienteNuevo.esDonante = false;
            }
            
            clienteNuevo.Id = solicitud.Id;
            clienteNuevo.Apellido = solicitud.Apellido;
            clienteNuevo.Nombre = solicitud.Nombre;
            clienteNuevo.Correo = solicitud.Correo;
            clienteNuevo.medicamentos = solicitud.medicamentos;
            clienteNuevo.enfermedades = solicitud.enfermedades;

            respuesta.Asociados = clienteNuevo;

            GuardarAsociado(clienteNuevo);

            return respuesta;
        }

        [HttpPut]
        public async Task<RespuestaActualizarAsociado> Put([FromBody] SolicitudActualizarAsociado solicitud)
        {
            CargarAsociados();
        
            var respuesta = new RespuestaActualizarAsociado();

            var cliente= this.asociados;

            bool existeCliente = cliente.Exists(x=>x.Id == solicitud.Id);

            if(existeCliente)
            {
                cliente.Remove(cliente.Find(x => x.Id == solicitud.Id));

                var clienteNuevo = new Asociado();

                if( solicitud.medicamentos != true && solicitud.enfermedades != true ){
                
                    clienteNuevo.esDonante = true;

                }else{
                    clienteNuevo.esDonante = false;
                }

                
                clienteNuevo.Id = solicitud.Id;
                clienteNuevo.Apellido = solicitud.Apellido;
                clienteNuevo.Nombre = solicitud.Nombre;
                clienteNuevo.Correo = solicitud.Correo;
                clienteNuevo.medicamentos = solicitud.medicamentos;
                clienteNuevo.enfermedades = solicitud.enfermedades;
                
                
                respuesta.Asociados = clienteNuevo;
                GuardarAsociado(clienteNuevo);
                return respuesta;
            }else{

                respuesta.ErrorMensaje = "El usuario no esta en la base de datos.";
                return respuesta;
            }

        }

        [HttpDelete]
        public async Task<RespuestaEliminarAsociado> Delete ([FromBody] SolicitudEliminarAsociado solicitud)
        {
            CargarAsociados();
        
            var respuesta = new RespuestaEliminarAsociado();

            var cliente= this.asociados;

            bool existeCliente = cliente.Exists(x=>x.Id == solicitud.Id);

            if(existeCliente)
            {
                cliente.Remove(cliente.Find(x => x.Id == solicitud.Id));

                
                respuesta.MensajeExito = "Usuario eliminado con exito!!" ;
                GuardarAsociado(null);

                return respuesta;

            }else{

                respuesta.MensajeError = "El usuario no esta en la base de datos.";
                return respuesta;
            }
        }
    }   
} 