const API = "https://localhost:7291/usuarios";
const API_CARRITO = "https://localhost:7291/carrito";
const usuarioId = localStorage.getItem("usuarioId");
const nombreUsuario = localStorage.getItem("nombreUsuario");

document.addEventListener("DOMContentLoaded", async () => {
  if (!usuarioId || !nombreUsuario) {
    window.location.href = "../login.html";
    return;
  }

  document.getElementById("nombreUsuario").textContent = nombreUsuario;

  const usuario = await obtenerUsuario();

  mostrarCampo("correo", usuario.correo, usuario.metodoLogin === "Google", usuario.googleId === null);
  mostrarCampo("nombre", usuario.nombre);
  mostrarCampo("direccion", usuario.direccion ?? "Sin agregar", !usuario.direccion);
  mostrarCampo("contacto", usuario.contacto ?? "Sin agregar", !usuario.contacto);

  document.getElementById("btnMostrarCambioPass").addEventListener("click", () => {
    const seccion = document.getElementById("seccionCambioPass");
    seccion.style.display = seccion.style.display === "none" ? "block" : "none";
    if (seccion.style.display === "none") limpiarCamposPassword();
  });

  document.getElementById("formAjustes").addEventListener("submit", async (e) => {
    e.preventDefault();
    const usuarioActual = await obtenerUsuario();

    const direccionInput = document.getElementById("direccion").value.trim();
    const contactoInput = document.getElementById("contacto").value.trim();

    const nuevoUsuario = {
      usuarioId: usuarioActual.usuarioId,
      nombre: document.getElementById("nombre").value.trim(),
      direccion: direccionInput === "Sin agregar" ? null : direccionInput,
      contacto: contactoInput === "Sin agregar" ? null : contactoInput,
      metodoLogin: usuarioActual.metodoLogin,
      googleId: usuarioActual.googleId,
      correo: usuarioActual.correo,
      contraseña: null
    };

    if (usuarioActual.metodoLogin !== "Google") {
      nuevoUsuario.correo = document.getElementById("correo").value.trim();
    }

    const passActual = document.getElementById("passActual").value.trim();
    const nuevaPass = document.getElementById("nuevaPass").value.trim();
    const confirmarPass = document.getElementById("confirmarPass").value.trim();
    const quiereCambiarPass = nuevaPass || confirmarPass || passActual;

    if (quiereCambiarPass) {
      if (!nuevaPass || !confirmarPass) return alert("Completa todos los campos para cambiar la contraseña.");
      if (nuevaPass !== confirmarPass) return alert("La nueva contraseña no coincide con la confirmación.");
      if (usuarioActual.contraseña && !passActual) return alert("Debes ingresar tu contraseña actual.");

      const validacion = await fetch(`${API}/AutenticarUsuario`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ correo: usuarioActual.correo, contraseña: passActual })
      });

      if (!validacion.ok) return alert("❌ La contraseña actual es incorrecta.");
      nuevoUsuario.contraseña = nuevaPass;
    }

    const res = await fetch(`${API}/ActualizarUsuario`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(nuevoUsuario)
    });

    if (res.ok) {
      alert("✅ Cambios guardados correctamente.");
      limpiarCamposPassword();
      location.reload();
    } else {
      const mensaje = await res.text();
      alert("❌ " + mensaje);
    }
  });
});

async function obtenerUsuario() {
  const res = await fetch(`${API}/ObtenerUsuarioPorId?id=${usuarioId}`);
  return await res.json();
}

function mostrarCampo(id, valor, esBloqueado = false, puedeVincular = false) {
  const contenedor = document.getElementById(`grupo-${id}`);
  contenedor.innerHTML = `
    <label class="form-label">${id.charAt(0).toUpperCase() + id.slice(1)}:</label>
    <div class="d-flex align-items-center">
      <input id="${id}" type="text" autocomplete="off" class="form-control ${valor === "Sin agregar" ? "text-danger fw-bold" : ""}" 
        value="${valor}" ${id === "correo" && esBloqueado ? "readonly" : ""} />
      ${!(id === "correo" && esBloqueado) ? `<button type="button" class="btn btn-sm ms-2" onclick="habilitarEdicion('${id}')"><i class="fa fa-pencil"></i></button>` : ""}
      ${id === "correo" && puedeVincular ? `
        <button class="btn btn-outline-danger btn-sm ms-2" onclick="vincularGoogle()">
          Vincular Google <i class="fab fa-google"></i>
        </button>` : ""}
    </div>
  `;
}


function habilitarEdicion(id) {
  const campo = document.getElementById(id);
  campo.removeAttribute("readonly");
  campo.focus();
}

function limpiarCamposPassword() {
  document.getElementById("passActual").value = "";
  document.getElementById("nuevaPass").value = "";
  document.getElementById("confirmarPass").value = "";
}

