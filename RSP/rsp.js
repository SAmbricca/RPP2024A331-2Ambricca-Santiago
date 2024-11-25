class Persona {
    id;
    nombre;
    apellido;
    fechaNacimiento;
    constructor(id, nombre, apellido, fechaNacimiento) {
        this.id = id;
        this.nombre = nombre;
        this.apellido = apellido;
        this.fechaNacimiento = fechaNacimiento;
    }

    toString() {
        return `Id: ${this.id}. Nombre: ${this.nombre}. apellido: ${this.apellido}. fechaNacimiento: ${this.fechaNacimiento}`;
    }

    toArray() {
        return [this.id, this.nombre, this.apellido, this.fechaNacimiento];
    }
}

class Ciudadano extends Persona{
    dni;

    constructor(id, nombre, apellido, fechaNacimiento, dni) {
        super(id, nombre, apellido, fechaNacimiento);
        this.dni = dni;
    }

    toString() {
        return `${super.toString()}. dni: ${this.dni}.`;
    }

    toArray() {
        return super.toArray().concat([this.dni]);
    }
}

class Extranjero extends Persona {
    paisOrigen;
    constructor(id, nombre, apellido, fechaNacimiento, paisOrigen) {
        super(id, nombre, apellido, fechaNacimiento);
        this.paisOrigen = paisOrigen;
    }

    toString() {
        return `${super.toString()}. paisOrigen: ${this.paisOrigen}.`;
    }

    toArray() {
        return super.toArray().concat([this.paisOrigen]);
    }
}

window.onload = function(){
    obtenerDatos();
    document.getElementById("slTipo").addEventListener("change", habilitarCampos);
    document.getElementById('formularioABM').style.display = 'none';
    document.getElementById("botonAceptarFormularioABM").addEventListener("click", () => {
        if (operacionActual === "agregar") {
            altaPersona(); 
        }
    });
}

let listaPersonas = [];
function obtenerDatos(){
    mostrarSpinner();
    var xhttp = new XMLHttpRequest();
    let url = 'https://examenesutn.vercel.app/api/PersonaCiudadanoExtranjero';
    xhttp.open("GET", url);
    xhttp.send();
    xhttp.onreadystatechange = function(){
        if(xhttp.readyState === 4){    
            ocultarSpinner();            
            if(xhttp.status === 200){
                let jsonRespuesta = JSON.parse(xhttp.responseText);
                if(listaPersonas.length === 0){
                    listaPersonas = jsonRespuesta.map(persona =>{
                        if(persona.dni !== undefined){
                            return new Ciudadano(persona.id, persona.nombre, persona.apellido, persona.fechaNacimiento, persona.dni);
                        } else if (persona.paisOrigen !== undefined){
                            return new Extranjero(persona.id, persona.nombre, persona.apellido, persona.fechaNacimiento, persona.paisOrigen);
                        }
                    });
                }
                mostrarListaPersonas();
            } else {
                alert ("No se pudo paisOrigenr: " + url + "\nError: " +xhttp.status);
            }
        }
    }
}

let operacionActual = "agregar";

function mostrarListaPersonas(){
    let tablaLista = document.getElementById("datosTabla");
    tablaLista.innerHTML = "";

    listaPersonas.forEach(persona =>{
        let fila = tablaLista.insertRow();

        fila.insertCell().innerHTML = persona.id;
        fila.insertCell().innerHTML = persona.nombre;
        fila.insertCell().innerHTML = persona.apellido;
        fila.insertCell().innerHTML = persona.fechaNacimiento;
        fila.insertCell().innerHTML = persona instanceof Ciudadano ? persona.dni : "--";
        fila.insertCell().innerHTML = persona instanceof Extranjero ? persona.paisOrigen : "--";            
        fila.insertCell().innerHTML = `<button class="botonLista" onclick="mostrarPersona(${persona.id}, 'modificar')">Modificar</button>`;            
        fila.insertCell().innerHTML = `<button class="botonLista" onclick="mostrarPersona(${persona.id}, 'eliminar')">Eliminar</button>`;
    })
}

