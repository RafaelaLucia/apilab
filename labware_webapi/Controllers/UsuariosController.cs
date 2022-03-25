using labware_webapi.Contexts;
using labware_webapi.Domains;
using labware_webapi.Interfaces;
using labware_webapi.Repositories;
using labware_webapi.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace labware_webapi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }
        public UsuariosController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [HttpGet]
        public IActionResult ListarMedicos()
        {
            try
            {
                return Ok(_usuarioRepository.ListarTodos());
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            try
            {
                if (usuario == null)
                {
                    return BadRequest("Não foi possível cadastrar");
                };
                //usuario.Senha = Criptografia.GerarHash(usuario.Senha);
                _usuarioRepository.Cadastrar(usuario);
                return StatusCode(201);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPut("{idUsuario}")]
        public IActionResult Atualizar(int id, Usuario usuario)
        {
            try
            {
                _usuarioRepository.AtualizarPeloId(id, usuario);
                return StatusCode(204);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpDelete("{idUsuario}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _usuarioRepository.Deletar(id);
                return StatusCode(204);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }

        }

        [HttpGet("{idUsuario}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                return Ok(_usuarioRepository.Buscar(id));
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

    }
}
