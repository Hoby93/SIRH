function display(modalId) {
    var modal = document.getElementById(modalId);
    var primary = document.getElementById("primary");

    modal.style.display = "block";
    primary.style.opacity = 0.7;
}

function hidde(modalId) {
    var modal = document.getElementById(modalId);
    var primary = document.getElementById("primary");

    modal.style.display = "none";
    primary.style.opacity = 1;
}