function cambiarFormulario(operacion = "agregar"){
    mostrarSpinner();
    habilitarCampos();
    operacionActual = operacion;

    let formularioLista = document.getElementById("formularioLista");
    let formularioAbm = document.getElementById("formularioABM");

    setTimeout(()=>{
        if(formularioLista.style.display === "none"){
            formularioLista.style.display = "block";
            formularioAbm.style.display = "none";
            limpiarCampos();
        } else {
            formularioLista.style.display = "none";
            formularioAbm.style.display = "block";
            if(operacion === "modificar"){
                document.getElementById("labelTituloAbm").textContent = "Modificar persona";
            } else if (operacion === "eliminar"){
                document.getElementById("labelTituloAbm").textContent = "Eliminar persona";
            } else {
                document.getElementById("labelTituloAbm").textContent = "Agregar persona";
            }
        }

        if(operacion === "cancelar"){
            limpiarCampos();
        }

        ocultarSpinner();
    }, 500);        
}

async function altaPersona(){
    mostrarSpinner();
    let nombre = document.getElementById("txtNombre").value;
    let apellido = document.getElementById("txtApellido").value;
    let fechaNacimiento = document.getElementById("txtFechaNacimiento").value;
    let dni;
    let paisOrigen;

    if(!validarPersona(nombre, apellido, fechaNacimiento)){
        ocultarSpinner();
        return;
    }        

    let persona;

    if(document.getElementById("slTipo").value === "Ciudadano"){
        dni = document.getElementById("txtDni").value;
        
        if(!validarCiudadano(dni)){
            ocultarSpinner();
            return;
        }

        persona = {nombre, apellido, fechaNacimiento, dni};
        
    } else if(document.getElementById("slTipo").value === "Extranjero"){
        paisOrigen = document.getElementById("txtPaisOrigen").value;
        
        if(!validarExtranjero(paisOrigen)){
            ocultarSpinner();
            return;
        }

        persona = {nombre, apellido, fechaNacimiento, paisOrigen};
    }     
    let respuesta = await enviarSolicitudPost(persona);

    if(respuesta){
        persona.id = respuesta.id;

        if(document.getElementById("slTipo").value === "Ciudadano"){
            let ciudadano = new Ciudadano(persona.id, nombre, apellido, fechaNacimiento, dni);
            listaPersonas.push(ciudadano);
        } else {
            let extranjero = new Extranjero(persona.id, nombre, apellido, fechaNacimiento, paisOrigen);
            listaPersonas.push(extranjero);
        }

        cambiarFormulario();
        mostrarListaPersonas();
    } else {
        ocultarSpinner();
        cambiarFormulario();
        alert ("No se pudo dar de alta");
    }
}

function modificarPersona(persona) {
    mostrarSpinner();

    enviarSolicitudPut(persona)
    .then(respuesta => {
        if (!respuesta) {
            mostrarListaPersonas();
            bloquearBotonAceptar();
            return;
        }

        let nombre = document.getElementById("txtNombre").value;
        let apellido = document.getElementById("txtApellido").value;
        let fechaNacimiento = document.getElementById("txtFechaNacimiento").value;

        if (!validarPersona(nombre, apellido, fechaNacimiento)) {
            ocultarSpinner();
            return;
        }

        let tipo = document.getElementById("slTipo").value;
        let nuevaPersona;

        if (tipo === "Ciudadano") {
            let dni = document.getElementById("txtDni").value;

            if (!validarCiudadano(dni)) {
                ocultarSpinner();
                return;
            }

            nuevaPersona = new Ciudadano(persona.id, nombre, apellido, fechaNacimiento, dni);
        } else if (tipo === "Extranjero") {
            let paisOrigen = document.getElementById("txtPaisOrigen").value;

            if (!validarExtranjero(paisOrigen)) {
                ocultarSpinner();
                return;
            }

            nuevaPersona = new Extranjero(persona.id, nombre, apellido, fechaNacimiento, paisOrigen);
        }

        let index = listaPersonas.findIndex(v => v.id === persona.id);
        if (index !== -1) {
            listaPersonas[index] = nuevaPersona;
        }

        cambiarFormulario();
        mostrarListaPersonas();
        bloquearBotonAceptar();
    })
    .catch(error => {
        alert("Error: " + error.message);
        ocultarSpinner();
        cambiarFormulario();
        mostrarListaPersonas();
    });
}

