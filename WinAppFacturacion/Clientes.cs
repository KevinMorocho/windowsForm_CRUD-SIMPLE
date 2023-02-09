using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WinAppFacturacion
{
    class Clientes
    {
        static string Connection = "Data Source=DESKTOP-9EUC20R\\SQLEXPRESS;Initial Catalog=pruebaFacturacion;Integrated Security=True";
        public static DataTable Listar()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(Connection);
            SqlCommand cmd = new SqlCommand("Select id as ID, cedula as CI, nombre as NOMBRE, apellido as APELLIDO, direccion as DIRECCION, email as CORREO, telefono as TELEFONO, fecha as NACIMIENTO from clientes", con);
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return dt;

        }

        public static bool Guardar(string nombre, string apellido, string direccion, string telefono, string fecha_nac, string cedula, string email)
        {
            bool exito = false;
            var fecha = DateTime.Parse(fecha_nac).ToString("yyyy-MM-dd");
            string sql = "INSERT INTO clientes (cedula, nombre, apellido, direccion, email, telefono, fecha) VALUES('" + cedula + "','" + nombre + "','" + apellido + "','" + direccion + "','" + email + "','" + telefono + "','" + fecha + "');";
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            try
            {

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                exito = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return exito;
        }
        public static bool Actualizar(string id, string nombre, string apellido, string direccion, string telefono, string fecha_nac, string cedula, string email)
        {
            bool exito = false;
            var fecha = DateTime.Parse(fecha_nac).ToString("yyyy-MM-dd");
            string sql = "UPDATE clientes SET cedula='" + cedula + "', nombre='" + nombre + "', apellido='" + apellido + "', direccion='" + direccion + "', email='" + email + "', telefono='" + telefono + "', fecha='" + fecha + "' WHERE id='" + id + "';";
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            try
            {

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                exito = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return exito;
        }
        public static bool Eliminar(string id)
        {
            bool exito = false;
            
            string sql = "DELETE FROM clientes WHERE id='" + id + "';";
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            try
            {

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                exito = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return exito;
        }
        public static bool Verificar( string cedula)
        {
            bool exito = false;
            string sql = "SELECT * FROM clientes WHERE cedula='" + cedula + "';";
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            try
            {
                int rowAffect = -1;
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader my = cmd.ExecuteReader();
                if (my.Read()) { }
                {
                    rowAffect = my.GetInt32(0);
                }
                exito = 0 != rowAffect? true:false;

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return exito;
        }

        public static bool Validar(string identificacion)
        {
            bool estado = false;
            char[] valced = new char[13];
            int provincia;
            if (identificacion.Length >= 10)
            {
                valced = identificacion.Trim().ToCharArray();
                provincia = int.Parse((valced[0].ToString() + valced[1].ToString()));
                if (provincia > 0 && provincia < 25)
                {
                    if (int.Parse(valced[2].ToString()) < 6)
                    {
                        estado = VerificaCedula(valced);
                    }
                    else if (int.Parse(valced[2].ToString()) == 6)
                    {
                        estado = VerificaSectorPublico(valced);
                    }
                    else if (int.Parse(valced[2].ToString()) == 9)
                    {

                        estado = VerificaPersonaJuridica(valced);
                    }
                }
            }
            return estado;
        }

        public static bool VerificaCedula(char[] validarCedula)
        {
            int aux = 0, par = 0, impar = 0, verifi;
            for (int i = 0; i < 9; i += 2)
            {
                aux = 2 * int.Parse(validarCedula[i].ToString());
                if (aux > 9)
                    aux -= 9;
                par += aux;
            }
            for (int i = 1; i < 9; i += 2)
            {
                impar += int.Parse(validarCedula[i].ToString());
            }

            aux = par + impar;
            if (aux % 10 != 0)
            {
                verifi = 10 - (aux % 10);
            }
            else
                verifi = 0;
            if (verifi == int.Parse(validarCedula[9].ToString()))
                return true;
            else
                return false;
        }
        public static bool VerificaSectorPublico(char[] validarCedula)
        {
            int aux = 0, prod, veri;
            veri = int.Parse(validarCedula[9].ToString()) + int.Parse(validarCedula[10].ToString()) + int.Parse(validarCedula[11].ToString()) + int.Parse(validarCedula[12].ToString());
            if (veri > 0)
            {
                int[] coeficiente = new int[8] { 3, 2, 7, 6, 5, 4, 3, 2 };

                for (int i = 0; i < 8; i++)
                {
                    prod = int.Parse(validarCedula[i].ToString()) * coeficiente[i];
                    aux += prod;
                }

                if (aux % 11 == 0)
                {
                    veri = 0;
                }
                else if (aux % 11 == 1)
                {
                    return false;
                }
                else
                {
                    aux = aux % 11;
                    veri = 11 - aux;
                }

                if (veri == int.Parse(validarCedula[8].ToString()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static bool VerificaPersonaJuridica(char[] validarCedula)
        {
            int aux = 0, prod, veri;
            veri = int.Parse(validarCedula[10].ToString()) + int.Parse(validarCedula[11].ToString()) + int.Parse(validarCedula[12].ToString());
            if (veri > 0)
            {
                int[] coeficiente = new int[9] { 4, 3, 2, 7, 6, 5, 4, 3, 2 };
                for (int i = 0; i < 9; i++)
                {
                    prod = int.Parse(validarCedula[i].ToString()) * coeficiente[i];
                    aux += prod;
                }
                if (aux % 11 == 0)
                {
                    veri = 0;
                }
                else if (aux % 11 == 1)
                {
                    return false;
                }
                else
                {
                    aux = aux % 11;
                    veri = 11 - aux;
                }

                if (veri == int.Parse(validarCedula[9].ToString()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
