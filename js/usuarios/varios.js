document.addEventListener("DOMContentLoaded", () => {
  const nombreUsuario = localStorage.getItem("nombreUsuario");
  const usuarioId = localStorage.getItem("usuarioId");

  // Verificar si hay sesión activa
  if (!usuarioId || !nombreUsuario) {
    alert("Acceso denegado. Por favor inicia sesión.");
    window.location.href = "../../html/login.html";
    return;
  }

  // Mostrar nombre en el navbar
  const spanNombre = document.getElementById("nombreUsuario");
  if (spanNombre) {
    spanNombre.textContent = nombreUsuario;
  }

  // Acción de cerrar sesión
  const btnLogout = document.getElementById("logoutUsuario");
  if (btnLogout) {
    btnLogout.addEventListener("click", () => {
      localStorage.clear();
      window.location.href = "../../html/login.html";
    });
  }
});
