// Función para agregar una nueva película a la tabla
document.getElementById('formAgregarPelicula').addEventListener('submit', function (event) {
    event.preventDefault(); // Evita que el formulario se envíe

    // Obtener los valores del formulario
    const titulo = document.getElementById('titulo').value;
    const anio = document.getElementById('anio').value;
    const director = document.getElementById('director').value;
    const categoria = document.getElementById('categoria').value;
    const stream = document.getElementById('stream').value;
    const portada = document.getElementById('portada').value;

    // Crear una nueva fila en la tabla
    const nuevaFila = document.createElement('tr');
    nuevaFila.innerHTML = `
                <td>${titulo}</td>
                <td>${anio}</td>
                <td>${director}</td>
                <td>${categoria}</td>
                <td><a href="${stream}" target="_blank">Ver en Stream</a></td>
                <td><img src="${portada}" alt="Portada" class="img-fluid" style="width: 50px;"></td>
                <td>
                    <button class="btn btn-warning btn-sm" onclick="editarPelicula(this)">Editar</button>
                    <button class="btn btn-danger btn-sm" onclick="eliminarPelicula(this)">Eliminar</button>
                </td>
            `;

    // Añadir la nueva fila a la tabla
    document.getElementById('tablaPeliculas').appendChild(nuevaFila);

    var myModal = new bootstrap.Modal(document.getElementById('agregarModal'));
    myModal.hide();

    document.getElementById('formAgregarPelicula').reset();
});
