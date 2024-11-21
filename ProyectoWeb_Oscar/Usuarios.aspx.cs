using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoWeb_Oscar
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        private void ComprobarUsuario()
        {
            try
            {
                BD_ProyectoDataContext dataContext = new BD_ProyectoDataContext();
                var consulta = from usuario in dataContext.USUARIOS
                               where usuario.CORREO == txtUsuario.Text && usuario.CLAVE == txtContraseña.Text
                               select usuario;

                if (consulta.Any())
                {
                    int userId = (from usuario in dataContext.USUARIOS
                                  where usuario.CORREO == txtUsuario.Text && usuario.CLAVE == txtContraseña.Text
                                  select usuario.ID_USUARIO).FirstOrDefault();

                    var nombre = (from usuario in dataContext.USUARIOS
                                  where usuario.CORREO == txtUsuario.Text && usuario.CLAVE == txtContraseña.Text
                                  select usuario.NOMBRE).FirstOrDefault();


                    Session["id"] = userId.ToString();
                    Session["nombre"] = nombre.ToString();

                    //en este if se comprueba de donde se ingreso a esta pagina
                    if (Session["acceso"].ToString() == "usuario")
                    {
                        Response.Redirect("PerfilUsuario.aspx");
                    }
                    else if (Session["acceso"].ToString() == "pedidos")
                    {

                        Response.Redirect("Pedidos.aspx");
                    }


                }
                else
                {
                    msjError.Attributes["class"] = msjError.Attributes["class"].Replace("hidden", "");

                }
            }
            catch (Exception ex)
            {
                // Llamar a la función JavaScript para mostrar la alerta
                string errorMessage = "Error: " + ex.Message.Replace("'", "\\'");
                string script = "mostrarAlerta('" + errorMessage + "');";
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", script, true);
            }

            
        }

        protected void btnIngreasr_Click(object sender, EventArgs e)
        {
            ComprobarUsuario();
        }

        protected void btnCrearUsuario_Click(object sender, EventArgs e)
        {


            Response.Redirect("CrearUsuario.aspx");

        }
    }
}