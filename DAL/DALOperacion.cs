using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALOperacion
    {
        private readonly string stringConnection = "Data Source=localHost;Initial Catalog=Banco;Integrated Security=True;";
        public bool InsertOperacion(DateTime fecha, string operacion, decimal importe)
        {
            using(SqlConnection connection = new SqlConnection(stringConnection))
            {
                string query = "INSERT INTO OPERACION (fecha, tipo_operacion, importe) VALUES (@fecha, @tipo, @importe)";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@fecha", fecha);
                    cmd.Parameters.AddWithValue("@tipo", operacion);
                    cmd.Parameters.AddWithValue("@importe", importe);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public DataTable EncontrarTodasOperaciones()
        {
            using (SqlConnection connection = new SqlConnection(stringConnection))
            {
                string query = "Select * from Operacion";
                using (SqlDataAdapter dataTable = new SqlDataAdapter(query, connection))
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, connection))
                {
                    connection.Open();
                    var data = new DataTable();
                    sqlDataAdapter.Fill(data);
                    return data;
                }
            }
        }
    }
}
