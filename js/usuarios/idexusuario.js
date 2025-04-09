document.addEventListener("DOMContentLoaded", () => {
  const nombreUsuario = localStorage.getItem("nombreUsuario");
  const usuarioId = localStorage.getItem("usuarioId");

  // Verificar sesión activa
  if (!usuarioId || !nombreUsuario) {
    alert("Acceso denegado. Por favor inicia sesión.");
    window.location.href = "../login.html";
    return;
  }

  // Mostrar nombre del usuario en la navbar
  const spanNombre = document.getElementById("nombreUsuario");
  if (spanNombre) {
    spanNombre.textContent = nombreUsuario;
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
});
