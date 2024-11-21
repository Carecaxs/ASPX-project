using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoWeb_Oscar
{
    public partial class AgregarSuplemento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
  
                // Llamar a la función para obtener las categorías desde la base de datos
                List<CATEGORIAS> categorias = ObtenerCategorias();

                // Asignar las categorías al DropDownList (ddlCategoria)
                ddlCategorias.DataSource = categorias;
                ddlCategorias.DataTextField = "NOMBRE"; // Nombre de la columna que contiene el nombre de la categoría en la tabla
                ddlCategorias.DataValueField = "ID_CATEGORIA"; // Nombre de la columna que contiene el ID de la categoría en la tabla
                ddlCategorias.DataBind();

                MostrarDatosVistaSuplementosCategorias();


            }
      

        }

        private List<CATEGORIAS> ObtenerCategorias()
        {
            try
            {
                BD_ProyectoDataContext dataContext = new BD_ProyectoDataContext();
                var categorias = dataContext.CATEGORIAS.ToList();
                return categorias;

            }
            catch (Exception ex)
            {
                divError.Visible = true;
                divError.Attributes["class"] = divError.Attributes["class"].Replace("hidden", "");
                msjSP_Error.Text = ex.Message;
                return new List<CATEGORIAS>();
            }
        }

        protected void MostrarDatosVistaSuplementosCategorias()
        {
            BD_ProyectoDataContext dataContext = new BD_ProyectoDataContext();

            var vistaSuplementosCategorias = dataContext.VistaSuplementos;

            // Enlazar los datos al GridView
  

            if (vistaSuplementosCategorias.Any())//si contiene algun registro
            {
                grdSuplementos.DataSource = vistaSuplementosCategorias;
                grdSuplementos.DataBind();//cargar en la gridview
                lblTexto.Visible = false;

            }
            else
            {
                lblTexto.Visible = true;//mostrar msj no hya registros
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            divError.Visible = false;
            divFormato.Visible = false;
            Agregar();//metodo para agregar suplemento
            MostrarDatosVistaSuplementosCategorias();//mostrar vista

        }

        private void Agregar()//agregar suplemento
        {
            try
            {
                divError.Visible = false;
                divFormato.Visible = false;

                BD_ProyectoDataContext dataContext = new BD_ProyectoDataContext();

                //accedo a los controles txt del html para poder enviarlos al spAgregarSuplemento
                string nombre = txtNombre.Text;
                int categoria = int.Parse(ddlCategorias.SelectedValue);
                string descripcion = Request.Form["txtDescripcion"];
                Decimal precio = decimal.Parse(txtPrecio.Text);
                int cantidad = int.Parse(txtCantidad.Text);
                string msj = string.Empty;
                string rutaImagen = string.Empty;

                if (fileImagen.HasFile)//si se ingresa una imagen
                {
                    string rutaCarpetaDestino = Server.MapPath("~/imagenes/"); // Ruta de la carpeta destino en el servidor donde quiero guardarlas
                                                                            
                    string rutaCompletaArchivo = Path.Combine(rutaCarpetaDestino, fileImagen.FileName);// obtener la ruta completa del archivo

                    if (File.Exists(rutaCompletaArchivo))//si ya existe ese nombre de imagen entra 
                    {
                        string nombreUnico = Path.GetFileNameWithoutExtension(fileImagen.FileName);//se obtiene el nombre
                        string extensionArchivo = Path.GetExtension(fileImagen.FileName);//se obtiene la extension, ejemplo:jpg
                        int contador = 1;
                        do//en este do while se le va agregando un avlor numerico al nombre de la imagen hasta que se compruebe que no exita
                        {
                            nombreUnico = $"{nombreUnico}_{contador}{extensionArchivo}";
                            rutaCompletaArchivo = Path.Combine(rutaCarpetaDestino, nombreUnico);
                            contador++;
                        } while (File.Exists(rutaCompletaArchivo));//el ciclo termina hasta que no exista ese nombre para que sea unico

                        rutaImagen = string.Format("imagenes/{0}", nombreUnico);
                        //esta ruta se utiliza para guardarla en la BD en la tabla de suplementos
                    }
                    else
                    {
                        //esta ruta se utiliza para guardarla en la BD en la tabla de suplementos
                        rutaImagen = string.Format("imagenes/{0}", fileImagen.FileName);
                    }
        
            

                    fileImagen.SaveAs(rutaCompletaArchivo); // Guarda la imagen en la carpeta destino
                }

                    if (!string.IsNullOrEmpty(nombre) && int.TryParse(ddlCategorias.SelectedValue, out categoria) &&
                    decimal.TryParse(txtPrecio.Text, out precio) &&
                    int.TryParse(txtCantidad.Text, out cantidad))//comprobar que los campos no esten vacios
                {
                    dataContext.SP_AGREGAR_SUPLEMENTO(categoria, nombre.ToUpper(), descripcion.ToUpper(), precio, cantidad, ref msj, rutaImagen);
                    dataContext.SubmitChanges();

                    divFormato.Visible = true;
                    divFormato.Attributes["class"] = divFormato.Attributes["class"].Replace("hidden", "");
                    msjSP.Text = msj;
                    txtPrecio.Text = string.Empty;

               
                }
                else
                {
                    divError.Visible = true;
                    divError.Attributes["class"] = divError.Attributes["class"].Replace("hidden", "");
                    msjSP_Error.Text = "Debes llenar los campos";

                }
                 
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                divError.Attributes["class"] = divError.Attributes["class"].Replace("hidden", "");
                msjSP_Error.Text = ex.Message;
            }



        }

        protected void grdSuplementos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSuplementos.PageIndex = e.NewPageIndex;
            MostrarDatosVistaSuplementosCategorias();
        }
    }
}