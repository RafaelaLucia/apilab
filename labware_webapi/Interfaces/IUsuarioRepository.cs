﻿using labware_webapi.Domains;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace labware_webapi.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario Login(string email, string senha);
        void SalvarFotoDir(IFormFile foto, int id_usuario);
        string AtualizarFotoDir(int id_usuario);
        public List<Usuario> ListarTodos();
        Usuario Buscar(int idUsuario);
        void Cadastrar(Usuario novoUsuario);
        void Deletar(int idUsuario);
        void AtualizarPeloId(int idUsuario, Usuario usuarioAtualizado);

    
    }
    }
