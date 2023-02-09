using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WinAppFacturacion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "")
            {
                MessageBox.Show("Ingrese Usuario");
            }
            else if (txtPass.Text == "")
            {
                MessageBox.Show("Ingrese Contrasenia");
            }
            else
            {
                try
                {
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9EUC20R\SQLEXPRESS;Initial Catalog=pruebaFacturacion;Integrated Security=True");
                    SqlCommand cmd = new SqlCommand("select * from tbl_usuario where usuario = @usuario And password = @pass", con);
                    cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                    cmd.Parameters.AddWithValue("@pass", txtPass.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        this.Hide();
                        winCRUDcliente index = new winCRUDcliente();
                        index.Show();
                    }
                    else
                    {
                        MessageBox.Show("No existe ese usuario o No existe ese pass");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }

            }

        }
    }
}
