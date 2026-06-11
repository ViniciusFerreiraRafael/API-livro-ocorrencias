<h1 align="center">
  📋 API de Gerenciamento de Ocorrências Escolares
</h1>

<p align="center">
  <strong>Sistema RESTful para digitalização e gestão de ocorrências de indisciplina escolar</strong><br/>
  Projeto Final · 3º Semestre · Análise e Desenvolvimento de Sistemas
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET 10"/>
  <img src="https://img.shields.io/badge/C%23-ASP.NET%20Core-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="C#"/>
  <img src="https://img.shields.io/badge/Entity%20Framework%20Core-10.0-512BD4?style=for-the-badge&logo=nuget&logoColor=white" alt="EF Core"/>
  <img src="https://img.shields.io/badge/SQLite-003B57?style=for-the-badge&logo=sqlite&logoColor=white" alt="SQLite"/>
  <img src="https://img.shields.io/badge/Swagger-UI-85EA2D?style=for-the-badge&logo=swagger&logoColor=black" alt="Swagger"/>
</p>

---

## 📖 Sobre o Projeto

Grande parte das escolas ainda gerencia as ocorrências de indisciplina de forma manual, usando o tradicional **"livro negro"** — um caderno físico que circula entre professores e coordenação. Esse método gera perda de tempo, dificuldade de busca histórica e total impossibilidade de gerar estatísticas.

Esta API foi desenvolvida para **digitalizar e centralizar** esse processo. Com ela, a escola pode:

- ✅ Registrar ocorrências vinculando aluno, professor e tipo de infração
- ✅ Consultar o histórico completo de qualquer aluno em segundos
- ✅ Editar e excluir registros com total rastreabilidade
- ✅ Servir como base para futuros aplicativos mobile e portais de pais

---

## 👥 Integrantes do Grupo

| Nome | Responsabilidade no Projeto |
|---|---|
| **Luiz Felipe** | Levantamento de requisitos e regras de negócio |
| **Vinicius** | Modelagem de entidades, banco de dados e Migrations |
| **João Pedro** | Configuração da aplicação e Controllers de suporte |
| **Julio** | Controller principal de Ocorrências e testes de integração |

---

## 🛠️ Tecnologias Utilizadas

| Tecnologia | Versão | Finalidade |
|---|---|---|
| **C#** | 13 | Linguagem de programação principal |
| **ASP.NET Core Web API** | .NET 10 | Framework da API RESTful |
| **Entity Framework Core** | 10.0.8 | ORM para mapeamento objeto-relacional |
| **SQLite** | — | Banco de dados relacional embarcado |
| **Swagger / OpenAPI** | 10.0.8 | Documentação e interface de testes da API |
| **Postman** | — | Coleção de testes dos endpoints |

---

## 🏗️ Arquitetura do Projeto

O projeto segue o padrão de **arquitetura em camadas** exigido pelo edital, com separação clara de responsabilidades:

```
ProjetoAPI/
│
├── Models/                  # Entidades do domínio com validações (Data Annotations)
│   ├── Aluno.cs
│   ├── Turma.cs
│   ├── Professor.cs
│   ├── MotivoInfracao.cs
│   └── Ocorrencia.cs
│
├── Data/                    # Contexto do banco de dados
│   └── AppDbContext.cs      # DbContext com Fluent API e Seed de dados (HasData)
│
├── Controllers/             # Endpoints REST (CRUD completo para cada entidade)
│   ├── AlunosController.cs
│   ├── TurmasController.cs
│   ├── ProfessoresController.cs
│   ├── MotivosInfracaoController.cs
│   └── OcorrenciasController.cs
│
├── Migrations/              # Histórico de versões do schema do banco de dados
├── Program.cs               # Ponto de entrada e configuração da aplicação
├── escola.db                # Banco de dados SQLite (gerado pelo EF Core)
└── postman_collection.json  # Coleção de testes prontos para importar no Postman
```

### Padrões técnicos aplicados

- **Fluent API** no `AppDbContext` para mapeamento explícito dos 4 relacionamentos 1:N
- **Data Annotations** nos Models com mensagens de validação em português
- **Seed de dados** via `HasData` — banco populado automaticamente na primeira migration
- **Eager Loading** com `.Include()` e `.ThenInclude()` para retornar entidades relacionadas
- **Assincronicidade** completa com `async/await` em todos os endpoints
- **Validação de Foreign Keys** com `AnyAsync` antes de salvar, retornando `404` se a entidade relacionada não existir

---

## ⚙️ Como Executar o Projeto

### Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download) instalado
- [EF Core CLI](https://learn.microsoft.com/ef/core/cli/dotnet) instalado globalmente

```bash
# Instalar a CLI do Entity Framework (se ainda não tiver)
dotnet tool install --global dotnet-ef
```

### Passo a Passo

**1. Clone o repositório**
```bash
git clone https://github.com/seu-usuario/seu-repositorio.git
cd seu-repositorio/ProjetoAPI
```

**2. Restaure os pacotes NuGet**
```bash
dotnet restore
```

**3. Aplique as Migrations e gere o banco de dados**

> Este comando cria o arquivo `escola.db` e popula automaticamente as tabelas com os dados de seed (turmas, alunos, professores e motivos de infração).

```bash
dotnet ef database update
```

**4. Execute a API**
```bash
dotnet run
```

A API estará disponível em `http://localhost:5000` (ou na porta indicada no terminal).

---

## 📚 Documentação e Testes

### Swagger UI

Com a API em execução em ambiente de desenvolvimento, acesse a interface interativa do Swagger no navegador:

```
http://localhost:5000/swagger
```

A documentação lista todos os endpoints disponíveis, com os esquemas de request e response, e permite executar requisições diretamente na interface.

### Postman

A coleção de testes completa está disponível na raiz do repositório:

```
postman_collection.json
```

Para importar: abra o Postman → **Import** → selecione o arquivo `postman_collection.json`.

---

## 🗂️ Endpoints Disponíveis

| Método | Rota | Descrição |
|---|---|---|
| `GET` | `/api/alunos` | Lista todos os alunos com a turma |
| `GET` | `/api/alunos/{id}` | Busca um aluno por ID |
| `POST` | `/api/alunos` | Cria um novo aluno |
| `PUT` | `/api/alunos/{id}` | Atualiza os dados de um aluno |
| `DELETE` | `/api/alunos/{id}` | Remove um aluno |
| `GET` | `/api/turmas` | Lista todas as turmas com os alunos |
| `GET` | `/api/professores` | Lista todos os professores |
| `GET` | `/api/motivosinfracao` | Lista todos os motivos de infração |
| `GET` | `/api/ocorrencias` | Lista todas as ocorrências (com Aluno, Turma, Professor e Motivo) |
| `GET` | `/api/ocorrencias/{id}` | Busca uma ocorrência por ID |
| `POST` | `/api/ocorrencias` | Registra uma nova ocorrência |
| `PUT` | `/api/ocorrencias/{id}` | Atualiza uma ocorrência |
| `DELETE` | `/api/ocorrencias/{id}` | Remove uma ocorrência |

---

## 🗃️ Diagrama Entidade-Relacionamento

<img width="1426" height="660" alt="Diagrama ER" src="https://github.com/user-attachments/assets/ebc96dd7-6cc4-4a9c-93f8-bced855c9b3c" />

---

## 📄 Licença

Este projeto foi desenvolvido para fins acadêmicos no curso de **Análise e Desenvolvimento de Sistemas**.