function bloquearBotonAceptar() {
    document.getElementById("botonAceptarFormularioABM").onclick = null;
}


function mostrarPersona(personaId, accion) {
    let persona = listaPersonas.find(v => v.id === personaId);

    if (persona) {
        let camposComunes = [
            { id: "txtId", value: persona.id },
            { id: "txtNombre", value: persona.nombre },
            { id: "txtApellido", value: persona.apellido },
            { id: "txtFechaNacimiento", value: persona.fechaNacimiento },
        ];

        camposComunes.forEach(campo => {
            let input = document.getElementById(campo.id);
            input.value = campo.value;
            input.disabled = accion === "eliminar";
        });

        if (persona instanceof Ciudadano) {
            configurarCamposEspecificos("Ciudadano", [
                { id: "txtDni", value: persona.dni },
            ], accion);
        } else if (persona instanceof Extranjero) {
            configurarCamposEspecificos("Extranjero", [
                { id: "txtPaisOrigen", value: persona.paisOrigen },
            ], accion);
        }

        cambiarFormulario(accion);

        if (accion === "modificar") {
            habilitarCampos();
            document.getElementById("botonAceptarFormularioABM").onclick = () => modificarPersona(persona);
        } else if (accion === "eliminar") {
            bloquearTodosLosCampos();
            document.getElementById("botonAceptarFormularioABM").onclick = () => enviarSolicitudDelete(personaId);
        }
    } else {
        alert("Persona no encontrada");
    }
}

function configurarCamposEspecificos(tipo, campos, accion) {
    let selectTipo = document.getElementById("slTipo");
    selectTipo.value = tipo;
    selectTipo.disabled = accion === "eliminar";

    campos.forEach(campo => {
        let input = document.getElementById(campo.id);
        input.value = campo.value;
        input.disabled = accion === "eliminar";
    });
}

// Funciones para el envio de solicitudes
async function enviarSolicitudPost(persona){
    try{
        mostrarSpinner();
        let url = 'https://examenesutn.vercel.app/api/PersonaCiudadanoExtranjero';
        let respuesta = await fetch(url, {
            method: 'POST',
            headers: {'Content-Type' : 'application/json'},
            body: JSON.stringify(persona)
        });

        if(respuesta.status === 200){
            let contenido = await respuesta.json();
            return contenido;
        } else {
            alert ("Error Status no esperado: " +respuesta.status);
            return false;
        }
    } catch(error){
        alert("Error al conectarse con " + url);
        return false;
    }
}
async function enviarSolicitudPut(persona){
    let url = 'https://examenesutn.vercel.app/api/PersonaCiudadanoExtranjero';
    return fetch(url, {
        method: 'PUT',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(persona)
    })
    .then(respuesta =>{
        if(respuesta.status === 200){
            return true;
        } else{
            alert ("No se pudo modificar: " + url + "\nError: " +respuesta.status);
            cambiarFormulario();
            return false; 
        }
    })
    .catch(error =>{
        alert(error.message);
        cambiarFormulario();
        return false;
    })
}
async function enviarSolicitudDelete(personaId){
    try{
        mostrarSpinner();
        let url = 'https://examenesutn.vercel.app/api/PersonaCiudadanoExtranjero';
        let respuesta = await fetch (url, {
            method: 'DELETE',
            headers:{'Content-Type': 'application/json'},
            body: JSON.stringify({id: personaId})
        });
        
        if(respuesta.status === 200){
            listaPersonas = listaPersonas.filter(v => v.id !== personaId);
            cambiarFormulario();
            mostrarListaPersonas();
        } else{
            cambiarFormulario();
            mostrarListaPersonas();                
            alert("Error: status " + respuesta.status);
        }
    } catch(error){
        alert("No pudo conectarse con " + url + "\nError: "+error.message);
    }
}

