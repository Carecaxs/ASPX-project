  /* <% --mediante js valido si la tecla tocada es diferente a un numero, si es asi retorna falso y no se ingresa-- %>*/
function validarEntrada(event) {
    var key = event.key;
    var keyCode = event.keyCode;

    // Verificar si la tecla presionada es Tab, Flechas o teclas de Borrar
    if (key === '.' || key === ',' || isNaN(parseInt(key)) &&
        keyCode !== 9 &&                      // Tab
        keyCode !== 37 && keyCode !== 39 &&   // Flecha Izquierda, Flecha Derecha
        keyCode !== 38 && keyCode !== 40 &&   // Flecha Arriba, Flecha Abajo
        keyCode !== 8 && keyCode !== 46) {    // Borrar, Suprimir
        event.preventDefault();
        return false;
    }
}


/*esta funcion me lleva a la parte de la pagina donde se muestra algun mensaje como por ejempo "registro agregado exitosamente"*/
function redireccionarAPosicion() {
    // Obtener la posición de la nueva sección
    var nuevaPosicion = document.getElementById("btnAgregar").offsetTop;

    // Desplazarse a la nueva posición
    window.scrollTo(0, nuevaPosicion);
}


// Maneja el evento keyup en el TextBox
function handleKeyUp(event) {
    if (event.keyCode === 13) { // Si se presiona la tecla Enter
        event.preventDefault(); // Evita el comportamiento predeterminado del Enter
        __doPostBack('<%= txtCantidad.UniqueID %>', ''); // Realiza un postback en el TextBox
    }
}


function mostrarAlerta(mensaje) {
    alert(mensaje);
}

