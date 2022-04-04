using labware_webapi.Contexts;
using labware_webapi.Domains;
using labware_webapi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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


        public Projeto Atualizar(Projeto projeto)
        {
            ctx.Entry(projeto).State = EntityState.Modified;
            ctx.SaveChangesAsync();
            return projeto;

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

      
        
    }
}
