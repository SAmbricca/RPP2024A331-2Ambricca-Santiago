class Vehiculo {
    constructor(id, modelo, anoFabricacion, velMax) {
        this.id = id;
        this.modelo = modelo;
        this.anoFabricacion = anoFabricacion;
        this.velMax = velMax;
    }

    toString() {
        return `Vehiculo: ${this.modelo}. Id: ${this.id}. Modelo: ${this.modelo}. anoFabricacion: ${this.anoFabricacion}. velMax: ${this.velMax}`;
    }

    toArray() {
        return [this.id, this.modelo, this.anoFabricacion, this.velMax];
    }
}

class Auto extends Vehiculo{
    constructor(id, modelo, anoFabricacion, velMax, cantidadPuertas, asientos) {
        super(id, modelo, anoFabricacion, velMax);
        this.cantidadPuertas = cantidadPuertas;
        this.asientos = asientos;
    }

    toString() {
        return `${super.toString()}. cantidadPuertas: ${this.cantidadPuertas}. Asientos: ${this.asientos}.`;
    }

    toArray() {
        return super.toArray().concat([this.cantidadPuertas, this.asientos, this.cantGoles]);
    }
}

class Camion extends Vehiculo {
    constructor(id, modelo, anoFabricacion, velMax, carga, autonomia) {
        super(id, modelo, anoFabricacion, velMax);
        this.carga = carga;
        this.autonomia = autonomia;
    }

    toString() {
        return `${super.toString()}. Carga: ${this.carga}. Autonomia: ${this.autonomia}.`;
    }

    toArray() {
        return super.toArray().concat([this.carga, this.autonomia, this.anoGrad]);
    }
}

let listaVehiculos = [];

window.onload = function() {
    document.getElementById('formularioABM').style.display = 'none';
    obtenerDatos();
};

// 3) Generar una lista en memoria de la jerarquía de clases implementada en el punto 1
function obtenerDatos() {
    const http = new XMLHttpRequest();

    http.open("GET", "https://examenesutn.vercel.app/api/VehiculoAutoCamion", false); // false para garantizar la secuencialidad
    http.send();

    if (http.status === 200) {
        try {
            const data = JSON.parse(http.responseText);

            // Recorro cada objeto en el array JSON y creo instancias de las clases
            data.forEach(elemento => {
                if ("cantidadPuertas" in elemento && "asientos" in elemento) {
                    var auto = new Auto(
                        elemento.id,
                        elemento.modelo,
                        elemento.anoFabricacion,
                        elemento.velMax,
                        elemento.cantidadPuertas,
                        elemento.asientos
                    );
                    listaVehiculos.push(auto);
                } else if ("carga" in elemento && "autonomia" in elemento) {
                    var camion = new Camion(
                        elemento.id,
                        elemento.modelo,
                        elemento.anoFabricacion,
                        elemento.velMax,
                        elemento.carga,
                        elemento.autonomia
                    );
                    listaVehiculos.push(camion);
                }
            });

            // En caso de tener una respuesta con código 200 Generar la lista en memoria y mostrar el Formulario Lista,
            console.log("Lista de Vehiculos: ", listaVehiculos);
            mostrarFormularioLista(listaVehiculos);
        } catch (error) {
            console.error(" ", error);
        }
    } else {
        console.warn("Error al obtener los datos:", xhr.status);
    }
}

function mostrarFormularioLista(arrayVehiculos) {
    // Limpio la tabla
    let contenidoTabla = document.getElementById('datosTabla');
    contenidoTabla.innerHTML = '';

    // Crear las filas de la tabla con los datos del array
    arrayVehiculos.forEach(vehiculo => {
        let fila = document.createElement('tr');

        // Generar la fila con los datos del vehículo
        fila.innerHTML = `
            <td>${vehiculo.id}</td>
            <td>${vehiculo.modelo}</td>
            <td>${vehiculo.anoFabricacion}</td>
            <td>${vehiculo.velMax}</td>
            <td>${vehiculo instanceof Auto ? vehiculo.cantidadPuertas : 'N/A'}</td>
            <td>${vehiculo instanceof Auto ? vehiculo.asientos : 'N/A'}</td>
            <td>${vehiculo instanceof Camion ? vehiculo.carga : 'N/A'}</td>
            <td>${vehiculo instanceof Camion ? vehiculo.autonomia : 'N/A'}</td>
            <td><button type="button" onclick="modificarVehiculo(event, ${vehiculo.id})">Modificar</button></td>
            <td><button type="button" onclick="eliminarVehiculo(${vehiculo.id})">Eliminar</button></td>
        `;

        contenidoTabla.appendChild(fila);
    });
}