document.getElementById("confirmarEliminarCuenta").addEventListener("click", async () => {
  if (!confirm("¿Realmente deseas eliminar tu cuenta? Esta acción es irreversible.")) return;

  try {
    const res = await fetch(`${API}/EliminarUsuario?id=${usuarioId}`, { method: "DELETE" });
    if (res.ok) {
      alert("✅ Tu cuenta ha sido eliminada correctamente.");
      localStorage.clear();
      window.location.href = "../index.html";
    } else {
      alert("❌ No se pudo eliminar la cuenta.");
    }
  } catch (error) {
    console.error("Error al eliminar cuenta:", error);
    alert("❌ Hubo un problema al eliminar la cuenta.");
  }
});

// 🔹 Logout
document.querySelectorAll(".logout-link").forEach(btn => {
  btn.addEventListener("click", (e) => {
    e.preventDefault();
    localStorage.removeItem("usuarioId");
    localStorage.removeItem("nombreUsuario");
    localStorage.removeItem("token");
    window.location.href = "../index.html";
  });
});

// 🔄 Control de navegación lateral
function activarBoton(idActivo) {
  document.querySelectorAll(".list-group-item").forEach(btn => {
    btn.classList.remove("active");
  });
  document.getElementById(idActivo).classList.add("active");
}

const contenedor = document.getElementById("contenidoDinamico");
const seccionCuenta = document.getElementById("seccionCuenta");

document.getElementById("btnCuenta").addEventListener("click", () => {
  activarBoton("btnCuenta");
  seccionCuenta.style.display = "block";
  contenedor.innerHTML = "";
});

document.getElementById("btnPedidos").addEventListener("click", async () => {
  activarBoton("btnPedidos");
  seccionCuenta.style.display = "none";
  const pedidos = await fetch(`${API_CARRITO}/activos/${usuarioId}`).then(r => r.json());

  contenedor.innerHTML = `<h4 class="mb-4">Pedidos activos</h4>`;
  if (pedidos.length === 0) {
    contenedor.innerHTML += `<p>No hay pedidos activos.</p>`;
    return;
  }

  pedidos.forEach(item => {
    const img = item.producto.imagenesProducto?.find(i => i.esPrincipal)?.urlImagen || "/imagenes/placeholder.png";
    const url = `../Usuarios/detalleproducto.html?productoId=${item.producto.productoId}`;
    contenedor.innerHTML += `
      <div onclick="window.location.href='${url}'" style="cursor:pointer;" class="border p-3 rounded mb-3 d-flex align-items-center gap-3">
        <img src="https://localhost:7291${img}" width="80" height="80" style="object-fit:contain;" />
        <div>
          <h6 class="mb-1">${item.producto.nombre}</h6>
          <p class="mb-0">Cantidad: ${item.cantidad}</p>
          <p class="mb-0">Estado: <strong>${item.estadoProducto.nombre}</strong></p>
        </div>
      </div>
    `;
  });
});

document.getElementById("btnHistorial").addEventListener("click", async () => {
  activarBoton("btnHistorial");
  seccionCuenta.style.display = "none";
  const entregados = await fetch(`${API_CARRITO}/entregados/${usuarioId}`).then(r => r.json());

  contenedor.innerHTML = `<h4 class="mb-4">Historial de pedidos</h4>`;
  if (entregados.length === 0) {
    contenedor.innerHTML += `<p>No tienes pedidos entregados aún.</p>`;
    return;
  }

  entregados.forEach(item => {
    const img = item.producto.imagenesProducto?.find(i => i.esPrincipal)?.urlImagen || "/imagenes/placeholder.png";
    const url = `../Usuarios/detalleproducto.html?productoId=${item.producto.productoId}`;
    contenedor.innerHTML += `
      <div onclick="window.location.href='${url}'" style="cursor:pointer;" class="border p-3 rounded mb-3 d-flex align-items-center gap-3">
        <img src="https://localhost:7291${img}" width="80" height="80" style="object-fit:contain;" />
        <div>
          <h6 class="mb-1">${item.producto.nombre}</h6>
          <p class="mb-0">Cantidad: ${item.cantidad}</p>
          <p class="mb-0">Estado: <strong>${item.estadoProducto.nombre}</strong></p>
          <p class="mb-0"><small>Fecha: ${new Date(item.fechaCompra).toLocaleDateString()}</small></p>
        </div>
      </div>
    `;
  });
});

async function vincularGoogle() {
  if (typeof google === "undefined") {
    alert("Google API no cargada.");
    return;
  }

  const usuarioActual = await obtenerUsuario();

  google.accounts.id.initialize({
    client_id: "361988047765-qge0f8dh6qff8p7sc1olfp8u1ogsm84c.apps.googleusercontent.com",
    callback: async (response) => {
      try {
        const res = await fetch(`${API}/VincularGoogle`, {
          method: "PUT",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            idToken: response.credential,
            usuarioId: usuarioActual.usuarioId
          })
        });

        const data = await res.json();
        if (res.ok) {
          alert("✅ Cuenta vinculada con Google");
          location.reload();
        } else {
          alert("❌ Error al vincular cuenta: " + data.mensaje);
        }
      } catch (err) {
        alert("❌ Error al vincular cuenta");
        console.error(err);
      }
    }
  });

  google.accounts.id.prompt(); // muestra el popup de login Google
}

