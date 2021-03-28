using Senai.Peoples.WebApi.Dominios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Interface
{
    public interface IFuncionariosRepository
    {
        List<FuncionarioDomain> Listar();

        FuncionarioDomain BuscarPorId(int id);

        void Deletar(int id);

        void Atualizar(int id, FuncionarioDomain novoFuncionario);

        void Inserir(FuncionarioDomain novoFuncionario);
    }
}
