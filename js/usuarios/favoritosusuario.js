document.addEventListener("DOMContentLoaded", () => {
    const usuarioId = localStorage.getItem("usuarioId");
    const nombre = localStorage.getItem("nombreUsuario");

    if (!usuarioId) {
        alert("Debes iniciar sesión para acceder.");
        window.location.href = "../login.html";
        return;
    }

    const nombreSpan = document.getElementById("nombreUsuario");
    if (nombreSpan) nombreSpan.textContent = nombre;

    cargarFavoritos();
});

async function cargarFavoritos() {
    const container = document.getElementById("favoritosContainer");
    const usuarioId = localStorage.getItem("usuarioId");
    container.innerHTML = "";

    try {
        const res = await fetch(`https://localhost:7291/favoritos/ConProductoPorUsuario/${usuarioId}`);
        if (!res.ok) throw new Error("Error al obtener favoritos");

        const favoritos = await res.json();

        if (favoritos.length === 0) {
            container.innerHTML = "<p class='text-center'>No tienes productos favoritos aún.</p>";
            return;
        }

        favoritos.forEach(fav => {
            const producto = fav.producto;
            if (!producto) return;
        
            // 🔍 Obtener ruta de imagen desde imagenesProducto
            const imagenRuta = producto.imagenesProducto?.[0]?.urlImagen;
const imagenUrl = imagenRuta
    ? `https://localhost:7291${imagenRuta}`
    : 'https://localhost:7291/imagenes/placeholder.png';

console.log("🖼️ Imagen URL cargada:", imagenUrl);
        
            const card = document.createElement("div");
            card.classList.add("col-md-3", "col-sm-6", "mb-4");
        
            card.innerHTML = `
                <div class="card border-0 h-100">
                    <img src="${imagenUrl}" class="card-img-top product-img" alt="${producto.nombre}">
                    <div class="card-body text-start">
                        <p class="fw-semibold mb-1">${producto.nombre}</p>
                        <div class="d-flex align-items-center mb-1">
                            <span class="text-warning me-1">
                                <i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i>
                                <i class="fa-regular fa-star"></i><i class="fa-regular fa-star"></i>
                            </span>
                            <span class="text-muted small ms-1">4.0/5</span>
                        </div>
                        <p class="fw-bold">$${producto.precio?.toFixed(2) || '0.00'}</p>
                        <button class="btn btn-outline-danger btn-sm w-100 eliminar-fav" data-id="${fav.favoritoId}">
                            <i class="fa fa-trash"></i> Eliminar
                        </button>
                    </div>
                </div>
            `;
        
            container.appendChild(card);
        });
        

        // 🗑 Eventos de eliminación
        document.querySelectorAll(".eliminar-fav").forEach(btn => {
            btn.addEventListener("click", async () => {
                const id = btn.dataset.id;
                if (!confirm("¿Eliminar de favoritos?")) return;

                const del = await fetch(`https://localhost:7291/Favoritos/Eliminarfavorito/${id}`, {
                    method: "DELETE"
                });

                if (del.ok) {
                    cargarFavoritos();
                } else {
                    alert("No se pudo eliminar el favorito.");
                }
            });
        });

    } catch (err) {
        console.error("Error al cargar favoritos:", err);
        container.innerHTML = "<p class='text-danger'>Hubo un error al cargar los favoritos :(</p>";
    }
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

