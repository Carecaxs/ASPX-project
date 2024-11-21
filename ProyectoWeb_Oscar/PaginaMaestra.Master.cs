using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ProyectoWeb_Oscar
{
    public partial class PaginaMaestra : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        public List<CATEGORIAS> ObtenerCategoriasDesdeLaBaseDeDatos()
        {
            BD_ProyectoDataContext dataContext = new BD_ProyectoDataContext();
            var categorias = dataContext.CATEGORIAS.ToList();
            return categorias;
        }

        protected void btnPedidos_Click(object sender, EventArgs e)
        {
            Session["acceso"] = "pedidos";
            Response.Redirect("Usuarios.aspx");
        }

        protected void btnUsuarios_Click(object sender, EventArgs e)
        {
            Session["acceso"] = "usuario";
            Response.Redirect("Usuarios.aspx");
 
        }
    }
}