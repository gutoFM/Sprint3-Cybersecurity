using SprintModel;
using Microsoft.EntityFrameworkCore;

namespace SprintData
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        
        public DbSet<ClienteModel> Clientes { get; set; }
        public DbSet<DicaInvestimentoModel> Dicas { get; set; }
        public DbSet<InvestimentoModel> Investimentos { get; set; }
    }
}