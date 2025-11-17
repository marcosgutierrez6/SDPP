using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace API_Rest_Form
{
    public partial class Form1 : Form
    {
        private ApiService _apiService;

        public Form1()
        {
            InitializeComponent();
            _apiService = new ApiService();
            textBox2.UseSystemPasswordChar = true;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string usuario = textBox1.Text;
            string pass = textBox2.Text;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Por favor ingresa usuario y contraseña");
                return;
            }

            // Desactivar controles mientras carga
            button1.Enabled = false;
            button1.Text = "Verificando...";

            // 1. Intentar Login
            string token = await _apiService.LoginAsync(usuario, pass);

            if (token != null)
            {
                // LOGIN EXITOSO
                button1.Text = "Cargando productos...";
                MessageBox.Show($"¡Login Correcto!\nCargando catálogo...", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 2. Cargar Productos (NUEVO)
                List<Producto> listaProductos = await _apiService.GetProductosAsync();

                if (listaProductos != null)
                {
                    // 3. Asignar los datos al DataGridView
                    dataGridView1.DataSource = listaProductos;
                    MessageBox.Show($"Se han cargado {listaProductos.Count} productos.");
                }
                else
                {
                    MessageBox.Show("Login ok, pero falló la carga de productos.");
                }
            }
            else
            {
                // LOGIN FALLIDO
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Restaurar botón
            button1.Enabled = true;
            button1.Text = "Iniciar Sesión";
        }

        // Eventos vacíos (puedes dejarlos o borrarlos si no los usas en el diseñador)
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }

        // Agrega esto para callar el error
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // No hacer nada
        }
    }
}