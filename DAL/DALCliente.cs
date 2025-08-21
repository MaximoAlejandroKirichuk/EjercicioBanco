using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALCliente
    {
        private readonly string stringConnection = "Data Source=localHost;Initial Catalog=Banco;Integrated Security=True;";
        public DataTable ObtenerClientes()
        {
            using (SqlConnection connection = new SqlConnection(stringConnection))
            {
                string query = "Select * from Cliente";
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, connection))
                {
                    connection.Open();
                    var data = new DataTable();
                    sqlDataAdapter.Fill(data);
                    return data;
                }
            } 
        }
        public bool ExisteDni(int dni)
        {
            using (SqlConnection con = new SqlConnection(stringConnection))
            {
                // Usamos COUNT(1) para verificar la existencia. Es más eficiente que SELECT DNI.
                string query = "SELECT COUNT(1) FROM Cliente WHERE DNI = @dni;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@dni", dni);

                    try
                    {
                        con.Open();
                        // ExecuteScalar() ejecuta la consulta y devuelve el valor de la primera columna
                        // de la primera fila del conjunto de resultados.
                        // En este caso, devuelve 0 si no existe y 1 (o más) si existe.
                        int count = (int)cmd.ExecuteScalar();

                        // Si el conteo es mayor que 0, el DNI existe.
                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al verificar la existencia del DNI.", ex);
                    }
                }
            }
        }
        public bool InsertarCliente(int dni, string email, DateTime fechaNacimiento, string nombre, int numeroTelefono)
        {
            using(SqlConnection connection = new SqlConnection(stringConnection))
            {
                string query = "INSERT INTO Cliente(DNI, email, fecha_nacimiento, nombre, numero_telefono) values (@DNI,@email,@fecha_nacimiento,@nombre,@numeroTelefono)";
                using(SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@DNI",dni);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("fecha_nacimiento",fechaNacimiento);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@numeroTelefono", numeroTelefono);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public bool DeleteCliente(int dni)
        {
            using(SqlConnection connection = new SqlConnection(stringConnection))
            {
                string query = "DELETE FROM Cliente WHERE DNI=@DNI";
                using(SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@DNI", dni);
                    return 0 < cmd.ExecuteNonQuery();
                }
            }
        }
        public bool UpdateCliente(int dni, string email, DateTime fechaNacimiento, string nombre, int numeroTelefono)
        {
            using(SqlConnection connection = new SqlConnection(stringConnection))
            {
                string query = "UPDATE CLIENTE SET email=@email, fecha_nacimiento=@fecha_nacimiento,nombre=@nombre,numero_telefono=@numero_telefono WHERE DNI=@dni";
                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@dni",dni);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@fecha_nacimiento", fechaNacimiento);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@numero_telefono", numeroTelefono);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
