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
    public class LoginDao
    {
        #region Contex
        public ProyectoProgra3Context contexto = new ProyectoProgra3Context();
        #endregion

        #region Seleccionar Todo
        public List<Usuario> getAll()
        {
            var usuario = contexto.Usuarios.ToList<Usuario>();
            return usuario;
        }
        #endregion

        #region Seleccionar por Id
        public Usuario? GetById(int id)
        {
            var usuario = contexto.Usuarios.Where(x => x.UsuarioId == id).FirstOrDefault();
            return usuario == null ? null : usuario;
        }
        #endregion

        #region Obtener por Corroo
        public Usuario? GetByCorreo(string correo) => 
            contexto.Usuarios.FirstOrDefault(a => a.Correo == correo);
        #endregion

        #region Insertar
        public bool CrearUsuario(Usuario usuario)
        {
            try
            {
                var user = new Usuario
                {
                    Nombre = usuario.Nombre,
                    Direccion = usuario.Direccion,
                    Contacto = usuario.Contacto,
                    Correo = usuario.Correo,
                    GoogleId = usuario.GoogleId,
                    Contraseña = new PasswordHasher<Usuario>().HashPassword(usuario, usuario.Contraseña),
                    MetodoLogin = usuario.MetodoLogin
                };
                contexto.Usuarios.Add(user);

                contexto.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear usuario: {ex.Message}");
                return false;
            }
        }
        #endregion

        #region Actualizar
        public bool actualizar(int id, Usuario actualizar)
        {
            try
            {
                var usuarioUpdate = GetById(id);

                if (usuarioUpdate == null)
                {
                    Console.WriteLine("Usuario es null");
                    return false;
                }

                usuarioUpdate.Nombre = actualizar.Nombre;
                usuarioUpdate.Direccion = actualizar.Direccion;
                usuarioUpdate.Contacto = actualizar.Contacto;
                usuarioUpdate.Correo = actualizar.Correo;
                usuarioUpdate.GoogleId = actualizar.GoogleId;
                usuarioUpdate.MetodoLogin = actualizar.MetodoLogin;

                // Solo rehashear si cambió la contraseña
                if (!string.IsNullOrWhiteSpace(actualizar.Contraseña))
                {
                    var hasher = new PasswordHasher<Usuario>();
                    usuarioUpdate.Contraseña = hasher.HashPassword(usuarioUpdate, actualizar.Contraseña);
                }

                contexto.Usuarios.Update(usuarioUpdate);
                contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return false;
            }
        }
        #endregion

        #region Eliminar
        public bool eliminarUsuario(int id)
        {
            var borrar = GetById(id);
            try
            {
                if (borrar == null)
                {
                    return false;
                }
                else
                {
                    contexto.Usuarios.Remove(borrar);
                    contexto.SaveChanges();
                    return true;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return false;
            }
        }
        #endregion

        #region Auntentificacion
        public Usuario login(string correo, string contraseña)
        {
            var usuario = contexto.Usuarios.FirstOrDefault(u => u.Correo == correo);
            if (usuario == null)
                return null;

            var hasher = new PasswordHasher<Usuario>();
            var result = hasher.VerifyHashedPassword(usuario, usuario.Contraseña, contraseña);

            Console.WriteLine($"[DEBUG] Verificación: {result}");

            return result == PasswordVerificationResult.Success ? usuario : null;
        }
        #endregion

        #region Auntentificacion Direccion y Contacto
        public (bool valido, List<string> camposFaltantes) ValidarDireccionYContacto(int usuarioId)
        {
            var usuario = contexto.Usuarios.FirstOrDefault(u => u.UsuarioId == usuarioId);

            if (usuario == null)
            {
                throw new Exception("El usuario no existe.");
            }

            List<string> camposFaltantes = new List<string>();

            if (string.IsNullOrWhiteSpace(usuario.Direccion))
            {
                camposFaltantes.Add("Dirección");
            }

            if (string.IsNullOrWhiteSpace(usuario.Contacto))
            {
                camposFaltantes.Add("Contacto");
            }

            return (camposFaltantes.Count == 0, camposFaltantes);
        }

        #endregion

        #region Login Google
        public Usuario AutenticarConGoogle(string googleId, string email, string nombre)
        {
            // Buscar por GoogleId
            var usuario = contexto.Usuarios.FirstOrDefault(u => u.GoogleId == googleId);

            if (usuario == null)
            {
                // Si no tiene GoogleId, buscar por correo (puede que haya iniciado antes por otro método)
                usuario = contexto.Usuarios.FirstOrDefault(u => u.Correo == email);

                if (usuario == null)
                {
                    // Crear nuevo usuario
                    usuario = new Usuario
                    {
                        GoogleId = googleId,
                        Correo = email,
                        Nombre = nombre,
                        MetodoLogin = "Google"
                    };

                    contexto.Usuarios.Add(usuario);
                    contexto.SaveChanges();
                }
                else
                {
                    // Si existe con el correo pero no tiene GoogleId, lo actualizamos
                    usuario.GoogleId = googleId;
                    usuario.MetodoLogin = "Google";
                    contexto.SaveChanges();
                }
            }

            return usuario;
        }
        #endregion
    }
}
