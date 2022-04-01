using labware_webapi.Domains;
using labware_webapi.Interfaces;
using labware_webapi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace labware_webapi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetosController : ControllerBase
    {
        private IProjetoRepository _repository { get; set; }
        public ProjetosController()
        {
            _repository = new ProjetoRepository();
        }


        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_repository.ListarTodos());
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }



        [HttpGet("{idProjeto}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                return Ok(_repository.Buscar(id));
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }




        [HttpPost]
        public IActionResult Cadastrar(Projeto projeto)
        {
            try
            {
                if (projeto == null)
                {
                    return BadRequest("Não foi possível cadastrar");
                };

                _repository.Cadastrar(projeto);
                return StatusCode(201);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }



        [HttpPut("{idProjeto}")]
        public IActionResult Atualizar(int id, Projeto projeto)
        {
            try
            {
                _repository.Atualizar(id, projeto);
                return StatusCode(204);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }


        [HttpDelete("{idTask}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _repository.Deletar(id);
                return StatusCode(204);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }

        }




        [HttpPost("{idProjeto}")]
        public IActionResult Postar(IFormFile arquivo, int idProjeto)
        {
            try
            {
                if (arquivo == null)
                    return BadRequest(new { mensagem = "Nenhum arquivo selecionado" });

                if (arquivo.Length > 500000)
                    return BadRequest(new { mensagem = "O tamanho máximo da imagem foi atingido." });

                string extensao = arquivo.FileName.Split('.').Last();

                if (extensao != "png")
                    return BadRequest(new { mensagem = "Apenas arquivos .png são permitidos." });


                // int idProjeto = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

                _repository.SalvarFoto(arquivo, idProjeto);

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


            [HttpGet("imagem/{idProjeto}")]
            public IActionResult getDIR(int id)
            {
                try
                {
                string base64 = _repository.AtualizarFoto(id);
                return Ok(base64);

                }
                catch (Exception ex)
                {
                return BadRequest(ex.Message);
                }
            }

        

    }
}
