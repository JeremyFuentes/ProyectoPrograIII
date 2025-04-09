document.getElementById("adminLoginForm").addEventListener("submit", async function (e) {
  e.preventDefault();

  const correo = document.getElementById("correoAdmin").value;
  const contrasena = document.getElementById("passwordAdmin").value;
  const errorMsg = document.getElementById("errorAdminLogin");

  const datos = {
    correo: correo,
    contrasenaHash: contrasena
  };

  try {
    const response = await fetch("https://localhost:7291/admin/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(datos)
    });

    console.log("Response status:", response.status); // 👉 debug

    if (!response.ok) {
      throw new Error("Credenciales incorrectas");
    }

    const result = await response.json();
    console.log("Resultado:", result); // 👉 debug

    localStorage.setItem("adminId", result.idAdmin);

    // ✅ Redirige
    window.location.href = "indexadmin.html";

  } catch (error) {
    console.error("Error:", error); // 👉 debug
    errorMsg.classList.remove("d-none");
    errorMsg.textContent = "❌ " + error.message;
  }
});
