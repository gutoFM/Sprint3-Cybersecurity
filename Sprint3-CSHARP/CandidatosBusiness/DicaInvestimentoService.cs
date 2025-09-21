using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SprintData;
using SprintBusiness;
using SprintModel;

namespace SprintBusiness
{
    public class DicaInvestimentoService(ApplicationDbContext context) : IDicaInvestimentoService
    {
        private readonly ApplicationDbContext _context = context;

        public List<DicaInvestimentoModel> ListarTodos() => [.. _context.Dicas];

        public DicaInvestimentoModel? ObterPorId(int id) =>
            _context.Dicas.FirstOrDefault(d => d.idDica == id);

        public List<DicaInvestimentoModel> ObterPorCategoria(string categoria) =>
            [.. _context.Dicas.Where(d => d.categoria == categoria)];

        public DicaInvestimentoModel Criar(DicaInvestimentoModel dica)
        {
            _context.Dicas.Add(dica);
            _context.SaveChanges();
            return dica;
        }

        public bool Atualizar(DicaInvestimentoModel dica)
        {
            var existente = _context.Dicas.Find(dica.idDica);
            if (existente == null) return false;

            existente.titulo = dica.titulo;
            existente.descricao = dica.descricao;
            existente.categoria = dica.categoria;
            existente.dataPublicacao = dica.dataPublicacao;
            existente.link = dica.link;

            _context.SaveChanges();
            return true;
        }

        public bool Remover(int id)
        {
            var dica = _context.Dicas.Find(id);
            if (dica == null) return false;

            _context.Dicas.Remove(dica);
            _context.SaveChanges();
            return true;
        }
    }
}