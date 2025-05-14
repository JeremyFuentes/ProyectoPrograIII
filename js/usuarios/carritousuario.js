const API_BASE = "https://localhost:7291";
const usuarioId = localStorage.getItem("usuarioId");
const nombreUsuario = localStorage.getItem("nombreUsuario");

if (!usuarioId || !nombreUsuario) {
  alert("Acceso denegado. Por favor inicia sesión.");
  window.location.href = "../login.html";
}

document.addEventListener("DOMContentLoaded", async () => {
  document.getElementById("nombreUsuario").textContent = nombreUsuario;

  const contenedorCarrito = document.getElementById("carritoContainer");
  const resumenSubtotal = document.getElementById("subtotal");
  const resumenTotal = document.getElementById("total");

  let carrito = [];

  await cargarCarrito();
  renderizarCarrito();
  calcularResumen();

  async function cargarCarrito() {
    const res = await fetch(`${API_BASE}/carrito/usuario/${usuarioId}`);
    carrito = await res.json();
  }

  function renderizarCarrito() {
    contenedorCarrito.innerHTML = "";

    carrito.forEach(item => {
      const producto = item.producto;
      const precio = producto.precio.toFixed(2);

      const imagenRuta = producto.imagenesProducto?.find(img => img.esPrincipal)?.urlImagen;
      const imagenUrl = imagenRuta
        ? `https://localhost:7291${imagenRuta}`
        : "https://localhost:7291/imagenes/placeholder.png";

      const contenedor = document.createElement("div");
      contenedor.className = "cart-item d-flex justify-content-between align-items-center";

      contenedor.innerHTML = `
        <div class="d-flex align-items-start gap-3">
          <a href="../Usuarios/detalleproducto.html?productoId=${producto.productoId}" class="text-decoration-none text-dark">
            <img src="${imagenUrl}" style="width:80px; height:80px; object-fit:contain; border-radius:10px;" alt="${producto.nombre}" />
          </a>
          <div>
            <a href="../Usuarios/detalleproducto.html?productoId=${producto.productoId}" class="text-decoration-none text-dark">
              <p class="fw-semibold mb-1">${producto.nombre}</p>
              <p class="fw-bold mt-2">$${precio}</p>
            </a>
          </div>
        </div>
        <div class="text-end">
          <button class="btn text-danger" onclick="eliminarProducto(${item.carritoId})">
            <i class="fa fa-trash"></i>
          </button>
          <div class="qty-control mt-2">
            <button class="qty-btn" onclick="cambiarCantidad(${item.carritoId}, -1)">-</button>
            <span id="cantidad-${item.carritoId}">${item.cantidad}</span>
            <button class="qty-btn" onclick="cambiarCantidad(${item.carritoId}, 1, ${producto.stock})">+</button>
          </div>
        </div>
      `;

      contenedorCarrito.appendChild(contenedor);
    });
  }

  window.cambiarCantidad = async function (carritoId, cambio, stock = Infinity) {
    const item = carrito.find(c => c.carritoId === carritoId);
    if (!item) return;

    let nuevaCantidad = item.cantidad + cambio;
    if (nuevaCantidad < 1 || nuevaCantidad > stock) return;

    const res = await fetch(`${API_BASE}/carrito/ActualizarCantidad?carritoId=${carritoId}&cantidad=${nuevaCantidad}`, {
      method: "PUT"
    });

    if (res.ok) {
      item.cantidad = nuevaCantidad;
      document.getElementById(`cantidad-${carritoId}`).textContent = nuevaCantidad;
      calcularResumen();
    }
  };

  window.eliminarProducto = async function (carritoId) {
    if (!confirm("¿Seguro que deseas eliminar este producto del carrito?")) return;

    const res = await fetch(`${API_BASE}/carrito/eliminar/${carritoId}`, {
      method: "DELETE"
    });

    if (res.ok) {
      carrito = carrito.filter(c => c.carritoId !== carritoId);
      renderizarCarrito();
      calcularResumen();
    }
  };

  function calcularResumen() {
    let subtotal = carrito.reduce((acc, item) => acc + item.producto.precio * item.cantidad, 0);
    let descuento = subtotal * 0.2;
    let envio = subtotal > 0 ? 15 : 0;
    let total = subtotal - descuento + envio;

    document.getElementById("subtotal").textContent = `$${subtotal.toFixed(2)}`;
    document.getElementById("descuento").textContent = `- $${descuento.toFixed(2)}`;
    document.getElementById("envio").textContent = `$${envio.toFixed(2)}`;
    document.getElementById("total").textContent = `$${total.toFixed(2)}`;
  }

  // Finalizar compra
  const btnFinalizar = document.getElementById("btnComprar");
  if (btnFinalizar) {
    btnFinalizar.addEventListener("click", async () => {
      if (carrito.length === 0) {
        alert("Tu carrito está vacío.");
        return;
      }

      const validacionRes = await fetch(`${API_BASE}/usuarios/ValidarDireccionContacto/${usuarioId}`);
      if (!validacionRes.ok) {
        const data = await validacionRes.json();
        const lista = document.getElementById("mensajeCamposFaltantes");
        if (lista) {
          lista.innerHTML = "";
          (data.camposFaltantes || []).forEach(campo => {
            const li = document.createElement("li");
            li.textContent = campo;
            lista.appendChild(li);
          });
        }

        const modal = new bootstrap.Modal(document.getElementById("modalValidacionContacto"));
        modal.show();
        return;
      }

      const userRes = await fetch(`${API_BASE}/usuarios/ObtenerUsuarioPorId?id=${usuarioId}`);
      const userData = await userRes.json();
      const direccionTexto = userData.direccion || "No especificada";

      const dirElem = document.getElementById("direccionUsuario");
      if (dirElem) dirElem.textContent = direccionTexto;

      const modalConfirm = new bootstrap.Modal(document.getElementById("modalConfirmacionCompra"));
      modalConfirm.show();
    });
  }

  const metodoPagoRadios = document.querySelectorAll("input[name='metodoPago']");
  metodoPagoRadios.forEach(radio => {
    radio.addEventListener("change", () => {
      const tarjetaForm = document.getElementById("formularioTarjeta");
      tarjetaForm.classList.toggle("d-none", radio.value !== "tarjeta");
    });
  });

  const btnConfirmar = document.getElementById("btnConfirmarCompra");
  if (btnConfirmar) {
    btnConfirmar.addEventListener("click", async () => {
      const metodo = document.querySelector("input[name='metodoPago']:checked")?.value;
      if (metodo === "tarjeta") {
        const inputs = document.querySelectorAll("#formularioTarjeta input");
        for (const input of inputs) {
          if (!input.value.trim()) {
            alert("Completa todos los datos de la tarjeta.");
            return;
          }
        }
      }

      const res = await fetch(`${API_BASE}/carrito/ConfirmarCompra/${usuarioId}`, {
        method: "PUT"
      });

      if (res.ok) {
        alert("✅ Compra confirmada. ¡Gracias por tu pedido!");
        window.location.reload();
      } else {
        alert("❌ Hubo un problema al confirmar la compra.");
      }
    });
  }
});