// 4- Implementar Funcionalidad de "Alta".
function FormularioDatosAAbm() {
    document.getElementById('formularioDatos').style.display = 'none';
    document.getElementById('formularioABM').style.display = 'block';
    document.getElementById('tituloFormulario').innerText = 'Formulario ABM';

    // Bloqueo el ID y vacio los datos
    document.getElementById('id ABM').disabled = true;

    document.getElementById('id ABM').value = ''; // Limpio y luego sera generado automaticamente
    document.getElementById('modelo ABM').value = '';
    document.getElementById('anoFabricacion ABM').value = '';
    document.getElementById('velMax ABM').value = '';
    document.getElementById('cantidadPuertas ABM').value = '';
    document.getElementById('asientos ABM').value = '';
    document.getElementById('carga ABM').value = '';
    document.getElementById('autonomia ABM').value = '';

    // Muestro dependiendo del tipo
    let tipoABM = document.getElementById('tipo ABM');
    if (tipoABM.value == '0') { // 0 para Auto
        document.getElementById('datosAutos').style.display = 'block';
        document.getElementById('datosCamiones').style.display = 'none';
    } else if (tipoABM.value == '1') { // 1 para Camion
        document.getElementById('datosAutos').style.display = 'none';
        document.getElementById('datosCamiones').style.display = 'block';
    }
}

document.getElementById('tipo ABM').addEventListener('change', function() {
    let tipoABM = document.getElementById('tipo ABM');
    if (tipoABM.value == '0') {
        document.getElementById('datosAutos').style.display = 'block';
        document.getElementById('datosCamiones').style.display = 'none';
    } else if (tipoABM.value == '1') {
        document.getElementById('datosAutos').style.display = 'none';
        document.getElementById('datosCamiones').style.display = 'block';
    }
});

function FormularioAbmADatos() {
    document.getElementById('formularioDatos').style.display = 'block';
    document.getElementById('formularioABM').style.display = 'none';
    document.getElementById('tituloFormulario').innerText = 'Formulario Lista';
}

function aceptarFormularioAbm() {
    console.log("BOTON ACEPTAR");
    var id = document.getElementById('id ABM').value;
    var modelo = document.getElementById('modelo ABM').value;
    var anoFabricacion = parseInt(document.getElementById('anoFabricacion ABM').value);
    var velMax = parseFloat(document.getElementById('velMax ABM').value);
    var tipo = document.getElementById('tipo ABM').value;

    // Validaciones
    try {
        if (!modelo || modelo.trim() === "") {
            alert("El modelo no puede estar vacío");
            return;
        }

        if (isNaN(anoFabricacion) || anoFabricacion < 1985) {
            alert("El año de fabricación debe ser mayor a 1985");
            return;
        }

        if (isNaN(velMax) || velMax < 0) {
            alert("La velocidad máxima debe ser mayor a 0");
            return;
        }

        let datosVehiculo;
        let datosVehiculoConId;

        if (tipo === "0") { // Auto
            let cantidadPuertas = parseInt(document.getElementById('cantidadPuertas ABM').value);
            let asientos = parseInt(document.getElementById('asientos ABM').value);

            if (isNaN(cantidadPuertas) || cantidadPuertas < 2) {
                alert("La cantidad de puertas debe ser mayor a 2");
                return;
            }

            if (isNaN(asientos) || asientos < 2) {
                alert("La cantidad de asientos debe ser mayor a 2");
                return;
            }

            datosVehiculo = { modelo, anoFabricacion, velMax, cantidadPuertas, asientos };
            datosVehiculoConId = {id, modelo, anoFabricacion, velMax, cantidadPuertas, asientos };
        } else if (tipo === "1") { // Camion
            let carga = parseFloat(document.getElementById('carga ABM').value);
            let autonomia = parseFloat(document.getElementById('autonomia ABM').value);

            if (isNaN(carga) || carga <= 0) {
                alert("La carga debe ser mayor a 0");
                return;
            }

            if (isNaN(autonomia) || autonomia <= 0) {
                alert("La autonomía debe ser mayor a 0");
                return;
            }

            datosVehiculo = { modelo, anoFabricacion, velMax, carga, autonomia };
            datosVehiculoConId = {id, modelo, anoFabricacion, velMax, carga, autonomia };
        }

        console.log("Formulario ABM aceptado:", datosVehiculo);
        // Me fijo si es una modificacion o un alta
        if (id) {
             // Le añado el id
            fetchPut(datosVehiculoConId);
        } else {
            fetchPost(datosVehiculo);
        }
    } catch (error) {
        console.error("Error en la función aceptarFormularioAbm:", error);
    }
}




