const API_BASE = "https://localhost:7291";

// DOM
const form = document.getElementById("formNuevoProducto");
const categoriaSelect = document.getElementById("categoriaId");
const marcaSelect = document.getElementById("marcaId");
const proveedorSelect = document.getElementById("proveedorId");
const imagenesInput = document.getElementById("imagenes");
const previewImagenes = document.getElementById("previewImagenes");
const submitButton = form.querySelector("button[type='submit']");
const cancelarBtn = document.getElementById("cancelarBtn");

// Loader
const loader = document.createElement("div");
loader.id = "loaderRedirect";
loader.style = `
  position: fixed; top: 0; left: 0; width: 100%; height: 100%;
  background-color: rgba(0,0,0,0.5); display: flex;
  justify-content: center; align-items: center;
  color: white; font-size: 2rem; z-index: 9999; display: none;
`;
loader.innerText = "Redireccionando...";
document.body.appendChild(loader);

// Verificar sesión de administrador
const adminId = localStorage.getItem("adminId");
if (!adminId) {
  alert("Acceso denegado. Inicie sesión como administrador.");
  window.location.href = "../Admin/loginadmin.html";
}

// Modo edición
const params = new URLSearchParams(window.location.search);
const productoId = params.get("id");
const modoEdicion = !!productoId;
let imagenesActuales = [];
let nuevasImagenesPrincipal = null;
let imagenesAEliminar = [];

// Cargar selects y datos si es edición
window.addEventListener("DOMContentLoaded", () => {
  cargarOpciones("auxiliares/categorias/todas", categoriaSelect, "categoriaId", "nombre");
  cargarOpciones("auxiliares/marcas/todas", marcaSelect, "marcaId", "nombre");
  cargarOpciones("auxiliares/proveedores/todos", proveedorSelect, "proveedorId", "nombre");

  if (modoEdicion) {
    imagenesInput.removeAttribute("required");

    // ✅ Cambiar texto del botón
    submitButton.innerText = "Guardar Cambios";

    // ✅ Cambiar el título del formulario
    const titulo = document.getElementById("tituloFormulario");
    if (titulo) titulo.textContent = "Actualizar Producto";

    // Cargar datos del producto
    fetch(`${API_BASE}/productos/ObtenerporId/${productoId}`)
      .then(res => res.json())
      .then(p => {
        form.nombre.value = p.nombre;
        form.precio.value = p.precio;
        form.stock.value = p.stock;
        form.categoriaId.value = p.categoriaId;
        form.marcaId.value = p.marcaId;
        form.proveedorId.value = p.proveedorId;
        form.descripcion.value = p.descripcion;
        form.estado.checked = p.estado;
        cargarImagenesActuales(productoId);
      });
  }
});

function cargarOpciones(endpoint, select, idKey, labelKey) {
  fetch(`${API_BASE}/${endpoint}`)
    .then(res => res.json())
    .then(data => {
      data.forEach(item => {
        const option = document.createElement("option");
        option.value = item[idKey];
        option.textContent = item[labelKey];
        select.appendChild(option);
      });
    });
}

function cargarImagenesActuales(id) {
  fetch(`${API_BASE}/imagenesProducto/producto/${id}`)
    .then(res => res.json())
    .then(data => {
      imagenesActuales = data;
      mostrarPreviewImagenes();
    });
}

