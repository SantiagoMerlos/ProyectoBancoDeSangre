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

        private void GuardarTurnos(Solicitante? solicitante)
    {
        if(solicitante != null) Solicitantes.Add(solicitante);
        string temp = JsonConvert.SerializeObject(Solicitantes);        
        System.IO.File.WriteAllText(DIRECCION_SOLICITANTES, temp);
    }

    private void CargarTurnos()
    {
        string json = System.IO.File.ReadAllText(DIRECCION_SOLICITANTES);
        Solicitantes = JsonConvert.DeserializeObject<List<Solicitante>>(json);
    
        if(Solicitantes == null) Solicitantes = new List<Solicitante>();
    
    }


 public List<Asociado> asociados { get; set; }
        public const string DIRECCION_CLIENTE = "db/clientes.json"; 

    private void CargarAsociados()
    {
        string json = System.IO.File.ReadAllText(DIRECCION_CLIENTE);
        asociados = JsonConvert.DeserializeObject<List<Asociado>>(json);
    
        if(asociados == null) asociados = new List<Asociado>();
    
    }


        [HttpGet]
        public async Task<RespuestaObtenerAsociado> Get([FromQuery] SolicitudObtenerSolicitante solicitud)
        {
            
            CargarAsociados();

            var user = new List<Asociado>();
            var donantes = new List<Asociado>();
            
            //int valor = 0;
            
            var respuesta = new RespuestaObtenerAsociado();
            if (solicitud.tipoSangre == "Ap") solicitud.tipoSangre ="A+";
            if (solicitud.tipoSangre == "Bp") solicitud.tipoSangre ="B+";
            if (solicitud.tipoSangre == "0p") solicitud.tipoSangre ="0+";
            
            if(solicitud.tipoSangre !="")
            {
                user = asociados.FindAll(x=> x.tipoSangre == solicitud.tipoSangre);
                donantes = user.FindAll(x=> x.esDonante == true);
                respuesta.Asociados = donantes;
            }
            else
            {
             respuesta.Asociados = donantes; 
            }
            
            return respuesta;
           
        }
        
        [HttpGet]
        [Route("Home")]
        
        public async Task<RespuestaObtenerSolicitante> GetObtener()
        {
            var respuesta = new RespuestaObtenerSolicitante();
            
            CargarTurnos();

            var user = new List<Solicitante>();
            
           /*var usuarios = this.Solicitantes;

            usuarios.Remove(usuarios.Find(x =>x.fechaDonacion < DateTime.Now ));
    
            GuardarTurnos();
             */

            user = Solicitantes.FindAll(x=> x.fechaDonacion > DateTime.Now);
            
            respuesta.solicitantes = user;
            
            return respuesta;
           
        }


        [HttpPost]
        public async Task<RespuestaCrearSolicitante> Post([FromBody] SolicitudCrearAsignacionTurnoss solicitud)
        {
            CargarTurnos();

            var respuesta = new RespuestaCrearSolicitante();
            var solicitanteNuevo = new Solicitante();
            
            solicitanteNuevo.Id = solicitud.Id;
            solicitanteNuevo.IdPedido = solicitud.IdPedido;
            solicitanteNuevo.nombre = solicitud.nombre;
            solicitanteNuevo.apellido = solicitud.apellido;
            solicitanteNuevo.tipoSangre = solicitud.tipoSangre;
            solicitanteNuevo.fechaDonacion = solicitud.fechaDonacion;
            solicitanteNuevo.horaDonacion = solicitud.horaDonacion;
        
            respuesta.solicitante = solicitanteNuevo;

            GuardarTurnos(solicitanteNuevo);

            return respuesta;
        } 

    /*     


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

       } */
 
    }
}