// Funciones para las validaciones
function validarPersona(nombre, apellido, fechaNacimiento){
    if (!validarTextoSinNumeros(nombre)) {
        alert("Nombre invalido");
        return false;
    }

    if (!validarTextoSinNumeros(apellido)) {
        alert("Apellido invalido");
        return false;
    }

    if (!validarFechaNacimiento(fechaNacimiento)){
        alert("Fecha de nacimiento invalida");
        return false;
    }

    return true;
}
function validarTextoSinNumeros(texto) {
    let regex = /^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$/;

    if (!texto || !regex.test(texto.trim())) {
        return false;
    }
    return true;
}
function validarFechaNacimiento(fecha) {
    if (fecha.length !== 8 || isNaN(fecha)) {
        return false;
    }

    let anio = parseInt(fecha.slice(0, 4), 10);
    let mes = parseInt(fecha.slice(4, 6), 10) - 1;
    let dia = parseInt(fecha.slice(6, 8), 10);

    let fechaValida = new Date(anio, mes, dia);

    return (
        fechaValida.getFullYear() === anio &&
        fechaValida.getMonth() === mes &&
        fechaValida.getDate() === dia
    );
}

function validarCiudadano(dni){
    if(dni <= 0 || isNaN(dni)){
        alert("El dni debe ser mayor a 0");
        return false;
    }
    return true;
}

function validarExtranjero(paisOrigen){
    if (!validarTextoSinNumeros(paisOrigen)) {
        alert("Pais de origen invalido");
        return false;
    }
    return true;
}

// Funciones para los cambios en la interfaz
function bloquearTodosLosCampos(){
    document.getElementById("txtNombre").disabled = true;
    document.getElementById("txtApellido").disabled = true;
    document.getElementById("txtFechaNacimiento").disabled = true;
    document.getElementById("txtDni").disabled = true;
    document.getElementById("txtPaisOrigen").disabled = true;
    document.getElementById("slTipo").disabled = true;
}


function habilitarCampos(){
    let tipoSeleccionado = document.getElementById("slTipo").value;
    document.getElementById("txtNombre").disabled = false;
    document.getElementById("txtApellido").disabled = false;
    document.getElementById("txtFechaNacimiento").disabled = false;
    document.getElementById("slTipo").disabled = false;

    if(tipoSeleccionado === "Ciudadano"){
        document.getElementById("txtDni").disabled = false;
        document.getElementById("txtDni").value = "";
        document.getElementById("txtPaisOrigen").disabled = true;
    } else if (tipoSeleccionado === "Extranjero"){
        document.getElementById("txtDni").disabled = true;
        document.getElementById("txtPaisOrigen").disabled = false;
        document.getElementById("txtDni").value = "";
    }
}

function limpiarCampos(){
    document.getElementById("txtId").value = "";
    document.getElementById("txtNombre").value = "";
    document.getElementById("txtApellido").value = "";
    document.getElementById("txtFechaNacimiento").value = "";
    document.getElementById("txtDni").value = "";
    document.getElementById("txtPaisOrigen").value = "";
}

function mostrarSpinner(){
    document.getElementById("spinner").style.display = "flex";
}

function ocultarSpinner(){
    document.getElementById("spinner").style.display = "none";
}
