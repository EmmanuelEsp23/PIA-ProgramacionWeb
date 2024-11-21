// MiPerfil.js
document.addEventListener('DOMContentLoaded', function () {

    var editButton = document.getElementById('editButton');

    var modal = new bootstrap.Modal(document.getElementById('editModal'));

    editButton.addEventListener('click', function () {
        modal.show(); 
    });
});


   

