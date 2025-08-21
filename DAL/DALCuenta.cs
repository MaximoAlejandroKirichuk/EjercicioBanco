using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALCuenta
    {
        private readonly string stringConnection = "Data Source=localHost;Initial Catalog=Banco;Integrated Security=True;";
        public DataTable EncontrarCuentaCliente(int dni)
        {
            using (SqlConnection connection = new SqlConnection(stringConnection))
            {
                string query = "Select * from Cuenta WHERE dni_cliente = @dni";
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, connection))
                {
                    connection.Open();
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@dni", dni);
                    // DataTable para almacenar los resultados de la consulta
                    var dataTable = new DataTable();
                    try
                    {
                        // El método Fill abre la conexión, ejecuta la consulta, llena el DataTable y luego cierra la conexión automáticamente.
                        sqlDataAdapter.Fill(dataTable);
                        // Devuelve el DataTable con los datos del cliente (o un DataTable vacío si no se encontró nada).
                        return dataTable;
                    }
                    catch (Exception ex)
                    {
                        // Manejo de errores para capturar cualquier problema con la conexión o la consulta.
                        throw new Exception("Error al buscar la cuenta del cliente. " + ex);
                    }
                }
            }
        }
        public bool InsertarCuenta(int dni, decimal saldo, string tipo)
        {
            using (SqlConnection connection = new SqlConnection(stringConnection))
            {
                string query = "INSERT INTO Cuenta (dni_cliente, saldo, tipo_cuenta) VALUES (@dni, @saldo, @tipo)"; ;
                using(SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@dni", dni);
                    cmd.Parameters.AddWithValue("@saldo", saldo);
                    cmd.Parameters.AddWithValue("@tipo", tipo);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public bool DeleteCuenta(int codCuenta)
        {
            using(SqlConnection connection = new SqlConnection(stringConnection))
            {
                string query = "Delete from Cuenta where codigo_cuenta = @codCuenta";
                using(SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("codCuenta", codCuenta);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool ModificarSaldo(decimal saldo, int codigoCuenta)
        {
            using (SqlConnection connection = new SqlConnection(stringConnection))
            {
                string query = "UPDATE Cuenta SET saldo=@saldo WHERE codigo_cuenta = @codCuenta";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("saldo", saldo);
                    cmd.Parameters.AddWithValue("codCuenta", codigoCuenta);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
