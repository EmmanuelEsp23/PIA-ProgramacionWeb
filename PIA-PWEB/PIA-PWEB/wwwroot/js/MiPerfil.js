// MiPerfil.js
document.addEventListener('DOMContentLoaded', function () {

    var editButton = document.getElementById('editButton');

    var modal = new bootstrap.Modal(document.getElementById('editModal'));

    editButton.addEventListener('click', function () {
        modal.show(); 
    });
});


    function changeProfilePic(event) {
                const file = event.target.files[0];
    if (file) {
                    const reader = new FileReader();
    reader.onload = function(e) {
                        const imgElement = document.getElementById('profilePic');
    const iconElement = document.getElementById('profileIcon');

    imgElement.src = e.target.result;
    imgElement.style.display = 'block';
    iconElement.style.display = 'none';
                    };
    reader.readAsDataURL(file);
                }
            }


