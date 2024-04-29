using Microsoft.EntityFrameworkCore;
using SNGeneratorAPI.Model;

namespace SNGeneratorAPI.Data
{
    public class SNContext: DbContext
    {
        public SNContext(DbContextOptions<SNContext> opts)
            :base(opts)
        {         
        }

        public DbSet<Operacao> Operacao { get; set; }
        public DbSet<Componente> Componente { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=SNGenerator;User id=sa;Password=Senha@mtw;Trusted_Connection=False; TrustServerCertificate=True", builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
            base.OnConfiguring(optionsBuilder);


        }
    }
}
