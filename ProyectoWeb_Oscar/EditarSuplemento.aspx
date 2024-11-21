<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="EditarSuplemento.aspx.cs" Inherits="ProyectoWeb_Oscar.EditarSuplemento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" style="margin-bottom: 15rem; margin-top: 2rem;">
        <h1 class="text-center">Edición de Suplementos</h1>
        <div class="row" style="margin-top: 2rem;">
            <div class="col-md-6">
                <!-- Formulario para agregar/editar suplementos -->
                <h2>Editar Suplemento</h2>

                <div class="form-group">
                    <label for="nombre">Nombre:</label>
                    <asp:TextBox ID="txtNombre" runat="server" class="form-control" placeholder="Nombre del suplemento"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtNombre" CssClass="alert-danger" ForeColor="Red" ValidationGroup="validar" Display="Dynamic">Campo nombre no puede estra vacio</asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label for="descripcion">Descripción:</label>
                    <textarea class="form-control" id="txtDescripcion" name="txtDescripcion" runat="server" placeholder="Descripción del suplemento"></textarea>
                </div>
                <div class="form-group">
                    <label for="precio">Precio:</label>
                    <asp:TextBox ID="txtPrecio" class="form-control" runat="server" placeholder="Precio del suplemento" onkeydown="return validarEntrada(event)" TextMode="Number"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtPrecio" CssClass="alert-danger" ForeColor="Red" ValidationGroup="validar" Display="Dynamic">Campo Precio no puede estar vacio</asp:RequiredFieldValidator>

                </div>
                <div class="form-group">
                    <label for="cantidad">Cantidad:</label>
                    <asp:TextBox ID="txtCantidad" class="form-control" runat="server" placeholder="Cantidad del suplemento" onkeydown="return validarEntrada(event)" TextMode="Number"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtCantidad" CssClass="alert-danger" ForeColor="Red" ValidationGroup="validar" Display="Dynamic">Campo cantidad no puede estra vacio</asp:RequiredFieldValidator>
                </div>

                <div class="form-group">
                    <label for="fileImagen">Imagen:</label>
                    <asp:FileUpload ID="fileImagen" runat="server" CssClass="form-control" />
                </div>

                <asp:Button class="btn btn-dark" Style="margin-top: 1rem; margin-bottom: 0.5rem;" ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" ValidationGroup="validar" />

                <div id="divFormato" class="alert alert-success hidden" runat="server">
                    <asp:Label ID="msjSP" runat="server" Text=""></asp:Label>
                </div>
                <div id="divError" class="alert error-box hidden" runat="server">
                    <asp:Label ID="msjSP_Error" runat="server" Text=""></asp:Label>
                </div>

            </div>
            <div class="col-md-6">
                <!-- GridView para mostrar los suplementos existentes -->
                <h2>Suplementos Existentes</h2>
                <asp:TextBox ID="txtBuscar" class="form-control" runat="server" placeholder="Buscar Suplemento" AutoPostBack="True"></asp:TextBox>
                <asp:Button class="btn btn-dark" ID="btnBuscar" runat="server" Style="margin-top: 0.5rem; margin-bottom: 0.5rem;" Text="Buscar" OnClick="btnBuscar_Click" />
                <asp:Button class="btn btn-dark" ID="btnMostrarTodos" runat="server" Style="margin-top: 0.5rem; margin-bottom: 0.5rem;" Text="Mostrar Todos" OnClick="btnMostrarTodos_Click" />



                <asp:GridView ID="grdSuplementos" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="grdSuplementos_PageIndexChanging" PageSize="6">
                    <Columns>
                        <asp:BoundField HeaderText="Nombre" DataField="NOMBRE" />
                        <asp:BoundField HeaderText="Descripción" DataField="DESCRIPCION" />
                        <asp:BoundField HeaderText="Precio" DataField="PRECIO" DataFormatString="{0:N2}" />
                        <asp:BoundField HeaderText="Cantidad" DataField="CANTIDAD" />
                        <asp:TemplateField HeaderText="Imagen">

                            <ItemTemplate>
                                <asp:Label ID="lblImagen" runat="server" Text='<%# Eval("RUTA_IMAGEN").ToString() != "" ? "Tiene imagen" : "No tiene imagen" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnPasarDatos" class="btn btn-dark" runat="server" CommandArgument='<%# Eval("ID_SUPLEMENTO").ToString() %>' CommandName="PasarDatos" OnCommand="btnPasarDatos_Command" Text="Editar" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div>
                    <asp:Label class="text-center" ID="lblTexto" runat="server" Text="No existen suplementos" Visible="False"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
