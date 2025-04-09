document.addEventListener("DOMContentLoaded", () => {
    const loginForm = document.getElementById("loginForm");
    const registerForm = document.getElementById("registerForm");
    const loginCard = document.getElementById("loginCard");
    const registerCard = document.getElementById("registerCard");
    const showRegister = document.getElementById("showRegister");
    const showLogin = document.getElementById("showLogin");
    const errorLogin = document.getElementById("errorLogin");
    const errorRegister = document.getElementById("errorRegister");
    const errorPasswordMatch = document.getElementById("errorPasswordMatch");

    // Alternar entre login y registro
    showRegister.addEventListener("click", () => {
        loginCard.classList.add("d-none");
        registerCard.classList.remove("d-none");
    });

    showLogin.addEventListener("click", () => {
        registerCard.classList.add("d-none");
        loginCard.classList.remove("d-none");
    });

  // 🔹 Manejar inicio de sesión
  loginForm.addEventListener("submit", async (event) => {
    event.preventDefault();
  
    const correo = document.getElementById("correoLogin").value;
    const contraseña = document.getElementById("passwordLogin").value;
  
    try {
      const response = await fetch("https://localhost:7291/usuarios/AutenticarUsuario", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ Correo: correo, Contraseña: contraseña })
      });
  
      if (!response.ok) {
        const errorMessage = await response.text();
        throw new Error(errorMessage || "Credenciales incorrectas");
      }
  
      const data = await response.json();

// Guardar ID y nombre directamente
localStorage.setItem("usuarioId", data.idUsuario);
localStorage.setItem("nombreUsuario", data.nombre);
  
      // Redirigir
      window.location.href = "usuarios/indexusuario.html";
  
    } catch (error) {
      errorLogin.textContent = "Credenciales incorrectas";
      errorLogin.classList.remove("d-none");
    }
  });
  


    // 🔹 Manejar registro de usuario
    registerForm.addEventListener("submit", async (event) => {
        event.preventDefault();

        const nombre = document.getElementById("nombre").value;
        const correo = document.getElementById("correo").value;
        const contraseña = document.getElementById("password").value;
        const confirmarContraseña = document.getElementById("confirmPassword").value;

        // Verificar que ambas contraseñas coincidan
        if (contraseña !== confirmarContraseña) {
            errorPasswordMatch.textContent = "Las contraseñas no coinciden.";
            errorPasswordMatch.classList.remove("d-none");
            return; // Detener el envío del formulario
        } else {
            errorPasswordMatch.classList.add("d-none");
        }

        try {
            const response = await fetch("https://localhost:7291/usuarios/CrearUsuario", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ Nombre: nombre, Correo: correo, Contraseña: contraseña })
            });

            if (!response.ok) {
                const errorMessage = await response.text();
                throw new Error(errorMessage || "Error al registrar usuario");
            }

            registerCard.classList.add("d-none");
            loginCard.classList.remove("d-none");
        } catch (error) {
            errorRegister.textContent = "Error al registrar usuario";
            errorRegister.classList.remove("d-none");
        }
    });
});

document.addEventListener("DOMContentLoaded", () => {
    const logoutBtn = document.getElementById("logoutUsuario");
    if (logoutBtn) {
      logoutBtn.addEventListener("click", (e) => {
        e.preventDefault(); // Previene redirección automática
  
        // Limpiar datos de sesión
        localStorage.removeItem("token");
        localStorage.removeItem("usuarioId");
        localStorage.removeItem("nombreUsuario");
  
        // Redirigir a login
        window.location.href = "../login.html";
      });
    }
  });
  
