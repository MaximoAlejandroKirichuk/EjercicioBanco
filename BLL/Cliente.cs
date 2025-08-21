using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Cliente
    {
        public int DNI { get; set; }
        public string Nombre { get; set; }
        public int NumeroTelefono { get; set; }
        public string Email { get; set; }
        public DateTime FechaNacimiento { get; set; }

        
        private readonly DALCliente dALCliente = new DALCliente();
        public DataTable MostrarTodosClientes()
        {
            return dALCliente.ObtenerClientes();
        }

        private bool ValidarDatos(int dni, string email, DateTime fechaNacimiento, string nombre, int numeroTelefono)
        {
            // Validación 1: El DNI no se repite
            // Asumimos que dALCliente tiene un método para verificar la existencia del DNI.
            if (dALCliente.ExisteDni(dni))
            {
                // Puedes lanzar una excepción específica o retornar false. Lanzar una excepción es a menudo preferible para dar un mensaje claro.
                throw new Exception("El DNI ya se encuentra registrado.");
            }

            // Validación 2: El nombre está vacío
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new Exception("El nombre no puede estar vacío.");
            }

            // Validación 3: El email está vacío
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("El email no puede estar vacío.");
            }

            // Validación 4: El número de teléfono es inválido
            if (numeroTelefono <= 0)
            {
                throw new Exception("El número de teléfono es inválido.");
            }

            // Validación 5: La fecha de nacimiento es inválida (ej. del futuro)
            if (fechaNacimiento > DateTime.Now)
            {
                throw new Exception("La fecha de nacimiento no puede ser en el futuro.");
            }

            // Si todas las validaciones pasan, el método retorna true.
            return true;
        }

        public bool AgregarCliente(int dni, string email, DateTime fechaNacimiento, string nombre, int numeroTelefono)
        {
           
            try
            {
                ValidarDatos(dni, email, fechaNacimiento, nombre, numeroTelefono);

                var respuesta = dALCliente.InsertarCliente(dni, email, fechaNacimiento, nombre, numeroTelefono);
                if (!respuesta)
                {
                    throw new Exception("Ocurrió un error al insertar el cliente.");
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception("Hay un error con los datos: " + ex.Message);
            }
        }
        public bool BorrarCliente(int dni)
        {
            var respuesta = dALCliente.DeleteCliente(dni);
            if (!respuesta) throw new Exception();
            return respuesta;
        }
        public bool Modificar(int dni, string email, DateTime fechaNacimiento, string nombre, int numeroTelefono)
        {
            var respuesta = dALCliente.UpdateCliente(dni, email, fechaNacimiento, nombre, numeroTelefono);
            if (!respuesta)
                throw new Exception();

            return respuesta;
        }
    }
}
