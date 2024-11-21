using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoWeb_Oscar
{
    public partial class Pedidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarGrid();

                Session["idDetallePedidos"] = null;//esta sesion se va usar cuando el usuario quiere editar algun pedido existente
                //se envia el id a la otra pagina para que se carguen los datos del pedido 

                if (Session["nombre"] != null)//por sesiones tambien se envian los nombres de los usuarios para mostrar una bienvenida con el nombre
                {
                    string nombre = Session["nombre"].ToString();
                    usuario.InnerText = "Pedidos de " + nombre;
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
                                join suplemento in dataContext.SUPLEMENTOS on detalle.ID_SUPLEMENTO equals suplemento.ID_SUPLEMENTO
                                where usuario.ID_USUARIO == idUsuario
                                select new
                                {
                                    id=detalle.ID_DETALLE_PEDIDO,
                                    Nombre = usuario.NOMBRE,
                                    FechaPedido = pedido.FECHA_PEDIDO,
                                    NombreSuplemento = suplemento.NOMBRE,
                                    CantidadSolicitada = detalle.CANTIDAD_SOLICITADA,
                                    Precio = suplemento.PRECIO * detalle.CANTIDAD_SOLICITADA,
                                    EstadoPedido = pedido.ESTADO
                                }).ToList();




                if (consulta.Any())
                {
           
                    var consultaFormateada = consulta.Select(c => new
                    {
                        id=c.id,
                        Nombre = c.Nombre,
                        FechaPedido = c.FechaPedido.ToString("yyyy-MM-dd"),
                        NombreSuplemento = c.NombreSuplemento,
                        CantidadSolicitada = c.CantidadSolicitada,
                        Precio = Math.Round(c.Precio * c.CantidadSolicitada,2),
                        EstadoPedido = c.EstadoPedido
                    }).ToList();
                 



                    GridViewPedidos.DataSource = consultaFormateada;
                    GridViewPedidos.DataBind();

                }
                else
                {
                    lblTexto.Visible = true;
                }

            }
            catch (Exception)
            {

                throw;
            }


        }

        protected void btnEditar_Command(object sender, CommandEventArgs e)//evento al seleccionar el boton editar de la tabla
        {
            int idDetallePedidos = int.Parse(e.CommandArgument.ToString());
            Session["idDetallePedidos"] = idDetallePedidos;//se le manda el id a la sesion
            Response.Redirect("CrearPedido.aspx");

        }
    }

}