document.addEventListener("DOMContentLoaded", () => {
  const usuarioId = localStorage.getItem("usuarioId");
  const nombre = localStorage.getItem("nombreUsuario");

  if (!usuarioId || !nombre) {
    alert("Acceso denegado. Por favor inicia sesión.");
    window.location.href = "../../html/login.html";
    return;
  }

  document.getElementById("nombreUsuario").textContent = nombre;

  cargarFiltros();
  const params = new URLSearchParams(window.location.search);
  const categoriaId = params.get("categoria");
  const marcaId = params.get("marca");
  cargarProductosConFavoritos({ categoriaId, marcaId });

  document.querySelector(".search-input")?.addEventListener("keypress", (e) => {
    if (e.key === "Enter") {
      const valor = e.target.value.trim();
      if (valor) {
        window.location.href = `productosusuario.html?busqueda=${encodeURIComponent(valor)}`;
      }
    }
  });

  document.getElementById("logoutUsuario")?.addEventListener("click", () => {
    localStorage.clear();
    window.location.href = "../index.html";
  });

  document.getElementById("btnAplicarFiltros")?.addEventListener("click", () => {
    const categoriaId = document.getElementById("filtroCategoria")?.value;
    const marcaId = document.getElementById("filtroMarca")?.value;

    const params = new URLSearchParams(window.location.search);
    if (categoriaId) params.set("categoria", categoriaId);
    else params.delete("categoria");
    if (marcaId) params.set("marca", marcaId);
    else params.delete("marca");

    window.location.href = `productosusuario.html?${params.toString()}`;
  });
});

async function cargarFiltros() {
  const categoriaSel = document.getElementById("filtroCategoria");
  const marcaSel = document.getElementById("filtroMarca");

  if (!categoriaSel || !marcaSel) return;

  const [cats, marcas] = await Promise.all([
    fetch("https://localhost:7291/auxiliares/categorias/todas").then(r => r.json()),
    fetch("https://localhost:7291/auxiliares/marcas/todas").then(r => r.json())
  ]);

  cats.forEach(c => {
    const opt = document.createElement("option");
    opt.value = c.categoriaId;
    opt.textContent = c.nombre;
    categoriaSel.appendChild(opt);
  });

  marcas.forEach(m => {
    const opt = document.createElement("option");
    opt.value = m.marcaId;
    opt.textContent = m.nombre;
    marcaSel.appendChild(opt);
  });

  const params = new URLSearchParams(window.location.search);
  const categoriaActual = params.get("categoria");
  const marcaActual = params.get("marca");
  if (categoriaActual) categoriaSel.value = categoriaActual;
  if (marcaActual) marcaSel.value = marcaActual;
}

