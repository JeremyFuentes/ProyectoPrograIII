const API_BASE = "https://localhost:7291";
const contenedor = document.getElementById("contenedorPedidos");
const estados = ["Pendiente de pago", "En transacción", "Enviado", "En reparto", "Entregado"];

window.addEventListener("DOMContentLoaded", () => {
  const adminId = localStorage.getItem("adminId");
  if (!adminId) return location.href = "loginadmin.html";
  cargarPedidos();
});

async function cargarPedidos() {
  contenedor.innerHTML = "";

  try {
    const res = await fetch(`${API_BASE}/carrito/PedidosAgrupados`);
    const usuarios = await res.json();

    usuarios.forEach((usuario, i) => {
      const card = document.createElement("div");
      card.classList.add("accordion-item");
      card.innerHTML = `
        <h2 class="accordion-header">
          <button class="accordion-button ${i > 0 ? 'collapsed' : ''}" type="button" data-bs-toggle="collapse" data-bs-target="#collapse${i}">
            ${usuario.nombre} (${usuario.correo})
          </button>
        </h2>
        <div id="collapse${i}" class="accordion-collapse collapse ${i === 0 ? 'show' : ''}">
          <div class="accordion-body">
            ${usuario.pedidos.map(pedido => `
              <div class="d-flex justify-content-between align-items-center border-bottom py-2 ${pedido.estadoProductoId === 5 ? 'pedido-entregado' : ''}">
                <div>
                  <strong>Producto:</strong> ${pedido.nombreProducto}<br>
                  <strong>Estado:</strong> <span id="estado-${pedido.carritoId}">${estados[pedido.estadoProductoId - 1]}</span>
                </div>
                <button class="btn btn-primary btn-sm" onclick="cambiarEstado(${pedido.carritoId}, ${pedido.estadoProductoId})" ${pedido.estadoProductoId >= 5 ? 'disabled' : ''}>
                  Cambiar Estado
                </button>
              </div>
            `).join('')}
          </div>
        </div>
      `;
      contenedor.appendChild(card);
    });
  } catch (err) {
    alert("Error cargando pedidos.");
    console.error(err);
  }
}

async function cambiarEstado(carritoId, estadoActual) {
  const nuevoEstado = estadoActual + 1;
  if (nuevoEstado > 5) return;

  try {
    const res = await fetch(`${API_BASE}/carrito/ActualizarEstado/${carritoId}/${nuevoEstado}`, {
      method: "PUT"
    });

    if (res.ok) {
      document.getElementById(`estado-${carritoId}`).innerText = estados[nuevoEstado - 1];
      if (nuevoEstado === 5) {
        event.target.disabled = true;
      }
    } else {
      alert("No se pudo actualizar el estado.");
    }
  } catch (err) {
    console.error(err);
    alert("Error inesperado.");
  }
}

// Logout
document.getElementById("btnLogout").addEventListener("click", () => {
  localStorage.removeItem("adminId");
  window.location.href = "loginadmin.html";
});
