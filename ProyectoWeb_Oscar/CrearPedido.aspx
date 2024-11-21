<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="CrearPedido.aspx.cs" Inherits="ProyectoWeb_Oscar.CrearPedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" style="margin-bottom: 5rem;">
        <h1 class="text-center">Crear Pedido</h1>

        <div class="row">
            <div class="col-md-6 col-md-offset-3">

                <div class="form-group">
                    <label for="suplemento">Suplemento:</label>
                    <asp:TextBox ID="txtSuplemento" class="form-control" runat="server" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>

                </div>
                <div class="form-group">
                    <label for="cantidad">Cantidad:</label>
                    <asp:TextBox ID="txtCantidad" class="form-control" runat="server" TextMode="Number" onkeydown="return validarEntrada(event)" onkeyup="handleKeyUp(event)" AutoPostBack="True" OnTextChanged="txtCantidad_TextChanged"></asp:TextBox>

                </div>
                <div class="form-group">
                    <label for="precio">Precio:</label>
                    <asp:TextBox ID="txtPrecio" class="form-control" runat="server" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>

                    <asp:Button Style="margin-top: 1rem;" class="btn btn-primary" ID="btnAgregar" runat="server" Text="Agregar al Pedido" OnClick="btnAgregar_Click" />
                    <asp:Button Style="margin-top: 1rem;" class="btn btn-success" ID="btnRealizarPedido" runat="server" Text="Finalizar Pedido" OnClick="btnRealizarPedido_Click" />
                    <asp:Button Style="margin-top: 1rem; margin-bottom: 0rem;" class="btn btn-danger" ID="btnReset" runat="server" Text="Resetear productos añadidos" OnClick="btnReset_Click" />
                    <asp:Button Style="margin-top: 1rem; margin-bottom: 2rem;" class="btn btn-success" ID="btnEditar" runat="server" Text="Actualizar" Visible="False" OnClick="btnEditar_Click" />
                    <asp:Button Style="margin-top: 1rem; margin-bottom: 2rem;" class="btn btn-dark" ID="btnRegresar" runat="server" Text="Regresar a mis pedidos" OnClick="btnRegresar_Click" />
                </div>





                <div id="divFormato" class="alert alert-success hidden" runat="server">
                    <asp:Label ID="msjSP" runat="server" Text=""></asp:Label>
                </div>
                <div id="divError" class="alert error-box hidden" runat="server">
                    <asp:Label ID="msjSP_Error" runat="server" Text=""></asp:Label>
                </div>

            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <h2 class="text-center">Suplementos</h2>
                <asp:TextBox ID="txtBuscar" class="form-control" runat="server" placeholder="Buscar Suplemento" AutoPostBack="True"></asp:TextBox>
                 <asp:Button class="btn btn-dark" ID="btnBuscar" runat="server" Style="margin-top: 0.5rem; margin-bottom: 0.5rem;" Text="Buscar" OnClick="btnBuscar_Click" />
                <asp:Button class="btn btn-dark" ID="btnMostrarTodos" runat="server" Style="margin-top: 0.5rem; margin-bottom: 0.5rem;" Text="Mostrar Todos" OnClick="btnMostrarTodos_Click" />

                <asp:GridView ID="grdSuplementos" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="grdSuplementos_PageIndexChanging" PageSize="20">
                    <Columns>
                        <asp:BoundField HeaderText="Nombre" DataField="NOMBRE" />
                        <asp:BoundField HeaderText="Descripción" DataField="DESCRIPCION" />
                        <asp:BoundField HeaderText="Precio" DataField="PRECIO" DataFormatString="{0:N2}" />
                        <asp:BoundField HeaderText="Cantidad" DataField="CANTIDAD" />
                        <asp:BoundField HeaderText="Categoría" DataField="NombreCategoria" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="Button1" class="btn btn-dark" runat="server" CommandArgument='<%# Eval("ID_SUPLEMENTO").ToString() %>' CommandName="PasarDatos" OnCommand="Button1_Command" Text="Seleccionar" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:Label class="text-center" ID="lblTexto" runat="server" Text="No existen suplementos" Visible="False"></asp:Label>
            </div>
        </div>
    </div>




</asp:Content>
