using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public enum TipoCuenta
    {
        CajaAhorro,
        CuentaCorriente
    }
    public class BLLCuenta
    {
        private readonly int cantidadCuentas = 2; 
        public int DniCliente { get; set; }
        public int Saldo { get; }
        public TipoCuenta Tipo { get; set; }
        public int CodigoCuenta { get; set; }
        private readonly DALCuenta dALCuenta = new DALCuenta();

        public DataTable EncontrarCuentaCliente(int dni)
        {
            return dALCuenta.EncontrarCuentaCliente(dni);
        }
        public bool AgregarCuenta(int dni, decimal saldo, TipoCuenta tipo)
        {
            var respuesta = dALCuenta.EncontrarCuentaCliente(dni);
            var cuentasExistentes = dALCuenta.EncontrarCuentaCliente(dni);

            // Verificar si el cliente ya tiene una cuenta del tipo que se intenta agregar.
            // Usamos un bucle para recorrer todas las filas del DataTable.
            foreach (DataRow fila in cuentasExistentes.Rows)
            {
                string tipoCuentaExistente = fila["tipo_cuenta"].ToString();
                if (tipoCuentaExistente == tipo.ToString()) throw new Exception("Ya existe una cuenta de este tipo.");
            }
            if (respuesta.Columns.Count >= cantidadCuentas) throw new Exception("Ya cuenta con toda la cantidaad de cuentas posibles del banco");
            return dALCuenta.InsertarCuenta(dni, saldo, tipo.ToString());
        }
        public bool BorrarCuenta(int codigoCuenta, decimal saldo)
        {
            if (saldo != 0) throw new Exception("El saldo tiene que ser 0 para borrar");
            var respuesta = dALCuenta.DeleteCuenta(codigoCuenta);
            if (!respuesta) throw new Exception("Ocurrio un error al eliminar la cuenta");
            return respuesta;
        }
    }
}
