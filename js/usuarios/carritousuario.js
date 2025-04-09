document.addEventListener("DOMContentLoaded", () => {
    const usuarioId = localStorage.getItem("usuarioId");
    const nombre = localStorage.getItem("nombreUsuario");

    // 🚫 Si no hay sesión, redirige
    if (!usuarioId || !nombre) {
        alert("Debes iniciar sesión para acceder al carrito.");
        window.location.href = "../login.html";
        return;
    }

    // 👤 Mostrar nombre del usuario en el navbar
    const nombreSpan = document.getElementById("nombreUsuario");
    if (nombreSpan) {
        nombreSpan.textContent = nombre;
    }

    // Cerrar sesión
    const btnLogout = document.getElementById("logoutUsuario");
    if (btnLogout) {
        btnLogout.addEventListener("click", () => {
            localStorage.removeItem("usuarioId");
            localStorage.removeItem("nombreUsuario");
            localStorage.removeItem("token");
            window.location.href = "../login.html";
        });
    }
    // 🛒 Aquí puedes llamar a tu función para cargar el carrito si ya tienes una
    // cargarCarrito(usuarioId); // <- si tienes una función ya creada para esto
});
