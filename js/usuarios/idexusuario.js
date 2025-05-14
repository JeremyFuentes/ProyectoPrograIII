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

document.addEventListener("DOMContentLoaded", async () => {
  const contenedorLaptops = document.getElementById("laptopsNuevas");
  const contenedorAsus = document.getElementById("productosAsus");

  // Obtener hasta 4 laptops
  const laptops = await fetch("https://localhost:7291/productos/filtrar?categoriaId=1") // ← ID real de 'laptops'
    .then(r => r.json())
    .then(data => data.slice(0, 4));

  // Obtener hasta 4 Asus
  const asus = await fetch("https://localhost:7291/productos/filtrar?marcaId=1") // ← ID real de 'Asus'
    .then(r => r.json())
    .then(data => data.slice(0, 4));

  const crearCard = (producto) => `
  <div class="col-md-3 col-sm-6 mb-4">
    <div class="card card-producto h-100">
      <img src="https://localhost:7291${producto.imagen}" class="card-img-top" alt="${producto.nombre}">
      <div class="card-body text-start">
        <p class="fw-semibold">${producto.nombre}</p>
        <p class="fw-bold">$${producto.precio.toFixed(2)}</p>
        <a href="detalleproducto.html?productoId=${producto.productoId}" class="btn btn-outline-dark w-100 mt-auto">Ver más</a>
      </div>
    </div>
  </div>
`;


  laptops.forEach(p => contenedorLaptops.innerHTML += crearCard(p));
  asus.forEach(p => contenedorAsus.innerHTML += crearCard(p));
});

document.addEventListener("DOMContentLoaded", () => {
  const searchInput = document.querySelector(".search-input");

  if (searchInput) {
    searchInput.addEventListener("keypress", function (e) {
      if (e.key === "Enter") {
        const termino = this.value.trim();
        if (termino) {
          window.location.href = `/html/Usuarios/productosusuario.html?busqueda=${encodeURIComponent(termino)}`;
        }
      }
    });
  }
});
