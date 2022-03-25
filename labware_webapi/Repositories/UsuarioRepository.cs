using labware_webapi.Contexts;
using labware_webapi.Domains;
using labware_webapi.Interfaces;
using labware_webapi.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace labware_webapi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        LabWatchContext ctx = new();
        public string AtualizarFotoDir(int id_usuario)
        {
            throw new NotImplementedException();
        }

        public void AtualizarPeloId(int idUsuario, Usuario usuarioAtualizado)
        {
            throw new NotImplementedException();
        }

        public Usuario Buscar(int idUsuario)
        {
            return ctx.Usuarios.FirstOrDefault(m => m.IdUsuario == idUsuario);
        }

        public void Cadastrar(Usuario novoUsuario)
        {
            ctx.Usuarios.Add(novoUsuario);
            ctx.SaveChanges();
        }

        public void Deletar(int idUsuario)
        {
            ctx.Usuarios.Remove(Buscar(idUsuario));
            ctx.SaveChanges();
        }

        public List<Usuario> ListarTodos()
        {
            return ctx.Usuarios.ToList();
        }

        public Usuario Login(string email, string senha)
        {

            //return ctx.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha);

            var usuario = ctx.Usuarios.FirstOrDefault(u => u.Email == email);

            if (usuario != null)
            {
                bool confere = Criptografia.Comparar(senha, usuario.Senha);
                if (confere)
                    return usuario;
            }


            return null;
        }

        public void SalvarFotoDir(IFormFile foto, int id_usuario)
        {
            throw new NotImplementedException();
        }
    }
}
