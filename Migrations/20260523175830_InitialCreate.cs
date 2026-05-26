using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjetoAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MotivosInfracao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Gravidade = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotivosInfracao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Professores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Disciplina = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    Matricula = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turmas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    AnoLetivo = table.Column<int>(type: "INTEGER", nullable: false),
                    Turno = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turmas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RegistroAluno = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ContatoResponsavel = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    TurmaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alunos_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ocorrencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataOcorrencia = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataRegistro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Observacao = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    AlunoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProfessorId = table.Column<int>(type: "INTEGER", nullable: false),
                    MotivoInfracaoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocorrencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ocorrencias_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ocorrencias_MotivosInfracao_MotivoInfracaoId",
                        column: x => x.MotivoInfracaoId,
                        principalTable: "MotivosInfracao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ocorrencias_Professores_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MotivosInfracao",
                columns: new[] { "Id", "Descricao", "Gravidade" },
                values: new object[,]
                {
                    { 1, "Uso de celular em sala de aula", "Leve" },
                    { 2, "Desrespeito ao professor", "Grave" },
                    { 3, "Briga com colega", "Grave" },
                    { 4, "Atraso recorrente sem justificativa", "Médio" },
                    { 5, "Perturbação do andamento da aula", "Leve" }
                });

            migrationBuilder.InsertData(
                table: "Professores",
                columns: new[] { "Id", "Disciplina", "Matricula", "Nome" },
                values: new object[,]
                {
                    { 1, "Matemática", "MAT001", "Prof. Carlos Silva" },
                    { 2, "Português", "POR002", "Profa. Ana Souza" },
                    { 3, "História", "HIS003", "Prof. Marcos Pereira" }
                });

            migrationBuilder.InsertData(
                table: "Turmas",
                columns: new[] { "Id", "AnoLetivo", "Nome", "Turno" },
                values: new object[] { 1, 2026, "3º Ano - EE. Prof Theodorico de Oliveira", "Manhã" });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "ContatoResponsavel", "Nome", "RegistroAluno", "TurmaId" },
                values: new object[,]
                {
                    { 1, "11999990001", "Vinícius", "RA1001", 1 },
                    { 2, "11999990002", "Julio", "RA1002", 1 },
                    { 3, "11999990003", "João Pedro", "RA1003", 1 },
                    { 4, "11999990004", "Luiz Felipe", "RA1004", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_TurmaId",
                table: "Alunos",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencias_AlunoId",
                table: "Ocorrencias",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencias_MotivoInfracaoId",
                table: "Ocorrencias",
                column: "MotivoInfracaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencias_ProfessorId",
                table: "Ocorrencias",
                column: "ProfessorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ocorrencias");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "MotivosInfracao");

            migrationBuilder.DropTable(
                name: "Professores");

            migrationBuilder.DropTable(
                name: "Turmas");
        }
    }
}
