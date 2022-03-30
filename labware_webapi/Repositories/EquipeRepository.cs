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
    public class EquipeRepository : IEquipeRepository
    {
        LabWatchContext ctx = new LabWatchContext();

        public void AtualizarPeloId(int idEquipe, Equipe EquipeAtualizada)
        {
            Equipe equipeBscada = ctx.Equipes.Find(idEquipe);

            if (EquipeAtualizada.NomeEquipe != null)
            {
                equipeBscada.IdUsuario = EquipeAtualizada.IdUsuario;
                equipeBscada.NomeEquipe = EquipeAtualizada.NomeEquipe;
                equipeBscada.HorasTrabalhadas = EquipeAtualizada.HorasTrabalhadas;
                ctx.Equipes.Update(equipeBscada);
                ctx.SaveChanges();
            }
        }

        public Equipe Buscar(int id)
        {
            return ctx.Equipes.FirstOrDefault(t => t.IdEquipe == id);
        }

        public void Cadastrar(Equipe novaEquipe)
        {
            ctx.Equipes.Add(novaEquipe);
            ctx.SaveChanges();
        }

        public void AdicionarPessoasEquipe(int idEquipe, int idUsuario)
        {
            
           //var array = [];
            var validar= ctx.Usuarios.FirstOrDefault(i => i.IdUsuario == idUsuario);
          //  List<Usuario> Usuarios;
            Equipe equipe = ctx.Equipes.Find(idEquipe);
          //  Usuario usuario = ctx.Usuarios.Find(idUsuario);

            if (equipe != null)
            {
                if(validar != null)
                {
                  ctx.Equipes.ToList();
                }   
                
            }
    }

        public void Deletar(int idEquipe)
        {
            ctx.Equipes.Remove(Buscar(idEquipe));
            ctx.SaveChanges();
        }

        public List<Equipe> ListarTodos()
        {

            return ctx.Equipes.Include(c => c.IdUsuarioNavigation).ToList();
        }
    }
}
