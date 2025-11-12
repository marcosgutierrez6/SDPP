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

namespace Cliente
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }

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

        private async void RealizarCalculo(string operacion)
        {
            double n1, n2;

            if (!double.TryParse(textBox1.Text, out n1) || !double.TryParse(textBox2.Text, out n2))
            {
                MessageBox.Show("Por favor, ingrese números válidos en ambos campos.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (Service1Client cliente = new Service1Client())
                {
                    double resultado = await cliente.CalcularAsync(operacion, n1, n2);

                    MessageBox.Show($"El resultado de {operacion} es: {resultado}", "Resultado");
                }
            }
            catch (FaultException ex)
            {
                MessageBox.Show($"Error del servicio: {ex.Message}", "Error de Cálculo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error general: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}