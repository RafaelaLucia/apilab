﻿using labware_webapi.Domains;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace labware_webapi.Interfaces
{
    interface IProjetoRepository
    {
        List<Projeto> ListarTodos();
        Projeto Buscar(int idProjeto);
        void Cadastrar(Projeto novoProjeto);
        void Deletar(int idProjeto);
        public Projeto Atualizar(Projeto projeto);
    }
}
