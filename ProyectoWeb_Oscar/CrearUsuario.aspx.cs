using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ProyectoWeb_Oscar
{
    public partial class CrearUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void ComprobarContraseña()
        {
    
            //forma de acceder a los controles html, en este caso inputs mediante el name
            string clave1 = Request.Form["contrasena"];
            string clave2 = Request.Form["confirmar_contrasena"];


            if (clave1 == clave2)
            {
                AgregarUsuario();
            }
            else
            {
                //se elimina la clase hidden que tiene este div para que se muestre el mensaje de error 
                msjError.Attributes["class"] = msjError.Attributes["class"].Replace("hidden", "");
                lblMensaje.Text = "Las contraseñas no coinciden!!";


            }



        }

        private void AgregarUsuario()
        {
            //accedo a los controles del html
            string nombre = Request.Form["nombre"];
            string clave1 = Request.Form["contrasena"];
            string correo = Request.Form["correo"];

            string msj = string.Empty;
    

            BD_ProyectoDataContext dataContext = new BD_ProyectoDataContext();

            //comprobar si ese usuario ya existe
            //esta consulta linq retorna true o false
            bool existeUsuario = dataContext.USUARIOS.Any(u => u.CORREO == correo && u.CLAVE == clave1);

            if (!existeUsuario)//si no existe
            {
                //llamo al sp de agregar al usuario, el msj va obtener el id generado
                dataContext.SP_AGREGAR_USUARIO(nombre, correo, clave1, ref msj);
                dataContext.SubmitChanges();

                //envio el id por sesion
                Session["id"] = msj;
                Session["nombre"] = nombre;

                //mediante sesiones se va a mandar una palabra clave para que la otra pagina sepa de cual pagina es invocada
                //esta pagina se va utilizar mucho ya que es la de ingreso 
                if (Session["acceso"].ToString() == "usuario")
                {//significa que viene del boton usuarios entonces la persona quiere acceder al menu de usuarios
                    Session["acceso"] = "usuario";
                    Response.Redirect("PerfilUsuario.aspx");
                }
                else if (Session["acceso"].ToString() == "pedidos")
                {//significa que viene del boton pedidos entonces el usuario quiere acceder a los pedidos personales
                    Session["acceso"] = "pedidos";
                    Response.Redirect("Pedidos.aspx");
                }

               
            }
            else
            {
                lblMensaje.Text = "El usuario ya existe!!";
                //se elimina la clase hidden que tiene este div para que se muestre el mensaje de error 
                msjError.Attributes["class"] = msjError.Attributes["class"].Replace("hidden", "");
            }


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ComprobarContraseña();
        }
    }
}