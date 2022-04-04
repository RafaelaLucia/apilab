using labware_webapi.Contexts;
using labware_webapi.Domains;
using labware_webapi.Interfaces;
using labware_webapi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Patrimonio.Utils;
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




        /* [HttpPost]
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
         }*/



        [HttpPut("{idProjeto}")]
        public IActionResult Atualizar(int idProjeto, Projeto projeto)
        {
            try
            {
                _repository.Atualizar(projeto, idProjeto);
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

    }
}
