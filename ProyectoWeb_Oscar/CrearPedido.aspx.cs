using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoWeb_Oscar
{
    public partial class CrearPedido : System.Web.UI.Page
    {

        int idGlobal = -1;// variable global que se usa para guardar el id del suplemento


        //crear una clase para guardar los productos que quiere ingresar el usuario:
        // Clase SuplementoPedido
        public class SuplementoPedido
        {
            public int Suplemento { get; set; }
            public int Cantidad { get; set; }
            public int Precio { get; set; }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["IdGlobal"] = null;//variable que va guardar el id del suplemento a guadar, la idea era usar la variable global pero esta pierde su valor al interactuar con los botones

                MostrarDatosVistaSuplementosCategorias();
                // Inicializar la lista de pedidos en la sesión
                if (Session["ListaPedido"] == null) //al cargar la pagina por primera vez se instancia una clase lista que contenga elementpos del objeto creado anteriormente, aca se van a guardar los suplementos, precios y cantidades del pedido del usuario y se va guardar en una sesion para no perder los registros
                {
                    List<SuplementoPedido> listaPedido = new List<SuplementoPedido>();
                    Session["ListaPedido"] = listaPedido;
                }

                if (Session["idDetallePedidos"] != null)//la pagina que redirige a esta va mandar esta sesion null o con un id dependiendo de lo que se quiera hacer, la manda con un id si lo que se quiere es editar el pedido existente
                {
                    btnEditar.Visible = true;
                    btnAgregar.Visible = false;
                    btnReset.Visible = false;
                    btnRealizarPedido.Visible = false;
                    pasarDatos();//se pasan los datos del pedido a editar a los txt

                    BD_ProyectoDataContext dataContext = new BD_ProyectoDataContext();
    
                    Session["IdGlobal"]= dataContext.SUPLEMENTOS.Where(s => s.NOMBRE == txtSuplemento.Text).Select(s => s.ID_SUPLEMENTO).FirstOrDefault();//se busca el id del suplemento para guardarlo en la sesion 
                }

            }
        }

        protected void MostrarDatosVistaSuplementosCategorias()
        {
            try
            {
                BD_ProyectoDataContext dataContext = new BD_ProyectoDataContext();

                var vistaSuplementosCategorias = dataContext.VistaSuplementos;

                // Enlazar los datos al GridView


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
                divError.Visible = true;//msj error
                divError.Attributes["class"] = divError.Attributes["class"].Replace("hidden", "");
                msjSP_Error.Text = ex.Message;
            }
        }

        protected void grdSuplementos_PageIndexChanging(object sender, GridViewPageEventArgs e)//evento de varias paginas en el gridview
        {
            grdSuplementos.PageIndex = e.NewPageIndex;
            MostrarDatosVistaSuplementosCategorias();
        }


        protected void Button1_Command(object sender, CommandEventArgs e)//boton de seleccionar en el grid
        {
            idGlobal = int.Parse(e.CommandArgument.ToString());//se le asigna el id del duplemento seleccionado en la tabla
            Session["IdGlobal"] = idGlobal; //guarda id de suplemento

            if (idGlobal > 0)
            {
                try
                {
                    BD_ProyectoDataContext dataContext = new BD_ProyectoDataContext();
                    //obtener nombre y precio del suplemento con el id guardado en idGlobal
                    var consulta = (from s in dataContext.SUPLEMENTOS
                                    where s.ID_SUPLEMENTO == idGlobal
                                    select new { s.NOMBRE, s.PRECIO }).FirstOrDefault();

                    if (consulta != null)//si contiene un registro esos valores se le asignan a los txt
                    {
                        string nombre = consulta.NOMBRE;
                        int precio = (int)consulta.PRECIO;

                        txtSuplemento.Text = nombre;
                        txtCantidad.Text = "1";//cantidad por default se va asignar que una
                        txtPrecio.Text = precio.ToString();
                    }
                }
                catch (Exception ex)
                {

                    divError.Visible = true;//msj error
                    divError.Attributes["class"] = divError.Attributes["class"].Replace("hidden", "");
                    msjSP_Error.Text = ex.Message;
                }
            }
        }

        protected void txtCantidad_TextChanged(object sender, EventArgs e)//evento al cambiar la cantidad se actualice el precio total
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSuplemento.Text) && !string.IsNullOrEmpty(txtCantidad.Text))
                {
                    idGlobal = int.Parse(Session["IdGlobal"].ToString());

                    BD_ProyectoDataContext dataContext = new BD_ProyectoDataContext();

                    var precio = (from s in dataContext.SUPLEMENTOS
                                  where s.ID_SUPLEMENTO == idGlobal
                                  select new { s.PRECIO }).FirstOrDefault();

                    int cantidad = int.Parse(txtCantidad.Text);
                    int precioText = Convert.ToInt32(precio.PRECIO);
                    precioText *= cantidad;

                    txtPrecio.Text = precioText.ToString();

                }
                else
                {
                    txtPrecio.Text = string.Empty;

                }

            }
            catch (Exception ex)
            {

                divError.Visible = true;//msj error
                divError.Attributes["class"] = divError.Attributes["class"].Replace("hidden", "");
                msjSP_Error.Text = ex.Message;
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            divFormato.Visible = false;//msj exito
            divError.Visible = false;//msj error

            //comprobar que los valores no esten vacios
            if (Session["IdGlobal"]!=null && !string.IsNullOrEmpty(txtCantidad.Text) && !string.IsNullOrEmpty(txtPrecio.Text))
            {
                // Obtener los valores ingresados
                int suplemento = int.Parse(Session["IdGlobal"].ToString());
                int cantidad = int.Parse(txtCantidad.Text);
                int precio = int.Parse(txtPrecio.Text);


                try
                {
                    //consultar si la cantidad pedida no es mas de la cantidad existente
                    BD_ProyectoDataContext dataContext = new BD_ProyectoDataContext();
                    int cantidadExistente = (from s in dataContext.SUPLEMENTOS where s.ID_SUPLEMENTO == suplemento select s.CANTIDAD).FirstOrDefault();

                    if (cantidad <= cantidadExistente)//si la cantidad pedida es menor o igual
                    {
                        // Obtener la lista de pedidos de la sesión
                        List<SuplementoPedido> listaPedido = (List<SuplementoPedido>)Session["ListaPedido"];

                        // Crear una instancia de SuplementoPedido y agregarla a la listaPedido
                        SuplementoPedido suplementoPedido = new SuplementoPedido()
                        {
                            Suplemento = suplemento,
                            Cantidad = cantidad,
                            Precio = precio
                        };
                        listaPedido.Add(suplementoPedido);


                        divFormato.Visible = true;//msj exito
      
                        divFormato.Attributes["class"] = divFormato.Attributes["class"].Replace("hidden", "");
                        msjSP.Text = "Producto añadido";


                        MostrarDatosVistaSuplementosCategorias();//volver a cargar el gridview

                    }
                    else
                    {

                        divError.Visible = true;//msj error
                        divError.Attributes["class"] = divError.Attributes["class"].Replace("hidden", ""); //mostrar div de error
                        msjSP_Error.Text = "Cantidad insuficiente en inventario, no se añadio el producto";
                    }




                    // Limpiar los campos
                    txtSuplemento.Text = string.Empty;
                    txtCantidad.Text = string.Empty;
                    txtPrecio.Text = string.Empty;

                }
                catch (Exception ex)
                {
  
                    divError.Visible = true;//msj error
                    divError.Attributes["class"] = divError.Attributes["class"].Replace("hidden", "");
                    msjSP_Error.Text = ex.Message;
                }
            }
            else
            {

                divError.Visible = true;//msj error
                divError.Attributes["class"] = divError.Attributes["class"].Replace("hidden", "");
                msjSP_Error.Text = "No has agregado ningun producto";
            }
    
        }


        protected void btnRealizarPedido_Click(object sender, EventArgs e)//este evento crea el pedido con todos los suplementos guardados en la lista que tiene la sesion [listaPedido]
        {
            divFormato.Visible = false;//msj exito
            divError.Visible = false;//msj error

            List<SuplementoPedido> listaPedido = (List<SuplementoPedido>)Session["ListaPedido"];//
            if (listaPedido.Count != 0) //se comprueba que la lista donde se añaden los productos no este vacia
            {
                try
                {
                    int idUsuario = int.Parse(Session["id"].ToString());//esta sesion contiene el id del usuario, se manda desde la pagina que redirige a esta
                    string msj = string.Empty;

                    BD_ProyectoDataContext dataContext = new BD_ProyectoDataContext();


                    dataContext.SP_CREAR_PEDIDO(idUsuario, ref msj);
                    Session["idPedido"] = msj;//va guardar el id del pedido generado


                    int idPedido = int.Parse(msj);


                    foreach (SuplementoPedido suplementoPedido in listaPedido)//se guardan los registros de la lista
                    {
                        dataContext.SP_AGREGAR_PRODUCTO_PEDIDO(idPedido, suplementoPedido.Suplemento, suplementoPedido.Cantidad, ref msj);
                    }
                    // Guardar los cambios en la base de datos
                    dataContext.SubmitChanges();
                    MostrarDatosVistaSuplementosCategorias();

                    // Limpiar la lista para el próximo pedido
                    listaPedido.Clear();

                    divFormato.Visible = true;//msj exito
  
                    divFormato.Attributes["class"] = divFormato.Attributes["class"].Replace("hidden", "");
                    msjSP.Text = msj;

                    Session["ListaPedido"] = null;//se limpia la lista de la sesion

                    // Limpiar los campos
                    txtSuplemento.Text = string.Empty;
                    txtCantidad.Text = string.Empty;
                    txtPrecio.Text = string.Empty;

                }

                catch (Exception ex)
                {

                    divError.Visible = true;//msj error
                    divError.Attributes["class"] = divError.Attributes["class"].Replace("hidden", "");
                    msjSP_Error.Text = ex.Message;
                }
            }
            else
            {

                divError.Visible = true;//msj error
                divError.Attributes["class"] = divError.Attributes["class"].Replace("hidden", "");
                msjSP_Error.Text = "No has agregado ningun producto";
            }
           
        }

        //restablecer la lista de productos añadidos
        protected void btnReset_Click(object sender, EventArgs e)
        {
            divFormato.Visible = true;//msj exito
            divError.Visible = false;//msj error
            divFormato.Attributes["class"] = divFormato.Attributes["class"].Replace("hidden", "");
            msjSP.Text = "Productos Eliminados de la lista";
            List<SuplementoPedido> listaPedido = new List<SuplementoPedido>();
            Session["ListaPedido"] = listaPedido;

        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            //restablecer la lista de productos añadidos
            List<SuplementoPedido> listaPedido = new List<SuplementoPedido>();
            Session["ListaPedido"] = listaPedido;
            Response.Redirect("Pedidos.aspx");

         
        }

        private void pasarDatos()
        {
            try
            {
                BD_ProyectoDataContext dataContext = new BD_ProyectoDataContext();

                int idDetallePedido = int.Parse(Session["idDetallePedidos"].ToString());//sesion que contiene el id a editar

                var consulta = (from dp in dataContext.DETALLE_PEDIDOS
                                join s in dataContext.SUPLEMENTOS on dp.ID_SUPLEMENTO equals s.ID_SUPLEMENTO
                                where dp.ID_DETALLE_PEDIDO == idDetallePedido
                                select new
                                {
                                    nombreSuplemento = s.NOMBRE,
                                    precio = s.PRECIO * dp.CANTIDAD_SOLICITADA,
                                    cantidad = dp.CANTIDAD_SOLICITADA
                                }).FirstOrDefault();

                txtSuplemento.Text = consulta.nombreSuplemento;
                txtCantidad.Text = consulta.cantidad.ToString();
                txtPrecio.Text = consulta.precio.ToString("0.00");//formato para que se muestren solo dos decimales
            }
            catch (Exception ex)  
            {

                divError.Visible = true;//msj error
                divError.Attributes["class"] = divError.Attributes["class"].Replace("hidden", "");
                msjSP_Error.Text = ex.Message;
            }

        }

        protected void btnEditar_Click(object sender, EventArgs e)//este evento es para editar un pedido existente
        {
            divFormato.Visible = false;//msj exito
            divError.Visible = false;//msj error
            string msj = string.Empty;
            try
            {
                if (txtCantidad.Text != string.Empty)//si la cantidad no esta vacia
                {
                    
                    BD_ProyectoDataContext dataContext = new BD_ProyectoDataContext();

                    int idSuplemento = int.Parse(Session["IdGlobal"].ToString());
                    int idDetallePedido = int.Parse(Session["idDetallePedidos"].ToString());//sesion que contiene el id a editar
                    int cantidad = int.Parse(txtCantidad.Text);
;                   string retorno=string.Empty;

                    //consulta guarda la cantidad solicitada del pedido antes de actualizar
                    int consultaCantidad = (from c in dataContext.DETALLE_PEDIDOS
                                            where c.ID_DETALLE_PEDIDO == idDetallePedido
                                            select c.CANTIDAD_SOLICITADA).FirstOrDefault();

                    //consulta guarda el id del suplemento solicitado del pedido antes de actualizar
                    int consultaSuplemento = (from c in dataContext.DETALLE_PEDIDOS
                                            where c.ID_DETALLE_PEDIDO == idDetallePedido
                                            select c.ID_SUPLEMENTO).FirstOrDefault();

                    if (consultaCantidad != cantidad || consultaSuplemento!=idSuplemento)//se ejecuta solo si alguno recibio alguna modificacion
                    {

                        dataContext.SP_EDITAR_PEDIDO(idDetallePedido, idSuplemento, cantidad, ref msj, ref retorno);


                        if (int.Parse(retorno) == 1)//TODO SALIO BIEN
                        {
                            // Guardar los cambios en la base de datos
                            dataContext.SubmitChanges();

                            divFormato.Visible = true;//msj exito
                            divFormato.Attributes["class"] = divFormato.Attributes["class"].Replace("hidden", "");//mostrar div de exito
                            msjSP.Text = msj;
                            MostrarDatosVistaSuplementosCategorias();

                        }
                        else
                        {

                            divError.Visible = true;//msj error

                            divError.Attributes["class"] = divError.Attributes["class"].Replace("hidden", ""); //mostrar div de error
                            msjSP_Error.Text = msj;
                        }


                        // Limpiar los campos
                        txtSuplemento.Text = string.Empty;
                        txtCantidad.Text = string.Empty;
                        txtPrecio.Text = string.Empty;
                    }

                }
            }
            catch (Exception ex)
            {
         
                divError.Visible = true;//msj error
                divError.Attributes["class"] = divError.Attributes["class"].Replace("hidden", "");
                msjSP_Error.Text = ex.Message;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)//metodo para buscar por nombre un suplemento
        {
            divFormato.Visible = false;//msj exito
            divError.Visible = false;//msj error


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
                                   NombreCategoria = c.NOMBRE
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
                        NombreCategoria = c.NOMBRE
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

                divError.Visible = true;//msj error
                divError.Attributes["class"] = divError.Attributes["class"].Replace("hidden", "");
                msjSP_Error.Text = ex.Message;
            }
        }

        protected void btnMostrarTodos_Click(object sender, EventArgs e)//metodo para mostrar todos los suplementos
        {
            divFormato.Visible = false;//msj exito
            divError.Visible = false;//msj error
            grdSuplementos.Visible = true;
            MostrarDatosVistaSuplementosCategorias();
        }
    }
}