function cancelarFormularioAbm(){
    // limpio el formularioABM
    limpiarFormularioAbm();

    mostrarFormularioLista(listaVehiculos);

    // vuelvo atras la visualizacion
    FormularioAbmADatos();
}

function limpiarFormularioAbm() {
    document.getElementById('id ABM').value = ''; // Limpio y luego sera generado automaticamente
    document.getElementById('modelo ABM').value = '';
    document.getElementById('anoFabricacion ABM').value = '';
    document.getElementById('velMax ABM').value = '';
    document.getElementById('cantidadPuertas ABM').value = '';
    document.getElementById('asientos ABM').value = '';
    document.getElementById('carga ABM').value = '';
    document.getElementById('autonomia ABM').value = '';
}

// 5)
function modificarVehiculo(event, id) {
    event.preventDefault(); //Tuve que añadir esta linea de codigo para que no me recargue la pagina al dar click en el boton
    // Verifico que el vehiculo exista
    const vehiculo = listaVehiculos.find(v => v.id === id);
    
    if (!vehiculo) {
        alert("Vehículo no encontrado.");
        return;
    }

    document.getElementById('formularioDatos').style.display = 'none';
    document.getElementById('formularioABM').style.display = 'block';
    document.getElementById('tituloFormulario').innerText = 'Modificar Vehiculo';

    document.getElementById('id ABM').value = vehiculo.id;
    document.getElementById('id ABM').disabled = true;

    if (vehiculo instanceof Auto) {
        document.getElementById('modelo ABM').value = vehiculo.modelo;
        document.getElementById('anoFabricacion ABM').value = vehiculo.anoFabricacion;
        document.getElementById('velMax ABM').value = vehiculo.velMax;
        document.getElementById('cantidadPuertas ABM').value = vehiculo.cantidadPuertas;
        document.getElementById('asientos ABM').value = vehiculo.asientos;
        
        document.getElementById('datosAutos').style.display = 'block';
        document.getElementById('datosCamiones').style.display = 'none';
    } else if (vehiculo instanceof Camion) {
        document.getElementById('modelo ABM').value = vehiculo.modelo;
        document.getElementById('anoFabricacion ABM').value = vehiculo.anoFabricacion;
        document.getElementById('velMax ABM').value = vehiculo.velMax;
        document.getElementById('carga ABM').value = vehiculo.carga;
        document.getElementById('autonomia ABM').value = vehiculo.autonomia;

        document.getElementById('datosAutos').style.display = 'none';
        document.getElementById('datosCamiones').style.display = 'block';
    }
}


async function fetchPost(datosVehiculo){
    try {
        let respuesta = await fetch("https://examenesutn.vercel.app/api/VehiculoAutoCamion", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(datosVehiculo)
        });
    
        // spinner.style.display = 'none';
    
        if (respuesta.ok) {
            // Creo un nuevo vehiculo con el id obtenido y lo agrego a la lista
            let resultado = await respuesta.json();
            let nuevoVehiculo = {
                id: resultado.id, ...datosVehiculo
            };
    
            listaVehiculos.push(nuevoVehiculo);
            alert("Alta realizada con exito");
            mostrarFormularioLista(listaVehiculos); // actualizo la lista y el formulario
        } else {
            alert("No se pudo realizar el fetch");
            mostrarFormularioLista(listaVehiculos);
        }
    } catch (error) {
        console.error(" ", error);
        // spinner.style.display = 'none';
        alert("No se pudo realizar el fetch");
        mostrarFormularioLista(listaVehiculos);
    } finally {
        FormularioAbmADatos();
    }
} 
async function fetchPut(datosVehiculo) {
    try {
        let respuesta = await fetch("https://examenesutn.vercel.app/api/VehiculoAutoCamion", {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(datosVehiculo)
        });

        if (respuesta.ok) {
            console.log("entre");
            let resultado = await respuesta.text();
            let i = listaVehiculos.findIndex(vehiculo => vehiculo.id === datosVehiculo.id);
            if (i !== -1) {
                listaVehiculos[i] = { id: resultado.id, ...datosVehiculo };
            }

            alert("Modificacion realizada con exito");
            mostrarFormularioLista(listaVehiculos);
        } else {
            alert("No se pudo realizar la modificacion");
            mostrarFormularioLista(listaVehiculos);
        }
    } catch (error) {
        console.error("Error:", error);
        alert(error);
        mostrarFormularioLista(listaVehiculos);
    } finally {
        FormularioAbmADatos();
    }
}