function mostrarPreviewImagenes() {
  previewImagenes.innerHTML = "";
  imagenesActuales.forEach(img => {
    const wrapper = document.createElement("div");
    wrapper.classList.add("position-relative", "d-inline-block", "me-2", "mb-2");

    const thumb = document.createElement("img");
    thumb.src = API_BASE + img.urlImagen;
    thumb.style.width = "100px";
    thumb.style.height = "100px";
    thumb.style.objectFit = "contain";
    thumb.classList.add("rounded", "border");

    const btnEliminar = document.createElement("button");
    btnEliminar.classList.add("btn", "btn-danger", "btn-sm", "position-absolute");
    btnEliminar.style.top = "0";
    btnEliminar.style.right = "0";
    btnEliminar.innerHTML = "&times;";
    btnEliminar.onclick = () => {
      imagenesAEliminar.push(img.idImagen);
      imagenesActuales = imagenesActuales.filter(i => i.idImagen !== img.idImagen);
      mostrarPreviewImagenes();
    };

    const btnPrincipal = document.createElement("button");
    btnPrincipal.classList.add("btn", "btn-warning", "btn-sm", "position-absolute");
    btnPrincipal.style.bottom = "0";
    btnPrincipal.style.right = "0";
    btnPrincipal.innerText = "⭐";
    btnPrincipal.title = "Marcar como principal";
    btnPrincipal.onclick = () => {
      nuevasImagenesPrincipal = img.idImagen;
      imagenesActuales.forEach(i => i.esPrincipal = false);
      img.esPrincipal = true;
      mostrarPreviewImagenes();
    };

    if (img.esPrincipal) {
      thumb.classList.add("border-primary", "border-3");
    }

    wrapper.appendChild(thumb);
    wrapper.appendChild(btnEliminar);
    wrapper.appendChild(btnPrincipal);
    previewImagenes.appendChild(wrapper);
  });
}

form.addEventListener("submit", async (e) => {
  e.preventDefault();
  const formData = new FormData(form);
  const imagenes = imagenesInput.files;
  const estado = form.estado.checked;

  const producto = {
    nombre: form.nombre.value,
    precio: form.precio.value,
    stock: form.stock.value,
    categoriaId: form.categoriaId.value,
    marcaId: form.marcaId.value,
    proveedorId: form.proveedorId.value,
    descripcion: form.descripcion.value,
    estado: estado
  };

  if (modoEdicion) {
    producto.productoId = productoId;

    const totalImagenesMostradas = previewImagenes.querySelectorAll("img").length;
    if (totalImagenesMostradas === 0 && imagenes.length === 0) {
      alert("Debes dejar al menos una imagen del producto o subir una nueva.");
      return;
    }

    const res = await fetch(`${API_BASE}/productos/ActualizarProducto`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(producto)
    });

    if (res.ok) {
      if (imagenes.length > 0) {
        const imgForm = new FormData();
        for (const img of imagenes) imgForm.append("imagenes", img);
        imgForm.append("productoId", productoId);

        await fetch(`${API_BASE}/imagenesProducto/subirVarias`, {
          method: "POST",
          body: imgForm
        });
      }

      if (nuevasImagenesPrincipal) {
        await fetch(`${API_BASE}/imagenesProducto/establecerPrincipal/${nuevasImagenesPrincipal}`, {
          method: "PUT"
        });
      }

      for (const idImg of imagenesAEliminar) {
        await fetch(`${API_BASE}/imagenesProducto/eliminarImagen/${idImg}`, {
          method: "DELETE"
        });
      }

      loader.style.display = "flex";
      setTimeout(() => window.location.href = "../Admin/indexadmin.html", 1500);
    } else {
      alert("Error al actualizar producto ❌");
    }
  } else {
    for (const key in producto) {
      formData.append(key, producto[key]);
    }

    for (const img of imagenes) {
      formData.append("imagenes", img);
    }

    const res = await fetch(`${API_BASE}/productos/RegistrarConImagenes`, {
      method: "POST",
      body: formData
    });

    if (res.ok) {
      loader.style.display = "flex";
      setTimeout(() => window.location.href = "../Admin/indexadmin.html", 1500);
    } else {
      alert("Error al registrar producto ❌");
    }
  }
});

// Botón cancelar
cancelarBtn.addEventListener("click", () => {
  if (modoEdicion) {
    if (confirm("¿Seguro que quieres salir sin guardar los cambios?")) {
      window.location.href = "../Admin/indexadmin.html";
    }
  } else {
    window.location.href = "../Admin/indexadmin.html";
  }
});
