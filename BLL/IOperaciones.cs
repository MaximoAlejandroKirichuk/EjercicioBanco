using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IOperaciones
    {
        void Depositar(decimal saldo, int codCuenta, decimal monto);
        void Extraer(decimal saldo,int codCuenta, decimal monto);
    }
}
