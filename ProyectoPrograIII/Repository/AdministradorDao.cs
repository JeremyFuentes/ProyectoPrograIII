using Microsoft.AspNetCore.Identity;
using ProyectoPrograIII.Context;
using ProyectoPrograIII.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPrograIII.Repository
{
    public class AdministradorDao
    {
        private readonly ProyectoProgra3Context contexto;

        public AdministradorDao(ProyectoProgra3Context contexto)
        {
            this.contexto = contexto;
        }

        public List<Administrador> GetAll() => contexto.Administradores.ToList();

        public Administrador? GetById(int id) =>
            contexto.Administradores.FirstOrDefault(a => a.IdAdmin == id);

        public Administrador? GetByCorreo(string correo) =>
            contexto.Administradores.FirstOrDefault(a => a.Correo == correo);

        public bool Crear(Administrador admin)
        {
            try
            {
                Console.WriteLine($"[DEBUG] Nombre: {admin.Nombre}, Correo: {admin.Correo}, Contraseña: {admin.ContrasenaHash}");

                var hasher = new PasswordHasher<Administrador>();
                admin.ContrasenaHash = hasher.HashPassword(admin, admin.ContrasenaHash);
                contexto.Administradores.Add(admin);
                contexto.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR al crear administrador: " + ex.Message);
                return false;
            }
        }

        public bool Actualizar(Administrador admin)
        {
            var existente = GetById(admin.IdAdmin);
            if (existente == null) return false;

            existente.Nombre = admin.Nombre;
            existente.Correo = admin.Correo;
            var hasher = new PasswordHasher<Administrador>();
            existente.ContrasenaHash = hasher.HashPassword(admin, admin.ContrasenaHash);

            contexto.SaveChanges();
            return true;
        }

        public bool Eliminar(int id)
        {
            var admin = GetById(id);
            if (admin == null) return false;

            contexto.Administradores.Remove(admin);
            contexto.SaveChanges();
            return true;
        }

        public Administrador? Login(string correo, string contraseña)
        {
            var admin = GetByCorreo(correo);
            if (admin == null) return null;

            var hasher = new PasswordHasher<Administrador>();
            var result = hasher.VerifyHashedPassword(admin, admin.ContrasenaHash, contraseña);
            return result == PasswordVerificationResult.Success ? admin : null;
        }
    }
}
