<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="AgregarSuplemento.aspx.cs" Inherits="ProyectoWeb_Oscar.AgregarSuplemento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .form-group label {
            margin-top: 1rem; /* Ajusta el valor del margen inferior  */
        }
    </style>


    <div class="container" style="margin-bottom: 15rem; margin-top: 2rem;">
        <h1>Agregar Suplementos</h1>
        <div class="row">

            <div class="col-md-6">
                <div class="form-group">
                    <label for="ddlCategoria">Categoría:</label>
                    <asp:DropDownList ID="ddlCategorias" runat="server" CssClass="form-control">
                    </asp:DropDownList>

                </div>
                <div class="form-group">
                    <label for="nombre">Nombre:</label>
                    <asp:TextBox ID="txtNombre" runat="server" class="form-control" placeholder="Nombre del suplemento"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtNombre" CssClass="alert-danger" ForeColor="Red" ValidationGroup="validar" Display="Dynamic">Campo nombre no puede estra vacio</asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label for="txtDescripcion">Descripción:</label>
                    <textarea class="form-control" id="txtDescripcion" name="txtDescripcion"></textarea>
                </div>
                <div class="form-group">
                    <label for="precio">Precio:</label>
                    <asp:TextBox ID="txtPrecio" class="form-control" runat="server" placeholder="Precio del suplemento" onkeydown="return validarEntrada(event)" TextMode="Number"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtPrecio" CssClass="alert-danger" ForeColor="Red" ValidationGroup="validar" Display="Dynamic">Campo Precio no puede estar vacio</asp:RequiredFieldValidator>


                </div>
                <div class="form-group">
                    <label for="cantidad">Cantidad en inventario:</label>
                    <asp:TextBox ID="txtCantidad" class="form-control" runat="server" placeholder="Cantidad del suplemento" onkeydown="return validarEntrada(event)" TextMode="Number"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtCantidad" CssClass="alert-danger" ForeColor="Red" ValidationGroup="validar" Display="Dynamic">Campo cantidad no puede estra vacio</asp:RequiredFieldValidator>
                </div>

                <div class="form-group">
                    <label for="fileImagen">Imagen:</label>
                    <asp:FileUpload ID="fileImagen" runat="server" CssClass="form-control" />
                </div>

                <asp:Button Style="margin-top: 1rem; margin-bottom: 2rem;" class="btn btn-dark" ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" ValidationGroup="validar" />
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
                <h3>Suplementos existentes</h3>
                <asp:GridView ID="grdSuplementos" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="grdSuplementos_PageIndexChanging" PageSize="20">
                    <Columns>
                        <asp:BoundField HeaderText="Nombre" DataField="NOMBRE" />
                        <asp:BoundField HeaderText="Descripción" DataField="DESCRIPCION" />
                        <asp:BoundField HeaderText="Precio" DataField="PRECIO" DataFormatString="{0:N2}" />
                        <asp:BoundField HeaderText="Cantidad" DataField="CANTIDAD" />
                        <asp:BoundField HeaderText="Categoría" DataField="NombreCategoria" />
                    </Columns>
                </asp:GridView>
                <asp:Label class="text-center" ID="lblTexto" runat="server" Text="No existen suplementos" Visible="False"></asp:Label>
            </div>
        </div>
    </div>




</asp:Content>
