using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ProyectoWeb_Oscar
{
    public partial class MostrarSuplementos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarSuplementos();
                Session["acceso"] = "pedidos";

            }
        }

        private void CargarSuplementos()
        {
            try
            {
                BD_ProyectoDataContext dataContext = new BD_ProyectoDataContext();

                //obtener los todos los registros de la tabla suplementos
                var consulta = dataContext.SUPLEMENTOS;

                foreach (var suplemento in consulta)
                {
                    string nombre = suplemento.NOMBRE;
                    string rutaImagen = suplemento.RUTA_IMAGEN;
                    string descripcion = suplemento.DESCRIPCION;


                    // Crear una tarjeta para cada suplemento
                    HtmlGenericControl card = new HtmlGenericControl("div");
                    card.Attributes["class"] = "card";
                    card.Attributes["style"] = "width: 25rem; margin-top:2rem; margin-bottom:1rem; margin-left:1rem;"; // Establecer el ancho de la tarjeta, margenes arriba y abajo

                    // Agregar la imagen del suplemento
                    Image image = new Image();
                    image.CssClass = "card-img-top card-img-fixed";//se le agrega clases
                    if (string.IsNullOrEmpty(rutaImagen))
                    {
                        // Si la ruta de la imagen está vacía, establecer una imagen por defecto
                        image.ImageUrl = "imagenes/suplements-stack.jpg";
                    }
                    else
                    {
                        // Si la ruta de la imagen no está vacía, usar la ruta proporcionada
                        image.ImageUrl = rutaImagen;
                    }
                    card.Controls.Add(image);

                    // Agregar el cuerpo de la tarjeta
                    HtmlGenericControl cardBody = new HtmlGenericControl("div");
                    cardBody.Attributes["class"] = "card-body";

                    // Agregar el título del suplemento
                    HtmlGenericControl cardTitle = new HtmlGenericControl("h5");
                    cardTitle.Attributes["class"] = "card-title";
                    cardTitle.InnerText = nombre;
                    cardBody.Controls.Add(cardTitle);

                    // Agregar el párrafo de ejemplo
                    HtmlGenericControl cardText = new HtmlGenericControl("p");
                    cardText.Attributes["class"] = "card-text";
                    cardText.InnerText = descripcion;
                    cardBody.Controls.Add(cardText);

                    // Agregar el enlace
                    HtmlGenericControl link = new HtmlGenericControl("a");
                    link.Attributes["class"] = "btn btn-dark";
                    link.Attributes["href"] = "Usuarios.aspx";
                    link.InnerText = "Realizar Pedido";
                    cardBody.Controls.Add(link);

                    card.Controls.Add(cardBody);

                    // Agregar la tarjeta al contenedor
                    suplementosContainer.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                divError.Attributes["class"] = divError.Attributes["class"].Replace("hidden", "");
                msjSP_Error.Text = ex.Message;
            }

        }
    }
}