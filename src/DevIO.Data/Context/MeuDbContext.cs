using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevIO.Data.Context
{
    public class MeuDbContext : DbContext
    {

        //public MeuDbContext(DbContextOptions options) : base(options)
        //{

        //}
        public MeuDbContext(DbContextOptions<MeuDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        public DbSet<Estilo> Estilos { get; set; }
        public DbSet<Zona> Zonas { get; set; }
        public DbSet<Bairro> Bairros { get; set; }
        public DbSet<Atende> Atendes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<TipoContato> TipoContatos { get; set; }

        public DbSet<Idioma> Idiomas { get; set; }
        public DbSet<Olhos> Olhares { get; set; }
        public DbSet<Sexo> Sexos { get; set; }
        public DbSet<TipoAnuncio> TipoAnuncios { get; set; }
        public DbSet<TipoPagamento> TipoPagamentos { get; set; }
        public DbSet<TipoLog> TipoLogs { get; set; }
        public DbSet<TipoConta> TipoContas { get; set; }
        public DbSet<Banco> Bancos { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<TipoCritica> TipoCriticas { get; set; }
        public DbSet<Visualizacao> Visualizacoes { get; set; }

        public DbSet<Garota> Garotas { get; set; }

        public DbSet<GarotaCategoria> GarotaCategorias { get; set; }

        public DbSet<Novidade> Novidades { get; set; }




        public DbSet<PermissoesSistema> PermissoesSistemas { get; set; }
        public DbSet<PermissoesInstituicao> PermissoesInstituicoes { get; set; }

        // Para mapear

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // se tiver alguma coluna sem especificação do varchar, o seu defalul será 100, como mostrado abaixo
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");
            //property.Relational().ColumnType = "varchar(100)";

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuDbContext).Assembly);

            //tirando o cascade delete
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}