async function cargarProductosConFavoritos(filtros = {}) {
  const container = document.getElementById("productosContainer");
  const usuarioId = localStorage.getItem("usuarioId");
  container.innerHTML = "";

  const { categoriaId, marcaId } = filtros;
  const params = new URLSearchParams(window.location.search);
  const busqueda = params.get("busqueda");

  let endpoint = "https://localhost:7291/productos/conImagenPrincipal";

  if (busqueda) {
    endpoint = `https://localhost:7291/productos/buscarConImagen/${encodeURIComponent(busqueda)}`;
  } else if (categoriaId && marcaId) {
    endpoint = `https://localhost:7291/productos/filtrar?categoriaId=${categoriaId}&marcaId=${marcaId}`;
  } else if (categoriaId) {
    endpoint = `https://localhost:7291/productos/filtrar?categoriaId=${categoriaId}`;
  } else if (marcaId) {
    endpoint = `https://localhost:7291/productos/filtrar?marcaId=${marcaId}`;
  }


  try {
    const [productos, favoritos] = await Promise.all([
      fetch(endpoint).then(r => r.json()),
      fetch(`https://localhost:7291/Favoritos/Usuario/${usuarioId}`).then(r => r.json())
    ]);

    const favoritosMap = {};
    favoritos.forEach(f => favoritosMap[f.productoId] = f.favoritoId);

    productos.forEach(p => {
      const fav = favoritosMap[p.productoId];
      const icono = fav ? "fa-solid fa-heart text-danger" : "fa-regular fa-heart text-danger";
      const img = p.imagen || "/imagenes/placeholder.png";

      const card = document.createElement("div");
      card.classList.add("col-md-4");
      card.innerHTML = `
        <div class="card border-0 h-100 position-relative">
          <img src="https://localhost:7291${img}" class="card-img-top product-img" alt="${p.nombre}">
          <button class="btn btn-link position-absolute top-0 end-0 m-2 p-0 icon-fav"
                  data-producto-id="${p.productoId}" data-favorito="${!!fav}" data-id-favorito="${fav || ""}">
            <i class="${icono}" style="font-size: 20px;"></i>
          </button>
          <div class="card-body">
            <p class="fw-semibold mb-1">${p.nombre}</p>
            <p class="fw-bold">$${p.precio.toFixed(2)}</p>
            <a href="../Usuarios/detalleproducto.html?productoId=${p.productoId}" class="btn btn-outline-dark w-100 mt-2">Ver más</a>
            <button class="btn btn-outline-dark w-100 mt-2 btn-agregar-carrito" 
                    data-producto-id="${p.productoId}" data-precio="${p.precio}">
              <i class="fa fa-shopping-cart me-2"></i>Agregar al carrito
            </button>
          </div>
        </div>
      `;
      container.appendChild(card);
    });

    document.querySelectorAll(".icon-fav").forEach(btn => {
      btn.addEventListener("click", async () => {
        const icon = btn.querySelector("i");
        const productoId = btn.dataset.productoId;
        const esFavorito = btn.dataset.favorito === "true";
        const idFavorito = btn.dataset.idFavorito;

        icon.classList.add("pop");
        setTimeout(() => icon.classList.remove("pop"), 200);

        if (esFavorito) {
          await fetch(`https://localhost:7291/Favoritos/Eliminarfavorito/${idFavorito}`, { method: "DELETE" });
          icon.className = "fa-regular fa-heart text-danger";
          btn.dataset.favorito = "false";
          btn.dataset.idFavorito = "";
        } else {
          const res = await fetch("https://localhost:7291/Favoritos/Agregar", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ usuarioId: parseInt(usuarioId), productoId: parseInt(productoId) })
          });
          const nuevo = await res.json();
          icon.className = "fa-solid fa-heart text-danger";
          btn.dataset.favorito = "true";
          btn.dataset.idFavorito = nuevo.idFavorito;
        }
      });
    });

    document.querySelectorAll(".btn-agregar-carrito").forEach(btn => {
      btn.addEventListener("click", async () => {
        const productoId = parseInt(btn.dataset.productoId);
        const precioUnitario = parseFloat(btn.dataset.precio);
        const usuarioId = parseInt(localStorage.getItem("usuarioId"));

        const res = await fetch(`https://localhost:7291/carrito/Usuario/${usuarioId}`);
        const carrito = await res.json();
        const yaExiste = carrito.some(item => item.productoId === productoId && item.estadoProductoId === 1);

        if (yaExiste) {
          alert("⚠️ El producto ya está en el carrito.");
          return;
        }

        const agregar = await fetch("https://localhost:7291/carrito/agregar", {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            usuarioId, productoId, cantidad: 1, estadoProductoId: 1, precioUnitario
          })
        });

        if (agregar.ok) {
          alert("✅ Producto agregado al carrito 🛒");
        } else {
          alert("❌ No se pudo agregar al carrito");
        }
      });
    });

  } catch (err) {
    console.error("Error:", err);
    container.innerHTML = `<p class="text-danger">No se pudieron cargar los productos :(</p>`;
  }
}

document.getElementById("btnAplicarFiltros")?.addEventListener("click", () => {
  const categoriaId = document.getElementById("filtroCategoria")?.value;
  const marcaId = document.getElementById("filtroMarca")?.value;

  cargarProductosConFavoritos({ categoriaId, marcaId });
});