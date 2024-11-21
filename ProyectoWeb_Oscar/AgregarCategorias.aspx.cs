using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoWeb_Oscar
{
    public partial class AgregarCategorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarGrid();
            }
        
        }

        private void cargarGrid()
        {
            try
            {


                BD_ProyectoDataContext dataContext = new BD_ProyectoDataContext();
                //en esta consulta LINQ se consulta por los pedidos de este usuario
                var consulta = from c in dataContext.CATEGORIAS
                               select new
                               {
                                   ID = c.ID_CATEGORIA,
                                   Nombre = c.NOMBRE
                               };





                if (consulta.Any())//si contiene algun registro
                {
                    grdCategorias.DataSource = consulta;
                    grdCategorias.DataBind();
                    lblTexto.Visible = false;

                }
                else
                {
                    lblTexto.Visible = true;//muestra un texto de que no hay registros
                }

            }
            catch (Exception ex)
            {
                divError.Visible = true;
                divError.Attributes["class"] = divError.Attributes["class"].Replace("hidden", "");
                msjSP_Error.Text = ex.Message;
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {

            AgregarCategoria();
            cargarGrid();
        }

        private void AgregarCategoria()
        {
            try
            {
                divFormato.Visible = false;
                divError.Visible = false;

                BD_ProyectoDataContext dataContext = new BD_ProyectoDataContext();
                string nombre = txtNombre.Text; ;
                string msj = string.Empty;

                dataContext.SP_AGREGAR_CATEGORIA(nombre.ToUpper(), ref msj);
                dataContext.SubmitChanges();

                divFormato.Visible = true;
                divFormato.Attributes["class"] = divFormato.Attributes["class"].Replace("hidden", "");
                msjSP.Text = msj;





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