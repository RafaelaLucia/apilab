using labware_webapi.Contexts;
using labware_webapi.Domains;
using labware_webapi.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace labware_webapi.Repositories
{
    public class ProjetoRepository : IProjetoRepository
    {
        LabWatchContext ctx = new LabWatchContext();


        public void Atualizar(int idProjeto, Projeto projetoAtualizado)
        {
            Projeto projBuscado = ctx.Projetos.Find(idProjeto);

            if (projetoAtualizado.TituloProjeto != null)
            {
                projBuscado.IdEquipe = projetoAtualizado.IdEquipe;
                projBuscado.IdStatusProjeto = projetoAtualizado.IdStatusProjeto;
                projBuscado.TituloProjeto = projetoAtualizado.TituloProjeto;
                projBuscado.DataInicio = projetoAtualizado.DataInicio;
                projBuscado.DataConclusao = projetoAtualizado.DataConclusao;
                projBuscado.nomeCliente = projetoAtualizado.nomeCliente;
                projBuscado.fotoCliente = projetoAtualizado.fotoCliente;
                ctx.Projetos.Update(projBuscado);
                ctx.SaveChanges();
            }
        }

        public string AtualizarFoto(int id_projeto)
        {
            string nome_arquivo = id_projeto.ToString() + ".png";
            string caminho = Path.Combine("clientefoto", nome_arquivo);

            if (File.Exists(caminho))
            {
                byte[] bytes_arquivo = File.ReadAllBytes(caminho);
                return Convert.ToBase64String(bytes_arquivo);
            }
            return null;
        }

        public Projeto Buscar(int idProjeto)
        {
            return ctx.Projetos.FirstOrDefault(t => t.IdProjeto == idProjeto);
        }

        public void Cadastrar(Projeto novoProjeto)
        {
            ctx.Projetos.Add(novoProjeto);
            ctx.SaveChanges();
        }

        public void Deletar(int idProjeto)
        {
            ctx.Projetos.Remove(Buscar(idProjeto));
            ctx.SaveChanges();
        }

        public List<Projeto> ListarTodos()
        {
            return ctx.Projetos
                .ToList();
        }

        public void SalvarFoto(IFormFile foto, int id_projeto)
        {
            string nome_arquivo = id_projeto.ToString() + ".png ";

            using (var stream = new FileStream(Path.Combine("clientefoto", nome_arquivo), FileMode.Create))
            {
                foto.CopyTo(stream);
            }
        }
    }
}
