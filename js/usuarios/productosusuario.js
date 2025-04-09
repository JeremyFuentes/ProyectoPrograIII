document.addEventListener("DOMContentLoaded", () => {
    const usuarioId = localStorage.getItem("usuarioId");
    const nombre = localStorage.getItem("nombreUsuario");

      // Verificar sesión activa
  if (!usuarioId || !nombreUsuario) {
    alert("Acceso denegado. Por favor inicia sesión.");
    window.location.href = "../login.html";
    return;
  }

    const nombreSpan = document.getElementById("nombreUsuario");
    if (nombreSpan) nombreSpan.textContent = nombre;

    cargarProductosConFavoritos();
});

async function cargarProductosConFavoritos() {
    const productosContainer = document.getElementById("productosContainer");
    const usuarioId = localStorage.getItem("usuarioId");
    productosContainer.innerHTML = "";

    try {
        const [productosRes, favoritosRes] = await Promise.all([
            fetch("https://localhost:7291/Productos/conImagenPrincipal"),
            fetch(`https://localhost:7291/Favoritos/Usuario/${usuarioId}`)
        ]);

        if (!productosRes.ok || !favoritosRes.ok) throw new Error("Error al obtener productos o favoritos");

        const productos = await productosRes.json();
        const favoritos = await favoritosRes.json();

        const favoritosIds = {}; // clave = productoId, valor = idFavorito
        favoritos.forEach(f => {
            favoritosIds[f.productoId] = f.favoritoId;
        });

        productos.forEach(p => {
            const esFavorito = favoritosIds.hasOwnProperty(p.productoId);
            const idFavorito = favoritosIds[p.productoId] || "";
            const icono = esFavorito ? "fa-solid fa-heart text-danger" : "fa-regular fa-heart text-danger";

            const card = document.createElement("div");
            card.classList.add("col-md-4");
            card.innerHTML = `
  <div class="card border-0 h-100 position-relative">
    <img src="https://localhost:7291${p.imagen || '/imagenes/placeholder.png'}"
         class="card-img-top product-img" alt="${p.nombre}">
    <button class="btn btn-link position-absolute top-0 end-0 m-2 p-0 icon-fav"
            data-producto-id="${p.productoId}" data-favorito="${esFavorito}" data-id-favorito="${idFavorito}">
      <i class="${icono}" style="font-size: 20px;"></i>
    </button>
    <div class="card-body">
      <p class="fw-semibold mb-1">${p.nombre}</p>
      <div class="d-flex align-items-center mb-1">
        <span class="text-warning me-1">
          <i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i>
          <i class="fa-regular fa-star"></i><i class="fa-regular fa-star"></i>
        </span>
        <span class="text-muted small ms-1">3.5/5</span>
      </div>
      <p class="fw-bold">$${p.precio.toFixed(2)}</p>

      <!-- Botón de agregar al carrito (sin funcionalidad) -->
      <button class="btn btn-outline-dark w-100 mt-2" disabled>
        <i class="fa fa-shopping-cart me-2"></i>Agregar al carrito
      </button>
    </div>
  </div>
`;


            productosContainer.appendChild(card);
        });

        // Aplicar eventos a íconos
        document.querySelectorAll(".icon-fav").forEach(btn => {
            btn.addEventListener("click", async () => {
                const icon = btn.querySelector("i");
                const productoId = btn.dataset.productoId;
                const esFavorito = btn.dataset.favorito === "true";
                const idFavorito = btn.dataset.idFavorito;

                // 💥 Animación pop
                icon.classList.add("pop");
                setTimeout(() => icon.classList.remove("pop"), 200);

                if (esFavorito && idFavorito) {
                    await fetch(`https://localhost:7291/Favoritos/Eliminarfavorito/${idFavorito}`, {
                        method: "DELETE"
                    });
                    icon.className = "fa-regular fa-heart text-danger";
                    btn.dataset.favorito = "false";
                    btn.dataset.idFavorito = "";
                } else {
                    const res = await fetch(`https://localhost:7291/Favoritos/Agregar`, {
                        method: "POST",
                        headers: { "Content-Type": "application/json" },
                        body: JSON.stringify({ usuarioId: parseInt(usuarioId), productoId: parseInt(productoId) })
                    });
                    const nuevoFavorito = await res.json();
                    icon.className = "fa-solid fa-heart text-danger";
                    btn.dataset.favorito = "true";
                    btn.dataset.idFavorito = nuevoFavorito.idFavorito;
                }
            });
        });

    } catch (error) {
        console.error("Error al cargar productos:", error);
        productosContainer.innerHTML = `<p class="text-danger">No se pudieron cargar los productos :(</p>`;
    }
}

// 🔹 Logout
document.getElementById("logoutUsuario")?.addEventListener("click", () => {
    localStorage.removeItem("usuarioId");
    localStorage.removeItem("nombreUsuario");
    localStorage.removeItem("token");
    window.location.href = "../index.html";
});
