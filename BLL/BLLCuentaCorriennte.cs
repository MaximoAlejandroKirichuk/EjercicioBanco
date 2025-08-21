using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLCuentaCorriennte : BLLCuenta, IOperaciones
    {
        private readonly DALCuenta dALCuenta = new DALCuenta();
        public void Depositar(decimal saldo, int codCuenta, decimal monto)
        {
            if (monto <= 0) throw new Exception("Monto menor a 0");
            saldo += monto;
            dALCuenta.ModificarSaldo(saldo, codCuenta);
            
        }

        public void Extraer(decimal saldo, int codCuenta, decimal monto)
        {
            if (monto <= 0) throw new Exception("Monto menor a 0");
            saldo -= monto;
            dALCuenta.ModificarSaldo(saldo, codCuenta);
        }
    }
}
