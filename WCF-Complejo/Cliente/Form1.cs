using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using Cliente.ServiceReference1;
using Servidor1;

namespace Cliente
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void sumar_Click(object sender, EventArgs e)
        {
            RealizarCalculo("sumar");
        }

        private void restar_Click(object sender, EventArgs e)
        {
            RealizarCalculo("restar");
        }

        private void dividir_Click(object sender, EventArgs e)
        {
            RealizarCalculo("dividir");
        }

        private void multiplicar_Click(object sender, EventArgs e)
        {
            RealizarCalculo("multiplicar");
        }

        private void RealizarCalculo(string operacion)
        {
            double n1, n2;

            if (!double.TryParse(textBox1.Text, out n1) || !double.TryParse(textBox2.Text, out n2))
            {
                MessageBox.Show("Por favor, ingrese números válidos en ambos campos.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PeticionCalculo peticion = new PeticionCalculo
            {
                Operacion = operacion,
                Numero1 = n1,
                Numero2 = n2
            };

            try
            {
                using (Service1Client cliente = new Service1Client())
                {
                    RespuestaCalculo respuesta = cliente.CalcularAsync(peticion);

                    string mensajeCompleto = $"--- Respuesta del Servidor --- {Environment.NewLine}" +
                                             $"Éxito: {respuesta.Exito}{Environment.NewLine}" +
                                             $"Operación: {respuesta.OperacionRealizada ?? "N/A"}{Environment.NewLine}" +
                                             $"Resultado: {respuesta.Resultado}{Environment.NewLine}" +
                                             $"Mensaje de Error: {(string.IsNullOrEmpty(respuesta.MensajeError) ? "Ninguno" : respuesta.MensajeError)}";

                    MessageBox.Show(mensajeCompleto, "Respuesta Completa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (FaultException ex)
            {
                MessageBox.Show($"Error de comunicación: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error general: {ex.Message}", "Error Inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
    }
}