using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoPrograIII.Context;
using ProyectoPrograIII.Repository;

namespace WebApi_Easy_Commerce.Controllers
{
    [Route("auxiliares")]
    [ApiController]
    public class AuxiliarController : ControllerBase
    {
        private readonly CategoriaMarcaProveedorDao _dao;

        public AuxiliarController(ProyectoProgra3Context contexto)
        {
            _dao = new CategoriaMarcaProveedorDao(contexto);
        }

        [HttpGet("categorias/todas")]
        public IActionResult ObtenerCategorias()
        {
            var lista = _dao.GetCategorias();
            return Ok(lista);
        }

        [HttpGet("marcas/todas")]
        public IActionResult ObtenerMarcas()
        {
            var lista = _dao.GetMarcas();
            return Ok(lista);
        }

        [HttpGet("proveedores/todos")]
        public IActionResult ObtenerProveedores()
        {
            var lista = _dao.GetProveedores();
            return Ok(lista);
        }
    }
}
