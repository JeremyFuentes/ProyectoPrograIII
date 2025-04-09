const API_BASE = "https://localhost:7291";
const tabla = document.querySelector("#tablaProductos tbody");

// Cargar productos después de verificar sesión
window.addEventListener("DOMContentLoaded", () => {
  const adminId = localStorage.getItem("adminId");

  // Verificar si hay sesión activa
  if (!adminId) {
    window.location.href = "loginadmin.html";
    return;
  }

  cargarProductos();
});

async function cargarProductos() {
  tabla.innerHTML = ""; // limpiar tabla
  try {
    const res = await fetch(`${API_BASE}/productos/ObtenerTodos`);
    const productos = await res.json();

    for (const producto of productos) {
      const imagen = await obtenerImagenPrincipal(producto.productoId);

      const fila = document.createElement("tr");
      fila.innerHTML = `
        <td>${producto.productoId}</td>
        <td>${producto.nombre}</td>
        <td>$${producto.precio}</td>
        <td>${producto.stock}</td>
        <td>${producto.categoriaId}</td>
        <td>${producto.marcaId}</td>
        <td>${producto.proveedorId}</td>
        <td>${producto.sku}</td>
        <td>${producto.descripcion}</td>
        <td>${producto.estado ? "Activo" : "Inactivo"}</td>
        <td>
          ${imagen ? `<img src="${API_BASE}${imagen}" width="50">` : "Sin imagen"}
        </td>
        <td>
          <button class="btn btn-warning btn-sm me-1" onclick="editarProducto(${producto.productoId})">Editar</button>
          <button class="btn btn-danger btn-sm" onclick="eliminarProducto(${producto.productoId})">Eliminar</button>
        </td>
      `;
      tabla.appendChild(fila);
    }
  } catch (err) {
    console.error("Error cargando productos:", err);
    alert("No se pudieron cargar los productos.");
  }
}

async function obtenerImagenPrincipal(productoId) {
  try {
    const res = await fetch(`${API_BASE}/imagenesProducto/producto/${productoId}`);
    const imagenes = await res.json();
    const principal = imagenes.find(img => img.esPrincipal);
    return principal?.urlImagen || imagenes[0]?.urlImagen || null;
  } catch (e) {
    return null;
  }
}

function editarProducto(id) {
  window.location.href = `agregarproductos.html?id=${id}`;
}

async function eliminarProducto(id) {
  if (!confirm("¿Deseas eliminar este producto?")) return;
  try {
    const res = await fetch(`${API_BASE}/productos/EliminarporId/${id}`, {
      method: "DELETE"
    });

    if (res.ok) {
      alert("Producto eliminado ✅");
      cargarProductos();
    } else {
      alert("Error al eliminar el producto ❌");
    }
  } catch (error) {
    alert("Error inesperado");
  }
}

// Logout
document.getElementById("btnLogout").addEventListener("click", () => {
  localStorage.removeItem("adminId"); // Limpiar sesión
  window.location.href = "loginadmin.html"; // Redirigir al login
});
