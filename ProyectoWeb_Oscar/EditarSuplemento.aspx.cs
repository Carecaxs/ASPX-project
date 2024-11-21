using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoWeb_Oscar
{
    public partial class EditarSuplemento : System.Web.UI.Page
    {
        //variable global que guarda el id del suplemento a modificar
        int idGlobal = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["IdGlobal"] = null;
                CargarGrid();
            }
        }

        protected void btnPasarDatos_Command(object sender, CommandEventArgs e)//mediante el boton editar del grid view se van a pasar los datos a los txt del suplemento seleccionado
        {
            idGlobal = int.Parse(e.CommandArgument.ToString());//se guada el id del suplemento seleccionado
            Session["IdGlobal"] = idGlobal;//se guarda el id en la sesion

            if (idGlobal > 0)
            {
                try
                {
                    //obetener los datos del suplemento mediante el id 
                    BD_ProyectoDataContext dataContext = new BD_ProyectoDataContext();
                    var consulta = (from s in dataContext.SUPLEMENTOS
                                    where s.ID_SUPLEMENTO == idGlobal
                                    select new { s.NOMBRE, s.DESCRIPCION, s.PRECIO, s.CANTIDAD}).FirstOrDefault();

                    if (consulta != null)
                    {
                        string nombre = consulta.NOMBRE;
                        int cantidad = consulta.CANTIDAD;
                        decimal precio = consulta.PRECIO;
                        string descripcion = consulta.DESCRIPCION;
                 

                        txtNombre.Text = nombre;
                        txtCantidad.Text = cantidad.ToString();
                        txtPrecio.Text = Convert.ToInt32(precio).ToString();
                        txtDescripcion.Value = descripcion.ToString();

   

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

        private void CargarGrid()
        {
            try
            {
                BD_ProyectoDataContext dataContext = new BD_ProyectoDataContext();

                var vistaSuplementosCategorias = dataContext.SUPLEMENTOS;

                if (vistaSuplementosCategorias.Any())
                {
                    grdSuplementos.DataSource = vistaSuplementosCategorias;
                    grdSuplementos.DataBind();
                    lblTexto.Visible = false;

                }
                else
                {
                    lblTexto.Visible = true;
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
            CargarGrid();
        }



  

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            divFormato.Visible = false;
            divError.Visible = false;

            try
            {
                BD_ProyectoDataContext dataContext = new BD_ProyectoDataContext();
                var consulta = from s in dataContext.SUPLEMENTOS
                               join sc in dataContext.SUPLEMENTOS_CATEGORIAS on s.ID_SUPLEMENTO equals sc.ID_SUPLEMENTO
                               join c in dataContext.CATEGORIAS on sc.ID_CATEGORIA equals c.ID_CATEGORIA
                               where s.NOMBRE.Contains(txtBuscar.Text)
                               select new
                               {
                                   ID_SUPLEMENTO = s.ID_SUPLEMENTO,
                                   NOMBRE = s.NOMBRE,
                                   DESCRIPCION = s.DESCRIPCION,
                                   PRECIO = s.PRECIO,
                                   CANTIDAD = s.CANTIDAD,
                                   RUTA_IMAGEN=s.RUTA_IMAGEN
                               };



                if (consulta.Any())
                {
                    var consultaFormateada = consulta.ToList(); // Ejecutar la consulta y obtener los resultados

                    // Redondear el precio en los resultados
                    consultaFormateada = consultaFormateada.Select(c => new
                    {
                        ID_SUPLEMENTO = c.ID_SUPLEMENTO,
                        NOMBRE = c.NOMBRE,
                        DESCRIPCION = c.DESCRIPCION,
                        PRECIO = Math.Round(c.PRECIO, 2),
                        CANTIDAD = c.CANTIDAD,
                        RUTA_IMAGEN=c.RUTA_IMAGEN
                    }).ToList();


                    grdSuplementos.DataSource = consultaFormateada;
                    grdSuplementos.DataBind();
                    grdSuplementos.Visible = true;
                    lblTexto.Visible = false;

                }
                else
                {
                    grdSuplementos.Visible = false;
                    lblTexto.Visible = true;
                }
            }
            catch (Exception ex)
            {

                divError.Visible = true;
                divError.Attributes["class"] = divError.Attributes["class"].Replace("hidden", "");
                msjSP_Error.Text = ex.Message;
            }
        }

        protected void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            grdSuplementos.Visible = true;
            CargarGrid();

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Editar();
            CargarGrid();

        }

        private void Editar()
        {
            divFormato.Visible = false;
            divError.Visible = false;
            try
            {
                BD_ProyectoDataContext dataContext = new BD_ProyectoDataContext();



                string rutaImagen = string.Empty;
                if (fileImagen.HasFile)//si se ingresa una imagen
                {
                    string rutaCarpetaDestino = Server.MapPath("~/imagenes/"); // Ruta de la carpeta destino en el servidor
                                                                               // Obtén la ruta completa del archivo
                    string rutaCompletaArchivo = Path.Combine(rutaCarpetaDestino, fileImagen.FileName);

                    if (File.Exists(rutaCompletaArchivo))//si ya existe ese nombre de imagen entra 
                    {
                        string nombreUnico = Path.GetFileNameWithoutExtension(fileImagen.FileName);
                        string extensionArchivo = Path.GetExtension(fileImagen.FileName);
                        int contador = 1;
                        do//en este do while se le va agregando un valor numerico al nombre de la imagen hasta que se compruebe que no exita
                        {
                            nombreUnico = $"{nombreUnico}_{contador}{extensionArchivo}";
                            rutaCompletaArchivo = Path.Combine(rutaCarpetaDestino, nombreUnico);
                            contador++;
                        } while (File.Exists(rutaCompletaArchivo));

                        rutaImagen = string.Format("imagenes/{0}", nombreUnico);
                    }
                    else
                    {
                        //obtener el nombre de la imagen
                        rutaImagen = string.Format("imagenes/{0}", fileImagen.FileName);
                    }
                    idGlobal = (int)Session["IdGlobal"];

                    string consulta = (from c in dataContext.SUPLEMENTOS
                                    where c.ID_SUPLEMENTO==idGlobal
                                    select c.RUTA_IMAGEN).FirstOrDefault();

                    if (!string.IsNullOrEmpty(consulta)) //si ese suplemento ya tenia guardada una imagen se elimina de la carpeta
                    {
                        string rutaCompletaImagenVieja = Path.Combine("C:\\FASE3_OSCAR\\PaginaWeb\\ProyectoWeb_Oscar\\", consulta);

                        if (File.Exists(rutaCompletaImagenVieja))
                        {
                            File.Delete(rutaCompletaImagenVieja);
                            // El archivo se ha eliminado exitosamente
                        }
                    }

                    fileImagen.SaveAs(rutaCompletaArchivo); // Guarda el archivo en la carpeta destino



                }


                string descripcion = txtDescripcion.Value; 
                Decimal precio = decimal.Parse(txtPrecio.Text);
                int cantidad = int.Parse(txtCantidad.Text);
                string msj = string.Empty;


                if (!string.IsNullOrEmpty(txtNombre.Text) &&
                    decimal.TryParse(txtPrecio.Text, out precio) &&
                    int.TryParse(txtCantidad.Text, out cantidad))
                {
                    dataContext.SP_EDITAR_SUPLEMENTO(idGlobal, txtNombre.Text.ToUpper(), descripcion.ToUpper(), precio, cantidad, ref msj, rutaImagen);
                    dataContext.SubmitChanges();
          


                    divFormato.Visible = true;
                    divFormato.Attributes["class"] = divFormato.Attributes["class"].Replace("hidden", "");
                    msjSP.Text = msj;

                    txtNombre.Text = string.Empty;
                    txtPrecio.Text = string.Empty;
                    txtDescripcion.Value = string.Empty;
                    txtCantidad.Text = string.Empty;
                    txtBuscar.Text = string.Empty;



                }
                else
                {
                    divFormato.Visible = true;
                    divFormato.Attributes["class"] = divFormato.Attributes["class"].Replace("hidden", "");
                    msjSP.Text = "Debes llenar los campos";

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