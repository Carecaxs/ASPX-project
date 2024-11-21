<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="ProyectoWeb_Oscar.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>





<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5" style="margin-bottom: 28rem;">
        <div class="row">
            <div class="col-md-6 offset-md-3">
                <h1 class="text-center">Iniciar Sesión</h1>

                <div class="mb-3">
                    <label for="username" class="form-label">Correo</label>
                    <div class="d-flex align-items-center">
                        <asp:TextBox ID="txtUsuario" CssClass="form-control" class="form-control" runat="server" placeholder="Ingrese su correo electronico"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Campo no puede estra vacio" ValidationGroup="validar" Text="*" ControlToValidate="txtUsuario" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="mb-3">
                    <label for="password" class="form-label">Contraseña</label>
                    <div class="d-flex align-items-center">
                        <asp:TextBox ID="txtContraseña" CssClass="form-control" class="form-control" runat="server" placeholder="Ingrese su contraseña" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Campo no puede estar vacio" ControlToValidate="txtContraseña" ValidationGroup="validar" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <asp:Button ID="btnIngreasr" class="btn btn-dark" runat="server" Text="Iniciar Sesión" ValidationGroup="validar" OnClick="btnIngreasr_Click" />


                <hr>
                <p class="text-center">¿No tienes una cuenta? <a href="#"><asp:Button ID="btnCrearUsuario" runat="server" Text="Crear nueva cuenta" CssClass="btn-invisible" OnClick="btnCrearUsuario_Click"/></a></p>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="validar" ForeColor="Red" />
            </div>
            <div id="msjError" class="error-box hidden" runat="server">
                <asp:Label class="text-center" ID="lblMensaje" runat="server" Text="Correo o Contraseña incorrecta, vuelva a intentar"></asp:Label>
            </div>
        </div>
    </div>











</asp:Content>
