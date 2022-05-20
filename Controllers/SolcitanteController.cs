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
    
    public class SolicitanteController : Controller
    { 
        public List<Solicitante> Solicitantes { get; set; }
        public const string DIRECCION_SOLICITANTES = "db/solicitantes.json"; 
        public SolicitanteController()
        {

        }

        private void GuardarAsociado(Solicitante? solicitante)
    {
        if(solicitante != null) Solicitantes.Add(solicitante);
        string temp = JsonConvert.SerializeObject(Solicitantes);        
        System.IO.File.WriteAllText(DIRECCION_SOLICITANTES, temp);
    }

    private void CargarAsociados()
    {
        string json = System.IO.File.ReadAllText(DIRECCION_SOLICITANTES);
        Solicitantes = JsonConvert.DeserializeObject<List<Solicitante>>(json);
    
        if(Solicitantes == null) Solicitantes = new List<Solicitante>();
    
    }



         [HttpPost]
        public async Task<RespuestaCrearSolicitante> Post([FromBody] SolicitudCrearSolicitante solicitud)
        {
            CargarAsociados();

            var respuesta = new RespuestaCrearSolicitante();
            var solicitanteNuevo = new Solicitante();
            
            solicitanteNuevo.Id = solicitud.Id;
            solicitanteNuevo.nombre = solicitud.nombre;
            solicitanteNuevo.cuil = solicitud.cuil;
            solicitanteNuevo.apellido = solicitud.apellido;
            

            respuesta.solicitante = solicitanteNuevo;

            GuardarAsociado(solicitanteNuevo);

            return respuesta;
        } 

        [HttpGet]
        public async Task<RespuestaObtenerSolicitante> Get([FromQuery] SolicitudObtenerSolicitante solicitud)
        {
            CargarAsociados();

            var user = new Solicitante();

            //int valor = 0;

            var respuesta = new RespuestaObtenerSolicitante();

            if(solicitud.Id != 0)
            {
                user = Solicitantes.Find(x=> x.Id == solicitud.Id);
                respuesta.solicitante = user;

                return respuesta;
            }
            else
            {
                respuesta.solicitantes = Solicitantes;
            }
            
            return respuesta;
           
        }


        [HttpPut]
        public async Task<RespuestaActualizarSolicitante> Put([FromBody] SolicitudActualizarSolicitante solicitud)
        {
            CargarAsociados();
        
            var respuesta = new RespuestaActualizarSolicitante();

            var listSolicitantes= this.Solicitantes;

            bool existeCliente = listSolicitantes.Exists(x=>x.Id == solicitud.Id);

            if(existeCliente)
            {
                listSolicitantes.Remove(listSolicitantes.Find(x => x.Id == solicitud.Id));

                var SolicitanteNuevo = new Solicitante();
                
                SolicitanteNuevo.Id = solicitud.Id;
                SolicitanteNuevo.apellido = solicitud.apellido;
                SolicitanteNuevo.nombre = solicitud.nombre;
                SolicitanteNuevo.cuil = solicitud.cuil;
                
                respuesta.solicitante = SolicitanteNuevo;
                GuardarAsociado(SolicitanteNuevo);

                return respuesta;
            }else{

                respuesta.ErrorMensaje = "El usuario no esta en la base de datos.";
                return respuesta;
            }

       }
 
    }
}