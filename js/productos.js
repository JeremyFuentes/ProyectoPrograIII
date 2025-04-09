document.addEventListener("DOMContentLoaded", () => {
    const productosContainer = document.getElementById("productosContainer");
  
    fetch("https://localhost:7291/Productos/conImagenPrincipal")
      .then(res => {
        if (!res.ok) throw new Error("Error al obtener productos");
        return res.json();
      })
      .then(productos => {
        productosContainer.innerHTML = ""; // Limpia por si acaso
  
        productos.forEach(p => {
          productosContainer.innerHTML += `
            <div class="col-md-4">
              <div class="card border-0 h-100">
                <img src="https://localhost:7291${p.imagen || '/imagenes/placeholder.png'}" 
                     class="card-img-top" 
                     alt="${p.nombre}" 
                     style="max-height: 200px; width: auto; margin: 0 auto; object-fit: contain;">
                <div class="card-body">
                  <p class="fw-semibold mb-1">${p.nombre}</p>
                  <div class="d-flex align-items-center mb-1">
                    <span class="text-warning me-1">
                      <i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i>
                      <i class="fa-regular fa-star"></i><i class="fa-regular fa-star"></i>
                    </span>
                    <span class="text-muted small ms-1">3.5/5</span>
                  </div>
                  <p class="fw-bold">$${p.precio.toFixed(2)}</p>
                </div>
              </div>
            </div>
          `;
        });
      })
      .catch(err => {
        productosContainer.innerHTML = `<p class="text-danger">Hubo un problema al cargar los productos :(</p>`;
        console.error(err);
      });
  });
  