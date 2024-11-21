<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="CrearUsuario.aspx.cs" Inherits="ProyectoWeb_Oscar.CrearUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" style="margin-bottom: 27rem; margin-top: 2rem;">
        <h1>Crear nuevo usuario</h1>

        <div class="form-group">
            <label for="nombre">Nombre:</label>
            <input type="text" class="form-control" id="nombre" name="nombre" required>
        </div>
        <div class="form-group">
            <label for="correo">Correo:</label>
            <input type="email" class="form-control" id="correo" name="correo" required>
        </div>
        <div class="form-group">
            <label for="contrasena">Contraseña:</label>
            <input type="password" class="form-control" id="contrasena" name="contrasena"  required>
        </div>
        <div class="form-group">
            <label for="confirmar_contrasena">Confirmar Contraseña:</label>
            <input type="password" class="form-control" id="confirmar_contrasena" name="confirmar_contrasena"required>
        </div>
        <asp:Button ID="Button1" runat="server" Text="Crear usuario" style="margin-top: 1rem;" class="btn btn-dark" OnClick="Button1_Click" />
        <div id="msjError" class="error-box hidden" runat="server">
            <asp:Label class="text-center" ID="lblMensaje" runat="server" Text=""></asp:Label>
        </div>
    </div>

</asp:Content>
