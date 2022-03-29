using labware_webapi.Contexts;
using labware_webapi.Domains;
using labware_webapi.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                ctx.Projetos.Update(projBuscado);
                ctx.SaveChanges();
            }
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
            return ctx.Projetos.Include(x => x.IdEquipeNavigation)
                .ThenInclude(x => x.IdUsuarioNavigation)
                .Select(x => new Projeto()
                {
                    IdProjeto = x.IdProjeto,
                    TituloProjeto = x.TituloProjeto,
                    DataInicio = x.DataInicio,
                    DataConclusao = x.DataConclusao,

                    IdEquipeNavigation = new Equipe()
                    {
                        IdUsuarioNavigation = new Usuario()
                        {

                            NomeUsuario = x.IdEquipeNavigation.IdUsuarioNavigation.NomeUsuario
                        }
                    }
                }).ToList();
        }
    }
}
