class Persona {
    constructor(id, nombre, apellido, edad) {
        this.id = id;
        this.nombre = nombre;
        this.apellido = apellido;
        this.edad = edad;
    }

    toString() {
        return `Persona: ${this.nombre}. Id: ${this.id}. Apellido: ${this.apellido}. Edad: ${this.edad}`;
    }

    toArray() {
        return [this.id, this.nombre, this.apellido, this.edad];
    }
}

class Futbolista extends Persona {
    constructor(id, nombre, apellido, edad, equipo, posicion, cantGoles) {
        super(id, nombre, apellido, edad);
        this.equipo = equipo;
        this.posicion = posicion;
        this.cantGoles = cantGoles;
    }

    toString() {
        return `${super.toString()}. Equipo: ${this.equipo}. Posición: ${this.posicion}. Cantidad de goles: ${this.cantGoles}`;
    }

    toArray() {
        return super.toArray().concat([this.equipo, this.posicion, this.cantGoles]);
    }
}

class Profesional extends Persona {
    constructor(id, nombre, apellido, edad, titulo, facultad, anoGrad) {
        super(id, nombre, apellido, edad);
        this.titulo = titulo;
        this.facultad = facultad;
        this.anoGrad = anoGrad;
    }

    toString() {
        return `${super.toString()}. Título: ${this.titulo}. Facultad: ${this.facultad}. Año de graduacion: ${this.anoGrad}`;
    }

    toArray() {
        return super.toArray().concat([this.titulo, this.facultad, this.anoGrad]);
    }
}

//2)Dada la siguiente cadena de caracteres, generar un Array de objetos de la jerarquía del punto 1.
let cadena = `[{"id":1, "nombre":"Marcelo", "apellido":"Luque", "edad":45, "titulo":"Ingeniero", "facultad":"UTN",
"anoGrad":2002},{"id":2, "nombre":"Ramiro", "apellido":"Escobar", "edad":35, "titulo":"Medico",
"facultad":"UBA", "anoGrad":20012},{"id":3, "nombre":"Facundo", "apellido":"Cairo", "edad":30,
"titulo":"Abogado", "facultad":"UCA", "anoGrad":2017},{"id":4, "nombre":"Fernando", "apellido":"Nieto",
"edad":18, "equipo":"Independiente", "posicion":"Delantero", "cantGoles":7},{"id":5, "nombre":"Manuel",
"apellido":"Loza", "edad":20, "equipo":"Racing", "posicion":"Volante", "cantGoles":2},{"id":6, "nombre":"Nicolas",
"apellido":"Serrano", "edad":23, "equipo":"Boca", "posicion":"Arquero", "cantGoles":0}]`;



let cadenaArray = JSON.parse(cadena);

//4)Implementar Funcionalidades:
//a)Mostrar en “Form Datos” la información de los objetos generados en el punto 2.
window.onload = function() {
    actualizarTabla(cadenaArray);
    document.getElementById('formularioABM').style.display = 'none';
};

function actualizarTabla(arrayPersonas = cadenaArray) {
    let contenidoTabla = document.getElementById('datosTabla');
    contenidoTabla.innerHTML = ''; //elimino los datos de la tabla

    //creo la nueva tabla
    arrayPersonas.forEach(persona => {
        let fila = document.createElement('tr');
        fila.innerHTML = `
            <td>${persona.id}</td>
            <td>${persona.nombre}</td>
            <td>${persona.apellido}</td>
            <td>${persona.edad}</td>
            <td>${persona.equipo !== undefined ? persona.equipo : '--'}</td>
            <td>${persona.posicion !== undefined ? persona.posicion : '--'}</td>
            <td>${persona.cantGoles !== undefined ? persona.cantGoles : '--'}</td>
            <td>${persona.titulo !== undefined ? persona.titulo : '--'}</td>
            <td>${persona.facultad !== undefined ? persona.facultad : '--'}</td>
            <td>${persona.anoGrad !== undefined ? persona.anoGrad : '--'}</td>
        `;
        //uso "!== undefined ? Persona.equipo : '--'" para definir por defecto el valor '--' cuando no se proporcionen esos datos

        //d) doble click en una fila del FORM DATOS
        fila.addEventListener('dblclick', function() {
            mostrarFilaYFormularioAbm(fila, persona);
        });

        contenidoTabla.appendChild(fila);
   });
}

