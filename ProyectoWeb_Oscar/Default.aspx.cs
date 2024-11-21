using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoWeb_Oscar
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)//evento del boton crear usuario
        {
            Response.Redirect("CrearUsuario.aspx");
            Session["acceso"] = "usuario";

        }

        protected void btnVerSuplementos_Click(object sender, EventArgs e)//evento del boton ver suplementos
        {
            Response.Redirect("MostrarSuplementos.aspx");

        }

        protected void btnPedidos_Click(object sender, EventArgs e)//evento del boton pedidos
        {
            Session["acceso"] = "pedidos";
            Response.Redirect("Usuarios.aspx");


        }
    }
}