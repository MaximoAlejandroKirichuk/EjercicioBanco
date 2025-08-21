using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
namespace EjercicioRepasoPOO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private readonly Cliente BLLCliente = new Cliente();
        private readonly BLLCuenta bLLCuenta = new BLLCuenta();
        private readonly BLLOperaciones bLLOperaciones = new BLLOperaciones();
        public void ActualizarData()
        {
            dataGridView1.DataSource = BLLCliente.MostrarTodosClientes();
        }
        public void ActualizarDataCuentas(int dni)
        {
            dataGridView2.DataSource = bLLCuenta.EncontrarCuentaCliente(dni);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ActualizarData();
        }
        public void ActualizarDataOperaciones()
        {
            dataGridView3.DataSource = bLLOperaciones.MostrarTodasOperaciones();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int DNI = Convert.ToInt32(textBox1.Text);
                string nombre = textBox2.Text;
                int numeroTelefono = Convert.ToInt32(textBox3.Text);
                string email = textBox4.Text;
                DateTime fechaNacimiento = dateTimePicker1.Value.Date;

                var respuesta = BLLCliente.AgregarCliente(DNI,email,fechaNacimiento,nombre,numeroTelefono);
                if (respuesta) MessageBox.Show("Se agrego un cliente");
                ActualizarData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int dni = Convert.ToInt32(textBox1.Text);
                var respuesta = BLLCliente.BorrarCliente(dni);
                if (respuesta) MessageBox.Show("Se borro correctamente el cliente con dni: " + dni);
                ActualizarData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al bajar " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.RowIndex >= 0)
                {
                    var fila = dataGridView1.Rows[e.RowIndex];
                    // Asignar los valores a los controles
                    textBox1.Text = fila.Cells[0].Value.ToString();  //0 DNI
                    textBox2.Text = fila.Cells[1].Value.ToString();  //1 nombre
                    textBox3.Text = fila.Cells[2].Value.ToString();  //2 numero_telefono
                    textBox4.Text = fila.Cells[3].Value.ToString();  //3 email
                    dateTimePicker1.Value = Convert.ToDateTime(fila.Cells[4].Value);// 4 fecha_nacimiento
                    textBox6.Text = fila.Cells[0].Value.ToString();
                    ActualizarDataCuentas(Convert.ToInt32(fila.Cells[0].Value));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int DNI = Convert.ToInt32(textBox1.Text);
                string nombre = textBox2.Text;
                int numeroTelefono = Convert.ToInt32(textBox3.Text);
                string email = textBox4.Text;
                DateTime fechaNacimiento = dateTimePicker1.Value.Date;

                var respuesta = BLLCliente.Modificar(DNI, email, fechaNacimiento, nombre, numeroTelefono);
                if (respuesta) MessageBox.Show("Se modifico un cliente");
                ActualizarData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error "+ ex.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                int DNITitular = Convert.ToInt32(textBox6.Text);

                var saldo = 0;
                TipoCuenta tipoCuenta;
                Enum.TryParse(comboBox1.Text, out tipoCuenta);

                var respuesta = bLLCuenta.AgregarCuenta(DNITitular,saldo,tipoCuenta);                
                if (!respuesta) throw new Exception("Ocurrio un error");
                MessageBox.Show("Se agrego correctamente la nueva cuenta");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al agregar una nueva cuenta " + ex.Message);
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.RowIndex >= 0)
                {
                    var fila = dataGridView2.Rows[e.RowIndex];
                    
                    // Asignar los valores a los controles
                    textBox6.Text = fila.Cells[0].Value.ToString();  // DNI
                    textBox7.Text = fila.Cells[1].Value.ToString();  // codCuenta
                    textBox8.Text = fila.Cells[2].Value.ToString();  //saldo
                    comboBox1.SelectedItem = fila.Cells[3].Value.ToString();//tipocuenta
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }
        }

        private void btnBajarCuenta_Click(object sender, EventArgs e)
        {
            try
            {
                int codCuenta = Convert.ToInt32(textBox7.Text);
                decimal saldo = Convert.ToDecimal(textBox7.Text);
                bLLCuenta.BorrarCuenta(codCuenta, saldo);
                MessageBox.Show("Se pudo borrar correctamente la cuenta");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al borrar");
            }
        }

        private void btnModificarCuenta_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                decimal saldo = Convert.ToDecimal(textBox8.Text);
                decimal monto = Convert.ToDecimal(textBox5.Text);
                int codCuenta = Convert.ToInt32(textBox7.Text);
                int dniTitular = Convert.ToInt32(textBox6.Text);
                Enum.TryParse(comboBox1.Text, out TipoCuenta tipo);
                if (tipo == TipoCuenta.CuentaCorriente)
                {
                    var BLLcuentaCorriente = new BLLCuentaCorriennte();
                    BLLcuentaCorriente.Extraer(saldo, codCuenta,monto);
                    bLLOperaciones.AgregarTransaccion("Extraccion", DateTime.Now, monto);
                    ActualizarDataCuentas(dniTitular);
                }
                if (tipo == TipoCuenta.CajaAhorro)
                {
                    var BLLCajaAhorro = new BLLCajaAhorro();
                    BLLCajaAhorro.Extraer(saldo, codCuenta,monto);
                    bLLOperaciones.AgregarTransaccion("Extraccion", DateTime.Now, monto);
                    ActualizarDataCuentas(dniTitular);
                }
                ActualizarDataOperaciones();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al incorporar dinero");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                decimal saldo = Convert.ToDecimal(textBox8.Text);
                decimal monto = Convert.ToDecimal(textBox5.Text);
                int codCuenta = Convert.ToInt32(textBox7.Text);
                int dniTitular = Convert.ToInt32(textBox6.Text);
                
                Enum.TryParse(comboBox1.Text, out TipoCuenta tipo);
                if (tipo == TipoCuenta.CuentaCorriente)
                {
                    var BLLcuentaCorriente = new BLLCuentaCorriennte();
                    BLLcuentaCorriente.Depositar(saldo,codCuenta,monto);
                    bLLOperaciones.AgregarTransaccion("Deposito",DateTime.Now,monto);
                    ActualizarDataCuentas(dniTitular);
                    
                }
                if(tipo == TipoCuenta.CajaAhorro)
                {
                    var BLLCajaAhorro = new BLLCajaAhorro();
                    BLLCajaAhorro.Depositar(saldo, codCuenta, monto);
                    bLLOperaciones.AgregarTransaccion("Deposito", DateTime.Now, monto);
                    ActualizarDataCuentas(dniTitular);
                }
                ActualizarDataOperaciones();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al depositar el dinero: " + ex.Message);
            }
        }
    }
}
