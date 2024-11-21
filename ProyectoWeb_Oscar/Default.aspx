<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProyectoWeb_Oscar.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="contenedor text-center">

        <%-- primera fila --%>
        <div class="columna campo1">
            <div class="relleno">
                <p class="container_p1">SUPLEMENTOS DEPORTIVOS</p>
                <p class="container_p2">Si desea ver los suplementos disponibles ingrese acá:</p>
                <asp:Button ID="btnVerSuplementos" runat="server" Text="Ver" class="btn btn-outline-dark" OnClick="btnVerSuplementos_Click"/>

            </div>

        </div>
        <div class="columna campo2">
            <div id="carouselExampleSlidesOnly" class="carousel slide" data-bs-ride="carousel">
                <div class="carousel-inner">
                    <div class="carousel-item active">
                        <img src="imagenes/img1.jpg" class="d-block w-100 imagenesSize" />
                    </div>
                    <div class="carousel-item">
                        <img src="imagenes/img2.jpg" class="d-block w-100 imagenesSize" />
                    </div>
                    <div class="carousel-item">
                        <img src="imagenes/img3.jpeg" class="d-block w-100 imagenesSize" />
                    </div>
                </div>
            </div>
        </div>

        <%-- segunda fila --%>
        <div class="columna campo3" style="margin-top: 5rem;">
            <div class="relleno">
                <p class="container_p1" style="padding-top: 2rem;">PEDIDOS</p>
                <p class="container_p2">Puedes realizar pedidos de suplementos de forma fácil:</p>
                <asp:Button ID="btnPedidos" class="btn btn-outline-dark" runat="server" Text="Realizar Pedido" OnClick="btnPedidos_Click" />
            
            </div>
        </div>

        <div class="columna campo4" style="margin-top: 5rem;">
            <img src="imagenes/img-pedido.jpg" class="img-fluid imagen-redonda" />
        </div>

        <%-- Tercera fila --%>
        <div class="columna campo5 fila3" style="margin-top: 5rem; grid-row: span 2;">
            <div class="relleno-auto">
                <p class="container_p1" style="padding-top: 2rem;">USUARIOS</p>
                <p class="container_p2">Crear un usuario (esto es necesario para realizar pedidos)</p>
                <div>
                    <img src="imagenes/usuarioadd.png" class="img-fluid" style="height: 15rem;" />
                </div>
                <asp:Button ID="Button1" class="btn btn-outline-dark" style="margin-top:2rem;" runat="server" Text="Crear Usuario" OnClick="Button1_Click" />
   

            </div>
        </div>




    </div>







</asp:Content>
