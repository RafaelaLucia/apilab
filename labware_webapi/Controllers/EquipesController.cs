﻿using labware_webapi.Domains;
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
    public class EquipesController : ControllerBase
    {
        private IEquipeRepository _repository { get; set; }
        public EquipesController()
        {
            _repository = new EquipeRepository();
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



        [HttpGet("{idEquipe}")]
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
        public IActionResult Cadastrar(Equipe equipe)
        {
            try
            {
                if (equipe == null)
                {
                    return BadRequest("Não foi possível cadastrar");
                };

                _repository.Cadastrar(equipe);
                return StatusCode(201);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }



        [HttpPut("{idEquipe}")]
        public IActionResult Atualizar(int id, Equipe equipe)
        {
            try
            {
                _repository.AtualizarPeloId(id, equipe);
                return StatusCode(204);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }


        [HttpDelete("{idEquipe}")]
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

        [HttpPost("{idUsuario}")]
        public IActionResult AdicionarPessoa(int idEquipe, int idUsuario, Equipe equipe)
        {
            try
            { 
                if (equipe == null)
                {
                    return BadRequest("Não foi possível Adicionar");
                };

                _repository.AdicionarPessoasEquipe(idEquipe, idUsuario);
                return StatusCode(200);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

    }
}