//b)Filtrar los datos de los objetos mostrados en “Form Datos” acorde al filtro Seleccionado cuando cambie el valor del control (Todos/Futbolistas/Profesionales).
function filtrarPersonas() {
    let filtro = document.getElementById('filtro').value;
    let listaFiltro = [];

    if (filtro == "Todos") {
        listaFiltro = cadenaArray;
    } else if (filtro == "Futbolistas") {
        listaFiltro = cadenaArray.filter(function(elementoActual) {
            return elementoActual.equipo !== undefined && elementoActual.posicion !== undefined;
        });
    } else if (filtro == "Profesionales") {
        listaFiltro = cadenaArray.filter(function(elementoActual) {
            return elementoActual.titulo !== undefined && elementoActual.facultad !== undefined;
        });
    }

    actualizarTabla(listaFiltro);
    return listaFiltro;
}

//c)Al hacer Click en el botón “Calcular”, debe mostrarse la velocidad maxima promedio de los elementos filtrados. Utilizar Map/Reduce/Filter
function calcularPromedioEdad() {
    let personas = filtrarPersonas();

    let sumaEdad = personas.reduce((acumulador, elemento) => {
        return acumulador + elemento.edad;
    }, 0);

    let promedio = 0;
    if (personas.length > 0) {
        promedio = sumaEdad / personas.length;
    }

    document.getElementById('promedio').value = promedio.toFixed(2); // toFixed(2) redondea a dos decimales
}

//d)Al hacer doble click en una fila del “Form Datos” o en el botón “Agregar” ocultar el “Form Datos” y mostrar el
//  “Formulario ABM” con los datos de la fila o vacío según corresponda (ocultar los botones que correspondan).

//la funcion doble click agregada en la funcion actualizarTabla()
function FormularioDatosAAbm() {
    document.getElementById('controlesFormularioDatos').style.display = 'none';
    document.getElementById('tituloFormulario').textContent = 'Form ABM';
    document.getElementById('tabla').style.display = 'none';
    document.getElementById('botonAgregarFormularioDatos').style.display = 'none';
    document.getElementById('formularioABM').style.display = 'block';
    document.getElementById('datosProfesionales').style.display = 'none';
}

function FormularioAbmADatos() {
    document.getElementById('controlesFormularioDatos').style.display = 'block';
    document.getElementById('tituloFormulario').textContent = 'Form Datos';
    document.getElementById('tabla').style.display = 'block';
    document.getElementById('botonAgregarFormularioDatos').style.display = 'block';
    document.getElementById('formularioABM').style.display = 'none';
}

function mostrarFilaYFormularioAbm(fila, persona) {
    FormularioDatosAAbm();
    let filas = document.querySelectorAll('#datosTabla tr');

    document.getElementById('tabla').style.display = 'block';
    filas.forEach(f => {
        if (f !== fila) {
            f.style.display = 'none';
            return;
        }
    });

    //Completo los datos en el formABM con los de la fila
    document.getElementById('id ABM').value = persona.id;
    document.getElementById('nombre ABM').value = persona.nombre;
    document.getElementById('apellido ABM').value = persona.apellido;
    document.getElementById('edad ABM').value = persona.edad;
    document.getElementById('equipo ABM').value = persona.equipo !== undefined ? persona.equipo : '';
    document.getElementById('posicion ABM').value = persona.posicion !== undefined ? persona.posicion : '';
    document.getElementById('cantGoles ABM').value = persona.cantGoles !== undefined ? persona.cantGoles : '';
    document.getElementById('titulo ABM').value = persona.titulo !== undefined ? persona.titulo : '';
    document.getElementById('facultad ABM').value = persona.facultad !== undefined ? persona.facultad : '';
    document.getElementById('anoGrad ABM').value = persona.anoGrad !== undefined ? persona.anoGrad : '';
}

