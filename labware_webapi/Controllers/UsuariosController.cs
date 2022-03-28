﻿using labware_webapi.Contexts;
using labware_webapi.Domains;
using labware_webapi.Interfaces;
using labware_webapi.Repositories;
using labware_webapi.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
        public IActionResult Listar()
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

        [HttpPut]
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





        [HttpPost("imagem/dir")]
        public IActionResult postDIR(IFormFile arquivo)
        {
            try
            {
                if (arquivo == null)
                    return BadRequest(new { mensagem = "Nenhum arquivo selecionado" });

                if (arquivo.Length > 500000)
                    return BadRequest(new { mensagem = "O tamanho máximo da imagem foi atingido." });

                string extensao = arquivo.FileName.Split('.').Last();

                if (extensao != "png")
                    return BadRequest(new { mensagem = "Apenas arquivos .png e .jpg são permitidos." });


                int idUsuario = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

                _usuarioRepository.SalvarFotoDir(arquivo, idUsuario);

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("imagem/dir")]
        public IActionResult getDIR()
        {
            try
            {

                int idUsuario = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

                string base64 = _usuarioRepository.AtualizarFotoDir(idUsuario);

                return Ok(base64);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
