using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WinAppFacturacion
{
    public partial class winCRUDcliente : Form
    {
        public winCRUDcliente()
        {
            InitializeComponent();
        }

        string idUpdate = "";
        private void winCRUDcliente_Load(object sender, EventArgs e)
        {
            dg_Clientes.DataSource = Clientes.Listar();

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDireccion_Click(object sender, EventArgs e)
        {
            
            txtDireccionLista.Show();
            List<string> direcciones = new List<string>();
            direcciones.Add(txtDireccionLista.Text);
            foreach (string direccion in direcciones)
            {
                txtDireccion.Text = txtDireccion.Text + direccion+"," + Environment.NewLine;
                txtDireccion.Text = txtDireccion.Text;
            }
            txtDireccionLista.Text = "";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (idUpdate == "")
            {
                if (Clientes.Verificar(txtCedula.Text))
                {
                    MessageBox.Show("Ya existe registro con esa cedula");
                }
                else
                {
                    if (Clientes.Validar(txtCedula.Text))
                    {
                        if (Clientes.Guardar(txtNombre.Text, txtApellido.Text, txtDireccion.Text, txtTelefono.Text, txtFechaNa.Text, txtCedula.Text, txtEmail.Text))
                        {
                            MessageBox.Show("Se registro con exito");
                            dg_Clientes.DataSource = Clientes.Listar();
                        }
                        else
                        {
                            MessageBox.Show("No se puede Guardar");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ingrese cedula valida");
                    }
                    
                }
                
            }

            if (idUpdate != "")
            {
                if (Clientes.Actualizar(idUpdate,txtNombre.Text, txtApellido.Text, txtDireccion.Text, txtTelefono.Text, txtFechaNa.Text, txtCedula.Text, txtEmail.Text))
                {
                    MessageBox.Show("Se actualizo con exito");
                    dg_Clientes.DataSource = Clientes.Listar();
                    idUpdate = "";
                }
                else
                {
                    MessageBox.Show("No se puede Actualizar");
                }
            }

            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtFechaNa.Text = "";
            txtCedula.Text = "";
            txtEmail.Text = "";
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string id = dg_Clientes.CurrentRow.Cells["ID"].Value.ToString();
            string cedula = dg_Clientes.CurrentRow.Cells["CI"].Value.ToString();
            string nombre = dg_Clientes.CurrentRow.Cells["NOMBRE"].Value.ToString();
            string apellido = dg_Clientes.CurrentRow.Cells["APELLIDO"].Value.ToString();
            string direccion = dg_Clientes.CurrentRow.Cells["DIRECCION"].Value.ToString();
            string telefono = dg_Clientes.CurrentRow.Cells["TELEFONO"].Value.ToString();
            string email = dg_Clientes.CurrentRow.Cells["CORREO"].Value.ToString();
            string fecha = dg_Clientes.CurrentRow.Cells["NACIMIENTO"].Value.ToString();
            var nacimiento = DateTime.Parse(fecha).ToString("dd/MM/yyy");
 
            txtNombre.Text = nombre;
            txtApellido.Text = apellido;
            txtDireccion.Text = direccion;
            txtTelefono.Text = telefono;
            txtFechaNa.Text = nacimiento;
            txtCedula.Text = cedula;
            txtEmail.Text = email;
            idUpdate = id;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string id = dg_Clientes.CurrentRow.Cells["ID"].Value.ToString();
            if (Clientes.Eliminar(id))
            {
                MessageBox.Show("Se elimino con exito");
                dg_Clientes.DataSource = Clientes.Listar();
                idUpdate = "";
            }
            else
            {
                MessageBox.Show("No se puede Eliminar");
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 32 & e.KeyChar <= 64) || (e.KeyChar >= 91 & e.KeyChar <= 96)|| (e.KeyChar >= 123 & e.KeyChar <= 255))
            {
                MessageBox.Show("Solo Letras", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 & e.KeyChar <= 47) || (e.KeyChar >= 58 & e.KeyChar <= 255))
            {
                MessageBox.Show("Solo Numeros", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 32 & e.KeyChar <= 64) || (e.KeyChar >= 91 & e.KeyChar <= 96) || (e.KeyChar >= 123 & e.KeyChar <= 255))
            {
                MessageBox.Show("Solo Letras", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 & e.KeyChar <= 47) || (e.KeyChar >= 58 & e.KeyChar <= 255))
            {
                MessageBox.Show("Solo Numeros", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
    }
}
