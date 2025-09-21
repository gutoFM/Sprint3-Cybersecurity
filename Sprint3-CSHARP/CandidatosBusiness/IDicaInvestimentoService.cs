using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SprintModel;

namespace SprintBusiness
{
    public interface IDicaInvestimentoService
    {
        List<DicaInvestimentoModel> ListarTodos();
        DicaInvestimentoModel? ObterPorId(int id);
        List<DicaInvestimentoModel> ObterPorCategoria(string categoria);
        DicaInvestimentoModel Criar(DicaInvestimentoModel dica);
        bool Atualizar(DicaInvestimentoModel dica);
        bool Remover(int id);
    }
}