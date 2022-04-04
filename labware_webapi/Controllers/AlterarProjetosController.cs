using labware_webapi.Contexts;
using labware_webapi.Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Patrimonio.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace labware_webapi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AlterarProjetosController : ControllerBase
    {

        private readonly LabWatchContext _context;

        public AlterarProjetosController(LabWatchContext context)
        {
            _context = context;
        }

        [HttpPut("{idProjeto}")]
        public async Task<IActionResult> PutEquipamento(int idProjeto, Projeto projeto, [FromForm] Projeto projetoAtualizado, IFormFile arquivo)
        {

            #region Upload da Imagem com extensões permitidas apenas
            string[] extensoesPermitidas = { "jpg", "png", "jpeg", "gif" };
            string uploadResultado = Upload.UploadFile(arquivo, extensoesPermitidas);

            if (uploadResultado == "")
            {
                return BadRequest("Arquivo não encontrado");
            }

            if (uploadResultado == "Extensão não permitida")
            {
                return BadRequest("Extensão de arquivo não permitida");
            }

            projetoAtualizado.fotoCliente = uploadResultado;
            #endregion

            projetoAtualizado = projeto;

            if (idProjeto != projeto.IdProjeto)
            {
                return BadRequest();
            }

            _context.Entry(projeto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjetoExists(idProjeto))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool ProjetoExists(int id)
        {
            return _context.Projetos.Any(e => e.IdProjeto == id);
        }


    }
}
