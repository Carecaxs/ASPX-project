<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="PerfilUsuario.aspx.cs" Inherits="ProyectoWeb_Oscar.PerfilUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5" style="margin-bottom: 40rem;">
        <div class="row">
            <div class="col-md-6 offset-md-3">
                <h1 class="text-center" runat="server" id="usuario">Bienvenido, Usuario</h1>
                <hr>

                <h2 class="text-center mt-4">Historial de Pedidos</h2>

            </div>
            <asp:GridView ID="grdDatos" runat="server" AutoGenerateColumns="False" EmptyDataText="No tienes pedidos" Width="100%">
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="FechaPedido" HeaderText="Fecha de Pedido" />
                    <asp:BoundField DataField="NombreSuplemento" HeaderText="Nombre de Suplemento" />
                    <asp:BoundField DataField="CantidadSolicitada" HeaderText="Cantidad Solicitada" />
                    <asp:BoundField DataField="Precio" HeaderText="Precio" />
                    <asp:BoundField DataField="EstadoPedido" HeaderText="Estado de Pedido" />
                </Columns>
            </asp:GridView>
            <asp:Label class="text-center" ID="lblTexto" runat="server" Text="No tienes Pedidos" Visible="False"></asp:Label>
        </div>

    </div>
























</asp:Content>
