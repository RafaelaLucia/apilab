using labware_webapi.Contexts;
using labware_webapi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace labware_webapi.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        LabWatchContext ctx = new();
        public void Atualizar(int idTask, Domains.Task taskAtualizada)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(Domains.Task novaTask)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int idTask)
        {
            //ctx.Tasks.Remove(BuscarPorId(idMedico));
            ctx.SaveChanges();
        }

        public List<Domains.Task> ListarMinhas(int idUsuario)
        {
            throw new NotImplementedException();
        }

        public List<Domains.Task> ListarTodos()
        {
            return ctx.Tasks.ToList();
        }
    }
}
