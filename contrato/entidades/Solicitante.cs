using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace contrato.entidades
{        
    public class Solicitante
    {
        public int Id { get; set; } 
        public int IdPedido { get; set; }

        public string cuil { get; set; }

        public string nombre { get; set; }
        public string apellido { get; set; }

        public string tipoSangre { get; set; }

        public DateTime fechaDonacion { get; set; }

        public string horaDonacion { get; set; }
    }
}