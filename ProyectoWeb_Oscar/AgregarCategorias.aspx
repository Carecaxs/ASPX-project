<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="AgregarCategorias.aspx.cs" Inherits="ProyectoWeb_Oscar.AgregarCategorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" style="margin-bottom: 28rem; margin-top: 2rem;">
        <h1>Agregar Categorías</h1>
        <div class="row">
            <div class="col-md-6">

                <div class="form-group">

                    <label for="nombre">Nombre:</label>
                    <asp:TextBox ID="txtNombre" runat="server" class="form-control" placeholder="Nombre de categoría"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtNombre" CssClass="alert-danger" ForeColor="Red" ValidationGroup="validar" Display="Dynamic">Campo categoría no puede estra vacío</asp:RequiredFieldValidator>
                </div>
                <asp:Button Style="margin-top: 1rem; margin-bottom: 2rem;" class="btn btn-dark" ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" ValidationGroup="validar" />
                <%-- este div se va usar para mostrar un msj de error --%>
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
                <h3>Categorías existentes</h3>
                <asp:GridView ID="grdCategorias" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField HeaderText="ID" DataField="ID" />
                        <asp:BoundField HeaderText="Nombre" DataField="NOMBRE" />
                    </Columns>
                </asp:GridView>
                <asp:Label class="text-center" ID="lblTexto" runat="server" Text="No existen Categorías" Visible="False"></asp:Label>

            </div>
        </div>
    </div>


</asp:Content>
