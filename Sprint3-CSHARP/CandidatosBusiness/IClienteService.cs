using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SprintModel;

namespace SprintBusiness
{
    public interface IClienteService
    {
        List<ClienteModel> ListarTodos();
        ClienteModel? ObterPorId(int id);
        ClienteModel? ObterPorEmail(string email);
        ClienteModel Criar(ClienteModel cliente);
        bool Atualizar(ClienteModel cliente);
        bool Remover(int id);
        bool ValidarLogin(string email, string senha);
    }
}
