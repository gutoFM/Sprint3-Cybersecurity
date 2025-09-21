using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SprintData;
using SprintModel;

namespace SprintBusiness
{
    public class InvestimentoService(ApplicationDbContext context) : IInvestimentoService
    {
        private readonly ApplicationDbContext _context = context;

        public List<InvestimentoModel> ListarTodos() => [.. _context.Investimentos];

        public InvestimentoModel? ObterPorId(int id) =>
            _context.Investimentos.FirstOrDefault(i => i.idInvestimento == id);

        public List<InvestimentoModel> ObterPorTipo(string tipo) =>
            [.. _context.Investimentos.Where(i => i.tipo == tipo)];

        public InvestimentoModel Criar(InvestimentoModel investimento)
        {
            _context.Investimentos.Add(investimento);
            _context.SaveChanges();
            return investimento;
        }

        public bool Atualizar(InvestimentoModel investimento)
        {
            var existente = _context.Investimentos.Find(investimento.idInvestimento);
            if (existente == null) return false;

            existente.nome = investimento.nome;
            existente.tipo = investimento.tipo;
            existente.rentabilidadeAnual = investimento.rentabilidadeAnual;
            existente.risco = investimento.risco;
            existente.valorMinimo = investimento.valorMinimo;

            _context.SaveChanges();
            return true;
        }

        public bool Remover(int id)
        {
            var investimento = _context.Investimentos.Find(id);
            if (investimento == null) return false;

            _context.Investimentos.Remove(investimento);
            _context.SaveChanges();
            return true;
        }
    }
}