//e)Al hacer click en alguno de los botones del “Formulario ABM” debe realizarse la operación que corresponda, ocultar el
//formulario y mostrar el Formulario “Form Datos” con los datos actualizados. En caso de ser un Alta, generar ID único.
//Utilizar Map/Reduce/Filter.

function filtrarPersonasAbm(){
    let tipo = document.getElementById('tipo ABM').value;

    if (tipo == "0") {
        document.getElementById('datosFutbolistas').style.display = 'block';
        document.getElementById('datosProfesionales').style.display = 'none';
    } else {
        document.getElementById('datosFutbolistas').style.display = 'none';
        document.getElementById('datosProfesionales').style.display = 'block';
    }

}

function agregarFormularioAbm() {
    let idAbm = document.getElementById('id ABM').value;
    //primero valido que no exista el Persona
    let idBuscado = parseInt(idAbm);

    let idsArray = cadenaArray.map(function(elemento) {
        return elemento.id;
    });

    let personaExistente = idsArray.includes(idBuscado);

    if (personaExistente) {
        alert("El id de la Persona ya existe");
        limpiarFormularioAbm();
        actualizarTabla(cadenaArray);
        FormularioAbmADatos();
        return cadenaArray; //devuelvo sin modificaciones
    }

    //si no existe el ingresado, le creo un nuevo ID
    let id = Math.floor(Math.random() * 1000) + 1; //https://www.w3schools.com/js/js_random.asp
    let nombre = document.getElementById('nombre ABM').value;
    let apellido = document.getElementById('apellido ABM').value;
    let edad = document.getElementById('edad ABM').value;

    //Determino el tipo de Persona
    let persona;
    let tipo = document.getElementById('tipo ABM').value;
    
    if (tipo == "0") {
        let equipo = document.getElementById('equipo ABM').value;
        let posicion = document.getElementById('posicion ABM').value;
        let cantGoles = document.getElementById('cantGoles ABM').value;
        persona = new Futbolista(id, nombre, apellido, edad, equipo, posicion, cantGoles); 
    } else {
        let titulo = document.getElementById('titulo ABM').value;
        let facultad = document.getElementById('facultad ABM').value;
        let anoGrad = document.getElementById('anoGrad ABM').value;
        persona = new Profesional(id, nombre, apellido, edad, titulo, facultad, anoGrad); 
    }

    //agrego el Persona a la cadena
    cadenaArray.push(persona.toArray());

    //limpio el formularioABM
    limpiarFormularioAbm();

    actualizarTabla(cadenaArray);

    //vuelvo atras la visualizacion
    FormularioAbmADatos()

    return cadenaArray;
}

function modificarFormularioAbm(){
    let id = document.getElementById('id ABM').value;
    let tipo = document.getElementById('tipo ABM').value;

    if (tipo == "0") {
        cadenaArray.forEach(persona => {
            if (persona.id == id) {
                persona.nombre = document.getElementById('nombre ABM').value;
                persona.apellido = document.getElementById('apellido ABM').value;
                persona.edad = document.getElementById('edad ABM').value;
                persona.equipo = document.getElementById('equipo ABM').value;
                persona.posicion = document.getElementById('posicion ABM').value;
                persona.cantGoles = document.getElementById('cantGoles ABM').value;
            }
        })
    } else {
        cadenaArray.forEach(persona => {
            if (persona.id == id) {
                persona.nombre = document.getElementById('nombre ABM').value;
                persona.apellido = document.getElementById('ano fab ABM').value;
                persona.edad = document.getElementById('edad ABM').value;
                persona.titulo = document.getElementById('titulo ABM').value;
                persona.facultad = document.getElementById('facultad ABM').value;
                persona.anoGrad = document.getElementById('anoGrad ABM').value;
            }
        })
    }
    //limpio el formularioABM
    limpiarFormularioAbm();

    actualizarTabla(cadenaArray);

    //vuelvo atras la visualizacion
    FormularioAbmADatos()
}

function eliminarFormularioAbm(){
    let id = document.getElementById('id ABM').value;

    cadenaArray.forEach(persona => {
        if (persona.id == id) {
            cadenaArray.splice(cadenaArray.indexOf(persona), 1); //el 1 indica cuantos elementos va a eliminar del array
        }
    })
    //limpio el formularioABM
    limpiarFormularioAbm();

    actualizarTabla(cadenaArray);

    //vuelvo atras la visualizacion
    FormularioAbmADatos()
}

