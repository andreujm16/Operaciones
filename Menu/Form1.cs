using Conexion;
using Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Menu
{
    public partial class Form1 : Form
    {
        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        LOG_Personas obj = new LOG_Personas();

        int personaid = 0;
        string personasel = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(txtdocumentoiden.Text) || String.IsNullOrEmpty(txtnombres.Text) || String.IsNullOrEmpty(txtapellidos.Text))
            {
                MessageBox.Show("Los siguientes datos son obligatorios: Documento de Identidad, Nombres, Apellidos. Por favor ingresarlos.");
            }
            else if(!Regex.IsMatch(txtdocumentoiden.Text, @"^[a-zA-Z0-9]+$"))
            {
                MessageBox.Show("Documento de Identidad solo acepta valores alfanumericos.");
            }
            else if(!Regex.IsMatch(txtnombres.Text, @"^[a-zA-Z0-9 ]+$") || !Regex.IsMatch(txtapellidos.Text, @"^[a-zA-Z0-9 ]+$"))
            {
                MessageBox.Show("Nombre y Apellidos solo aceptan valores latinos.");
            }
            else if (obj.ExistePersona(txtdocumentoiden.Text))
            {
                MessageBox.Show("Ya Existe una persona con el Documento de identidad " + txtdocumentoiden.Text);
            }
            else
            {
                per persona = new per
                {
                    documentoiden = txtdocumentoiden.Text,
                    nombres = txtnombres.Text,
                    apellidos = txtapellidos.Text,
                    fechanac = dtmfechanac.Value.Date.ToString("yyyy-MM-dd")
                };

                var result = obj.CrearPersona(persona);

                if (result)
                {
                    MessageBox.Show("Persona Creada Correctamente.");
                    ListarPersonas();

                }
                else
                {
                    MessageBox.Show("Hubo un problema al crear la persona. Por favor validar informacion.");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListarPersonas();

            ComboboxItem item = new ComboboxItem();
            item.Text = "Telefono";
            item.Value = 1;

            cboinfo.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "Correo";
            item.Value = 2;

            cboinfo.Items.Add(item);

            item = new ComboboxItem();
            item.Text = "Direccion";
            item.Value = 3;

            cboinfo.Items.Add(item);

            cboinfo.SelectedIndex = 0;
        }

        private void ListarPersonas()
        {
            dataGridView1.DataSource = obj.ListarPersonas();

            txtdocumentoiden.Text = "";
            txtnombres.Text = "";
            txtapellidos.Text = "";

        }

        private void btnborrar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string documentoiden = dataGridView1.CurrentRow.Cells["documentoiden"].Value.ToString();

                var result = obj.BorrarPersona(documentoiden);

                if (result)
                {
                    MessageBox.Show("Persona borrada correctamente.");
                    ListarPersonas();

                }
                else
                {
                    MessageBox.Show("Hubo un problema al borrar la persona. Por favor validar informacion.");
                }

            }
            else
                MessageBox.Show("Seleccione una persona a borrar.");
        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            try
            {
                string tipo = (cboinfo.SelectedItem as ComboboxItem).Value.ToString();

                if(txtinfo.Text == "")
                {
                    MessageBox.Show("Ingrese la informacion a agregar.");
                }
                else
                {
                    var result = obj.ValidaInformacionContacto(tipo, personaid);

                    if (!result)
                    {
                        MessageBox.Show("No se permite agregar mas informacion de este tipo. Maximo 2 por persona.");
                    }
                    else
                    {
                        var result1 = obj.AgregarInfo(tipo, txtinfo.Text, personaid);

                        if (result1)
                        {
                            MessageBox.Show("Informacion Creada Correctamente.");
                            ListarPersonas();

                        }
                        else
                        {
                            MessageBox.Show("Hubo un problema al agregar la informacion. Por favor validar informacion.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Seleccione una informacion para agregar.");
            }

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                personaid = Convert.ToInt32(row.Cells[0].Value.ToString());
                personasel = row.Cells[2].Value.ToString() + " " + row.Cells[3].Value.ToString();
            }

            groupBox2.Text = "Informacion de Contacto de: " + personasel;
            ListarInformacionPersona();
        }

        private void ListarInformacionPersona()
        {

            dataGridView2.DataSource = obj.ListarInformacionPersona(personaid);

            txtinfo.Text = "";


        }
    }
}
