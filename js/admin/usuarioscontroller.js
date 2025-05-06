const API = "https://localhost:7291/usuarios";
const tablaBody = document.getElementById("usuariosBody");
const form = document.getElementById("formUsuario");
const modalElement = document.getElementById("modalUsuario");
const modal = new bootstrap.Modal(modalElement);

// 🔐 Verificación de sesión (para administrador)
document.addEventListener("DOMContentLoaded", async () => {
    const adminId = localStorage.getItem("adminId");
    const adminNombre = localStorage.getItem("adminNombre");
  
    if (!adminId) {
      alert("Acceso denegado. Debes iniciar sesión como administrador.");
      window.location.href = "loginadmin.html";
      return;
    }
  
    await cargarUsuarios();
  });

async function cargarUsuarios() {
  const res = await fetch(`${API}/ObtenerTodosUsuarios`);
  const usuarios = await res.json();

  tablaBody.innerHTML = "";
  usuarios.forEach(u => {
    tablaBody.innerHTML += `
      <tr>
        <td>${u.usuarioId}</td>
        <td>${u.nombre}</td>
        <td>${u.correo}</td>
        <td>${u.direccion || '-'}</td>
        <td>${u.contacto || '-'}</td>
        <td>
          <button class="btn btn-sm btn-warning" onclick="editarUsuario(${u.usuarioId})">Editar</button>
          <button class="btn btn-sm btn-danger" onclick="eliminarUsuario(${u.usuarioId})">Eliminar</button>
        </td>
      </tr>
    `;
  });
}

function abrirFormulario() {
  form.reset();
  document.getElementById("usuarioId").value = "";
  document.getElementById("modalTitulo").textContent = "Nuevo Usuario";
  modal.show();
}

form.addEventListener("submit", async (e) => {
    e.preventDefault();
  
    const password = document.getElementById("contraseña").value;
    const confirmPassword = document.getElementById("confirmarContraseña").value;
  
    if (password && password !== confirmPassword) {
      alert("⚠️ Las contraseñas no coinciden.");
      return;
    }
  
    const usuario = {
      usuarioId: parseInt(document.getElementById("usuarioId").value) || 0,
      nombre: document.getElementById("nombre").value,
      correo: document.getElementById("correo").value,
      direccion: document.getElementById("direccion").value,
      contacto: document.getElementById("contacto").value,
      contraseña: password || "", // solo enviamos si escribió algo
      metodoLogin: "Formulario"
    };
  
    const esEdicion = usuario.usuarioId > 0;
    const url = esEdicion ? `${API}/ActualizarUsuario` : `${API}/CrearUsuario`;
    const metodo = esEdicion ? "PUT" : "POST";
  
    const res = await fetch(url, {
      method: metodo,
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(usuario)
    });
  
    if (res.ok) {
      alert(esEdicion ? "Usuario actualizado ✅" : "Usuario creado ✅");
      modal.hide();
      cargarUsuarios();
    } else {
      alert("❌ Error al guardar usuario");
    }
  });
  

  async function editarUsuario(id) {
    const res = await fetch(`${API}/ObtenerUsuarioPorId?id=${id}`);
    const u = await res.json();
  
    document.getElementById("usuarioId").value = u.usuarioId;
    document.getElementById("nombre").value = u.nombre;
    document.getElementById("correo").value = u.correo;
    document.getElementById("direccion").value = u.direccion || "";
    document.getElementById("contacto").value = u.contacto || "";
  
    // 🔁 Limpiar campos de contraseña al editar
    document.getElementById("contraseña").value = "";
    document.getElementById("confirmarContraseña").value = "";
  
    document.getElementById("modalTitulo").textContent = "Editar Usuario";
    modal.show();
  }
  

async function eliminarUsuario(id) {
  if (!confirm("¿Estás seguro de eliminar este usuario?")) return;

  const res = await fetch(`${API}/EliminarUsuario?id=${id}`, { method: "DELETE" });
  if (res.ok) {
    alert("Usuario eliminado ✅");
    cargarUsuarios();
  } else {
    alert("❌ No se pudo eliminar el usuario");
  }
}
