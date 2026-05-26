using Microsoft.EntityFrameworkCore;
using ProjetoAPI.Models;

namespace ProjetoAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<MotivoInfracao> MotivosInfracao { get; set; }
        public DbSet<Ocorrencia> Ocorrencias { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=escola.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ==================== RELACIONAMENTOS (Fluent API) ====================

            // Ocorrencia -> Aluno (N:1)
            modelBuilder.Entity<Ocorrencia>()
                .HasOne(o => o.Aluno)
                .WithMany(a => a.Ocorrencias)
                .HasForeignKey(o => o.AlunoId);

            // Ocorrencia -> Professor (N:1)
            modelBuilder.Entity<Ocorrencia>()
                .HasOne(o => o.Professor)
                .WithMany(p => p.Ocorrencias)
                .HasForeignKey(o => o.ProfessorId);

            // Ocorrencia -> MotivoInfracao (N:1)
            modelBuilder.Entity<Ocorrencia>()
                .HasOne(o => o.MotivoInfracao)
                .WithMany(m => m.Ocorrencias)
                .HasForeignKey(o => o.MotivoInfracaoId);

            // Aluno -> Turma (N:1)
            modelBuilder.Entity<Aluno>()
                .HasOne(a => a.Turma)
                .WithMany(t => t.Alunos)
                .HasForeignKey(a => a.TurmaId);

            // ==================== SEED DE DADOS INICIAIS ====================

            modelBuilder.Entity<Turma>().HasData(
                new Turma
                {
                    Id = 1,
                    Nome = "3º Ano - EE. Prof Theodorico de Oliveira",
                    AnoLetivo = 2026,
                    Turno = "Manhã"
                }
            );

            modelBuilder.Entity<Aluno>().HasData(
                new Aluno { Id = 1, RegistroAluno = "RA1001", Nome = "Vinícius",    ContatoResponsavel = "11999990001", TurmaId = 1 },
                new Aluno { Id = 2, RegistroAluno = "RA1002", Nome = "Julio",       ContatoResponsavel = "11999990002", TurmaId = 1 },
                new Aluno { Id = 3, RegistroAluno = "RA1003", Nome = "João Pedro",  ContatoResponsavel = "11999990003", TurmaId = 1 },
                new Aluno { Id = 4, RegistroAluno = "RA1004", Nome = "Luiz Felipe", ContatoResponsavel = "11999990004", TurmaId = 1 }
            );

            modelBuilder.Entity<Professor>().HasData(
                new Professor { Id = 1, Nome = "Prof. Carlos Silva",   Disciplina = "Matemática",   Matricula = "MAT001" },
                new Professor { Id = 2, Nome = "Profa. Ana Souza",     Disciplina = "Português",    Matricula = "POR002" },
                new Professor { Id = 3, Nome = "Prof. Marcos Pereira", Disciplina = "História",     Matricula = "HIS003" }
            );

            modelBuilder.Entity<MotivoInfracao>().HasData(
                new MotivoInfracao { Id = 1, Descricao = "Uso de celular em sala de aula",            Gravidade = "Leve"   },
                new MotivoInfracao { Id = 2, Descricao = "Desrespeito ao professor",                  Gravidade = "Grave"  },
                new MotivoInfracao { Id = 3, Descricao = "Briga com colega",                          Gravidade = "Grave"  },
                new MotivoInfracao { Id = 4, Descricao = "Atraso recorrente sem justificativa",       Gravidade = "Médio"  },
                new MotivoInfracao { Id = 5, Descricao = "Perturbação do andamento da aula",          Gravidade = "Leve"   }
            );
        }
    }
}
