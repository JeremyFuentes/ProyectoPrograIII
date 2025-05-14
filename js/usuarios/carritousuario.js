const API_BASE = "https://localhost:7291";
const usuarioId = localStorage.getItem("usuarioId");
const nombreUsuario = localStorage.getItem("nombreUsuario");

if (!usuarioId || !nombreUsuario) {
  alert("Acceso denegado. Por favor inicia sesión.");
  window.location.href = "../login.html";
}

const CODIGOS_DESCUENTO = {
  "EASY10": 10,
  "EASY15": 15,
  "EASY20": 20
};

let porcentajeDescuento = 0;
let carrito = [];

document.addEventListener("DOMContentLoaded", async () => {
  document.getElementById("nombreUsuario").textContent = nombreUsuario;

  await cargarCarrito();
  renderizarCarrito();
  calcularResumen();

  document.getElementById("btnAplicarCodigo").addEventListener("click", () => {
    const input = document.getElementById("codigoDescuento").value.trim().toUpperCase();
    if (CODIGOS_DESCUENTO.hasOwnProperty(input)) {
      porcentajeDescuento = CODIGOS_DESCUENTO[input];
      alert(`✅ Código válido aplicado: ${porcentajeDescuento}% de descuento.`);
    } else {
      porcentajeDescuento = 0;
      alert("❌ Código de descuento inválido.");
    }
    calcularResumen();
  });

  document.getElementById("btnComprar").addEventListener("click", async () => {
    if (carrito.length === 0) {
      alert("Tu carrito está vacío.");
      return;
    }

    const validacionRes = await fetch(`${API_BASE}/usuarios/ValidarDireccionContacto/${usuarioId}`);
    if (!validacionRes.ok) {
      const data = await validacionRes.json();
      const lista = document.getElementById("mensajeCamposFaltantes");
      lista.innerHTML = "";
      (data.camposFaltantes || []).forEach(campo => {
        const li = document.createElement("li");
        li.textContent = campo;
        lista.appendChild(li);
      });
      new bootstrap.Modal(document.getElementById("modalValidacionContacto")).show();
      return;
    }

    const userData = await (await fetch(`${API_BASE}/usuarios/ObtenerUsuarioPorId?id=${usuarioId}`)).json();
    document.getElementById("direccionUsuario").textContent = userData.direccion || "No especificada";

    new bootstrap.Modal(document.getElementById("modalConfirmacionCompra")).show();
  });

  document.getElementById("btnConfirmarCompra").addEventListener("click", async () => {
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

    const res = await fetch(`${API_BASE}/carrito/ConfirmarCompra/${usuarioId}`, { method: "PUT" });
    if (res.ok) {
      alert("✅ Compra confirmada. ¡Gracias por tu pedido!");
      window.location.reload();
    } else {
      alert("❌ Hubo un problema al confirmar la compra.");
    }
  });

  document.querySelector(".search-input")?.addEventListener("keypress", function (e) {
    if (e.key === "Enter") {
      const termino = this.value.trim();
      if (termino) {
        window.location.href = `/html/Usuarios/productosusuario.html?busqueda=${encodeURIComponent(termino)}`;
      }
    }
  });

  document.querySelectorAll("input[name='metodoPago']").forEach(radio => {
    radio.addEventListener("change", () => {
      const tarjetaForm = document.getElementById("formularioTarjeta");
      tarjetaForm.classList.toggle("d-none", radio.value !== "tarjeta");
    });
  });
});

async function cargarCarrito() {
  const res = await fetch(`${API_BASE}/carrito/usuario/${usuarioId}`);
  carrito = await res.json();
}

function renderizarCarrito() {
  const contenedor = document.getElementById("carritoContainer");
  contenedor.innerHTML = "";

  carrito.forEach(item => {
    const producto = item.producto;
    const imagen = producto.imagenesProducto?.find(img => img.esPrincipal)?.urlImagen || "/imagenes/placeholder.png";

    const card = document.createElement("div");
    card.className = "cart-item d-flex justify-content-between align-items-center";
    card.innerHTML = `
      <div class="d-flex align-items-start gap-3">
        <a href="../Usuarios/detalleproducto.html?productoId=${producto.productoId}" class="text-decoration-none text-dark">
          <img src="https://localhost:7291${imagen}" style="width:80px; height:80px; object-fit:contain; border-radius:10px;" alt="${producto.nombre}" />
        </a>
        <div>
          <a href="../Usuarios/detalleproducto.html?productoId=${producto.productoId}" class="text-decoration-none text-dark">
            <p class="fw-semibold mb-1">${producto.nombre}</p>
            <p class="fw-bold mt-2">$${producto.precio.toFixed(2)}</p>
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
      </div>`;
    contenedor.appendChild(card);
  });
}

function calcularResumen() {
  const subtotal = carrito.reduce((acc, item) => acc + item.producto.precio * item.cantidad, 0);
  const descuento = subtotal * (porcentajeDescuento / 100);
  const envio = subtotal > 0 ? 15 : 0;
  const total = subtotal - descuento + envio;

  document.getElementById("subtotal").textContent = `$${subtotal.toFixed(2)}`;
  document.getElementById("envio").textContent = `$${envio.toFixed(2)}`;
  document.getElementById("total").textContent = `$${total.toFixed(2)}`;
  document.getElementById("descuento").textContent =
    porcentajeDescuento > 0 ? `- $${descuento.toFixed(2)} (${porcentajeDescuento}%)` : "$0";
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
  const confirmar = confirm("¿Seguro que deseas eliminar este producto del carrito?");
  if (!confirmar) return;

  const res = await fetch(`${API_BASE}/carrito/eliminar/${carritoId}`, { method: "DELETE" });
  if (res.ok) {
    carrito = carrito.filter(c => c.carritoId !== carritoId);
    renderizarCarrito();
    calcularResumen();
  }
};
