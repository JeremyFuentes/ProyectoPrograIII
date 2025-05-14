const API_BASE = "https://localhost:7291";
const params = new URLSearchParams(window.location.search);
const productoId = params.get("productoId");

const usuarioId = localStorage.getItem("usuarioId");
const nombreUsuario = localStorage.getItem("nombreUsuario");

if (!usuarioId || !nombreUsuario) {
    alert("Acceso denegado. Por favor inicia sesión.");
    window.location.href = "../login.html";
}

document.getElementById("nombreUsuario").textContent = nombreUsuario;

// Declaramos `producto` fuera para usarla globalmente
let producto = null;

document.addEventListener("DOMContentLoaded", async () => {
    try {
        const [productoRes, imagenesRes, favoritosRes] = await Promise.all([
            fetch(`${API_BASE}/productos/ObtenerporId/${productoId}`),
            fetch(`${API_BASE}/imagenesProducto/producto/${productoId}`),
            fetch(`${API_BASE}/favoritos/usuario/${usuarioId}`)
        ]);

        if (!productoRes.ok || !imagenesRes.ok || !favoritosRes.ok)
            throw new Error("Error al obtener datos del producto");

        producto = await productoRes.json(); // Asignación aquí
        const imagenes = await imagenesRes.json();
        const favoritos = await favoritosRes.json();

        const favorito = favoritos.find(f => f.productoId == productoId);
        const esFavorito = !!favorito;
        const idFavorito = esFavorito ? favorito.favoritoId : "";

        document.getElementById("tituloProducto").textContent = producto.nombre;

        // Verificar si el producto está inactivo
        const btnAgregarCarrito = document.querySelector(".btn-dark.px-4.rounded-pill");
        if (!producto.estado) {
            btnAgregarCarrito.textContent = "No disponible";
            btnAgregarCarrito.disabled = true;
            btnAgregarCarrito.classList.add("btn-secondary");
            btnAgregarCarrito.classList.remove("btn-dark");
        }

        document.getElementById("precioProducto").textContent = producto.precio.toFixed(2);
        document.getElementById("descripcionProducto").textContent = producto.descripcion;
        const stockTexto = document.getElementById("stockTexto");
        const stockContenedor = document.getElementById("stockDisponible");

        stockTexto.textContent = `${producto.stock} unidades disponibles`;

        if (producto.stock >= 10) {
            stockContenedor.classList.add("text-success"); // verde
        } else if (producto.stock >= 5) {
            stockContenedor.classList.add("text-warning"); // amarillo
        } else {
            stockContenedor.classList.add("text-danger"); // rojo
        }


        const imagenPrincipal = document.getElementById("imagenPrincipal");
        const miniaturas = document.getElementById("miniaturas");

        function mostrarImagenPrincipal(url) {
            imagenPrincipal.src = url;

            document.querySelectorAll(".miniatura").forEach(img => {
                img.classList.remove("thumb-activa");
            });

            const activa = document.querySelector(`.miniatura[src="${url}"]`);
            if (activa) activa.classList.add("thumb-activa");
        }

        imagenes.forEach((img, i) => {
            const thumb = document.createElement("img");
            thumb.src = API_BASE + img.urlImagen;
            thumb.alt = producto.nombre;
            thumb.className = "img-thumbnail miniatura mb-2";
            thumb.style.width = "100px";
            thumb.style.cursor = "pointer";

            thumb.addEventListener("click", () => mostrarImagenPrincipal(thumb.src));

            miniaturas.appendChild(thumb);
            if (i === 0) mostrarImagenPrincipal(thumb.src);
        });

        const iconoFav = document.getElementById("iconoFavorito");
        const btnFav = document.getElementById("btnfavorito");

        btnFav.dataset.favorito = esFavorito ? "true" : "false";
        btnFav.dataset.idFavorito = idFavorito || "";

        if (esFavorito) {
            iconoFav.className = "fa-solid fa-heart text-danger";
        } else {
            iconoFav.className = "fa-regular fa-heart text-danger";
        }

        btnFav.addEventListener("click", async () => {
            const esFav = btnFav.dataset.favorito === "true";
            const idFav = btnFav.dataset.idFavorito;

            iconoFav.classList.add("pop");
            setTimeout(() => iconoFav.classList.remove("pop"), 200);

            if (esFav && idFav) {
                await fetch(`${API_BASE}/favoritos/Eliminarfavorito/${idFav}`, {
                    method: "DELETE"
                });
                iconoFav.className = "fa-regular fa-heart text-danger";
                btnFav.dataset.favorito = "false";
                btnFav.dataset.idFavorito = "";
            } else {
                const res = await fetch(`${API_BASE}/favoritos/Agregar`, {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({ usuarioId: parseInt(usuarioId), productoId: parseInt(productoId) })
                });
                const nuevo = await res.json();
                iconoFav.className = "fa-solid fa-heart text-danger";
                btnFav.dataset.favorito = "true";
                btnFav.dataset.idFavorito = nuevo.idFavorito;
            }
        });

    } catch (error) {
        console.error("❌ Error al cargar detalles del producto:", error);
        const contenedor = document.getElementById("detalleContainer") || document.querySelector(".container");
        contenedor.innerHTML = `<p class='text-danger'>No se pudo cargar el producto.</p>`;
    }

    // Cantidad controlada
    let cantidad = 1;
    const cantidadSpan = document.getElementById("cantidadProducto");
    const btnMas = document.getElementById("btnMas");
    const btnMenos = document.getElementById("btnMenos");

    btnMas.addEventListener("click", () => {
        if (producto && cantidad < producto.stock) {
            cantidad++;
            cantidadSpan.textContent = cantidad;
        } else {
            alert("Has alcanzado el límite de stock disponible.");
        }
    });

    btnMenos.addEventListener("click", () => {
        if (cantidad > 1) {
            cantidad--;
            cantidadSpan.textContent = cantidad;
        }
    });

    document.querySelector(".btn-dark.px-4.rounded-pill").addEventListener("click", async () => {
        const cantidadSeleccionada = parseInt(document.getElementById("cantidadProducto").textContent);

        try {
            const res = await fetch(`${API_BASE}/carrito/Agregar`, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    usuarioId: parseInt(localStorage.getItem("usuarioId")),
                    productoId: parseInt(productoId),
                    cantidad: cantidadSeleccionada,
                    estadoProductoId: 1,
                    precioUnitario: producto.precio // ✅ ya tienes el producto global
                })
            });

            const data = await res.json();

            if (res.ok) {
                alert("✅ Producto agregado al carrito.");
            } else if (res.status === 409) {
                alert("⚠️ El producto ya está en tu carrito.");
            } else {
                alert("❌ No se pudo agregar el producto al carrito.");
                console.error("Respuesta inesperada del servidor:", data);
            }

        } catch (err) {
            console.error("❌ Error de red:", err);
            alert("Error de red al intentar agregar el producto.");
        }
    });
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