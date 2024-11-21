<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="MostrarSuplementos.aspx.cs" Inherits="ProyectoWeb_Oscar.MostrarSuplementos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container text-center">
        <h1 class="mt-4" style="margin-top: 1rem; width: 90%;">Suplementos</h1>
        <hr />
        <div class="row" runat="server" id="suplementosContainer">
            <!-- Los suplementos se agregarán dinámicamente aquí -->
        </div>
    </div>
    <div id="divError" class="alert error-box hidden" runat="server">
        <asp:Label ID="msjSP_Error" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
