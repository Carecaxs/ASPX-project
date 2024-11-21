<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="Pedidos.aspx.cs" Inherits="ProyectoWeb_Oscar.Pedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">




    <div class="container" style="margin-top: 2rem; margin-bottom:10rem">
        <h1 class="text-center">Menú de Pedidos</h1>

        <div class="row" style="margin-top: 3rem;">
            <div class="text-center col-md-12">
                <a href="CrearPedido.aspx" class="btn btn-primary btn-lg">Crear Pedido</a>

            </div>

        </div>

        <div class="row" style="margin-top: 3rem;">
            <div class="col-md-12">
                <h2 class="text-center" runat="server" id="usuario">Pedidos</h2>

                <asp:GridView ID="GridViewPedidos" runat="server" CssClass="table table-striped" Width="100%" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="FechaPedido" HeaderText="Fecha de Pedido" />
                        <asp:BoundField DataField="NombreSuplemento" HeaderText="Suplemento" />
                        <asp:BoundField DataField="CantidadSolicitada" HeaderText="Cantidad Solicitada" />
                        <asp:BoundField DataField="Precio" HeaderText="Precio" />
                        <asp:BoundField DataField="EstadoPedido" HeaderText="Estado" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button class="btn btn-outline-dark" ID="btnEditar" runat="server" CommandArgument='<%# Eval("id").ToString() %>' CommandName="Editar" OnCommand="btnEditar_Command" Text="Modificar Pedido" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
         
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="text-center" style="margin-bottom: 25rem;">
        <asp:Label class="text-center" ID="lblTexto" runat="server" Text="No existen suplementos" Visible="False"></asp:Label>
    </div>




</asp:Content>
