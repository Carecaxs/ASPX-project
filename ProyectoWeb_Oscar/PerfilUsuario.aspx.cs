using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoWeb_Oscar
{
    public partial class PerfilUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarGrid();

                if (Session["nombre"] != null)
                {
                    string nombre = Session["nombre"].ToString();
                    usuario.InnerText = "Bienvenido, " + nombre;
                }
            }
        
        }

        private void cargarGrid()
        {
            try
            {
                //va recibir un valor por sesion que va ser el id de la persona usuario
                int idUsuario = int.Parse(Session["id"].ToString());

                BD_ProyectoDataContext dataContext = new BD_ProyectoDataContext();
                //en esta consulta LINQ se consulta por los pedidos de este usuario
                var consulta = (from pedido in dataContext.PEDIDOS
                               join detalle in dataContext.DETALLE_PEDIDOS on pedido.ID_PEDIDO equals detalle.ID_PEDIDO
                               join usuario in dataContext.USUARIOS on pedido.ID_USUARIO equals usuario.ID_USUARIO
                               join suplemento in dataContext.SUPLEMENTOS on detalle.ID_SUPLEMENTO equals suplemento.ID_SUPLEMENTO // Reemplaza "SUPLEMENTOS" con el nombre de tu tabla de suplementos
                               where usuario.ID_USUARIO == idUsuario
                               select new
                               {
                                   Nombre = usuario.NOMBRE,
                                   FechaPedido = pedido.FECHA_PEDIDO,
                                   NombreSuplemento = suplemento.NOMBRE, // Reemplaza "NOMBRE" con el nombre de la columna que contiene el nombre del suplemento
                                   Precio = suplemento.PRECIO * detalle.CANTIDAD_SOLICITADA,
                                   CantidadSolicitada = detalle.CANTIDAD_SOLICITADA,
                                   EstadoPedido = pedido.ESTADO
                               }).ToList();




                if (consulta.Any())
                {
                    var consultaFormateada = consulta.Select(c => new
                    {
                        Nombre = c.Nombre,
                        FechaPedido = c.FechaPedido.ToString("yyyy-MM-dd"),
                        NombreSuplemento = c.NombreSuplemento,
                        CantidadSolicitada = c.CantidadSolicitada,
                        Precio = Math.Round(c.Precio * c.CantidadSolicitada, 2),
                        EstadoPedido = c.EstadoPedido
                    }).ToList();

                    grdDatos.DataSource = consultaFormateada;
                    grdDatos.DataBind();
                }
                else
                {
                    lblTexto.Visible = true;
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
    }
}