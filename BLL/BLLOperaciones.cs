using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   
    public class BLLOperaciones
    {
        public string TipoOperacion { get; set; }
        public DateTime Fecha { get; set; }
        public decimal ImporteAfectado { get; set; }
        public int IdOperacion { get; set; }
        private readonly DALOperacion dALOperacion = new DALOperacion();
        public void AgregarTransaccion(string tipo, DateTime fecha, decimal importeAfectado)
        {
            var respuesta = dALOperacion.InsertOperacion(fecha, tipo, importeAfectado);
            if (!respuesta) throw new Exception("Ocurrio un error al registrar la operacion");
        }
        public DataTable MostrarTodasOperaciones()
        {
            return dALOperacion.EncontrarTodasOperaciones();
        }
    }
}
