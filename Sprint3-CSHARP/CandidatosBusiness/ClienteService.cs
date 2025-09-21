using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SprintModel;
using SprintData;

namespace SprintBusiness
{
    public class ClienteService(ApplicationDbContext context) : IClienteService
    {
        private readonly ApplicationDbContext _context = context;

        public List<ClienteModel> ListarTodos() => [.. _context.Clientes];

        public ClienteModel? ObterPorId(int id) =>
            _context.Clientes.FirstOrDefault(c => c.idCliente == id);

        public ClienteModel? ObterPorEmail(string email) =>
            _context.Clientes.FirstOrDefault(c => c.email == email);

        public ClienteModel Criar(ClienteModel cliente)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
            return cliente;
        }

        public bool Atualizar(ClienteModel cliente)
        {
            var existente = _context.Clientes.Find(cliente.idCliente);
            if (existente == null) return false;

            existente.nome = cliente.nome;
            existente.email = cliente.email;
            existente.senha = cliente.senha;
            existente.dataNascimento = cliente.dataNascimento;
            existente.saldo = cliente.saldo;

            _context.SaveChanges();
            return true;
        }

        public bool Remover(int id)
        {
            var cliente = _context.Clientes.Find(id);
            if (cliente == null) return false;

            _context.Clientes.Remove(cliente);
            _context.SaveChanges();
            return true;
        }

        public bool ValidarLogin(string email, string senha) =>
            _context.Clientes.Any(c => c.email == email && c.senha == senha);
    }
}
