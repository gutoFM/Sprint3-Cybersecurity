using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SprintModel;

namespace SprintBusiness
{
    public interface IInvestimentoService
    {
        List<InvestimentoModel> ListarTodos();
        InvestimentoModel? ObterPorId(int id);
        List<InvestimentoModel> ObterPorTipo(string tipo);
        InvestimentoModel Criar(InvestimentoModel investimento);
        bool Atualizar(InvestimentoModel investimento);
        bool Remover(int id);
    }
}