function cancelarFormularioAbm(){
    //limpio el formularioABM
    limpiarFormularioAbm();

    actualizarTabla(cadenaArray);

    //vuelvo atras la visualizacion
    FormularioAbmADatos()
}

function limpiarFormularioAbm() {
    document.getElementById('id ABM').value = '';
    document.getElementById('nombre ABM').value = '';
    document.getElementById('apellido ABM').value = '';
    document.getElementById('edad ABM').value = '';
    document.getElementById('equipo ABM').value = '';
    document.getElementById('posicion ABM').value = '';
    document.getElementById('cantGoles ABM').value = '';
    document.getElementById('titulo ABM').value = '';
    document.getElementById('facultad ABM').value = '';
    document.getElementById('anoGrad ABM').value = '';
    document.getElementById('tipo ABM').selectedIndex = 0; //Reinicia a "Futbolistas"
}

//f)El formulario ABM debe realizar validaciones acorde al tipo de objeto y a las restricciones descritas en el diagrama del
//punto 1. El campo ID no debe ser modificable y debe mostrar el id del objeto existente o vacío en caso de un alta.

//g)Al hacer Click en alguno de los botones de los encabezados de la tabla del Formulario “Form Datos”, ordenar las filas
//de la tabla por la columna clickeada.
function ordenarTabla(opcion) {
    //Utilizo la funcion sort, facilitando el problema. https://www.w3schools.com/js/js_array_sort.asp
    let orden = [];
    switch (opcion) {
        //por numeros
        case 'id':
            orden = cadenaArray.sort((a, b) => a.id - b.id);
            break;
        case 'edad':
            orden = cadenaArray.sort((a, b) => a.edad - b.edad);
            break;
        case 'cantGoles':
            orden = ordenarPorPropiedad(cadenaArray, "cantidadGoles");
            break;
        case 'anoGrad':
            orden = ordenarPorPropiedad(cadenaArray, "anoGrad");
            break;
        //por texto
        case 'nombre':
            orden = cadenaArray.sort((a, b) => a.nombre.localeCompare(b.nombre));
            break;
        case 'apellido':
            orden = cadenaArray.sort((a, b) => a.apellido.localeCompare(b.apellido));
            break;
        case 'equipo':
            orden = ordenarPorPropiedad(cadenaArray, "equipo");
            break;
        case 'posicion':
            orden = ordenarPorPropiedad(cadenaArray, "posicion");
            break;
        case 'titulo':
            orden = ordenarPorPropiedad(cadenaArray, "titulo");
            break;
        case 'facultad':
            orden = ordenarPorPropiedad(cadenaArray, "facultad");
            break;
    }

    actualizarTabla(orden);
}

function ordenarPorPropiedad(array, propiedad) {
    array.sort((a, b) => {
        // si son nulos, es decir '--', se ordenan al final
        if (a[propiedad] == null && b[propiedad] == null) return 0;
        if (a[propiedad] == null) return 1;
        if (b[propiedad] == null) return -1;

        if (typeof propiedad == 'number') {
            return a[propiedad] - b[propiedad];
        }

        return a[propiedad].localeCompare(b[propiedad]);
    });
}

//h)El formulario “Form Datos” debe mostrar/ocultar las columnas de la tabla según esté chequeado el checkbox
//  correspondiente a esa columna (chequeado mostrar, no chequeado ocultar).

function checkItemTabla() {
    //obtengo todos los checkboxes
    let checkboxes = document.querySelectorAll('#controlesFormularioDatos input[type="checkbox"]');
    checkboxes.forEach(checkbox => {
        let columnaClase = checkbox.getAttribute('data-columna');
        //seleccino todas las celdas y oculto el display
        let celdas = document.querySelectorAll('.' + columnaClase);
        celdas.forEach(celda => {
            if (checkbox.checked) {
                celda.style.display = ''; //por alguna razon, si pongo block, me satura toda la tabla
            } else {
                celda.style.display = 'none';
            }
        });
    });
}
