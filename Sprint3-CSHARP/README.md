# 📋 README - Aplicativo mobile Fiap Invest+
 
## 🎯 Visão Geral
API backend para o aplicativo Fiap Invest+, uma plataforma completa de controle financeiro, sistemas de gestão e dicas de investimentos desenvolvida em C# .NET.
 
<br>
 
## ✨ Contexto da Aplicação:
- 🔐 Autenticação e gestão de usuários

- 📊 Controle Financeiro Pessoal

- 💰 Catálogo de produtos de investimento

- 📈 Dicas e recomendações personalizadas

- 🔄 Gestão de carteira de investimentos

- 🎯 Simulação de estratégias de investimento
 
<br>
 
## 🛠️ Tecnologias Utilizadas
- .NET 9.0 - Framework principal
- Entity Framework Core - ORM para acesso a dados
- Oracle Database - Banco de dados
- ASP.NET Core - Framework web
- Swagger/OpenAPI - Documentação da API
 
<br>
 
## 🚀 Instalação e Configuração
# Pré-requisitos
.NET SDK 9.0 ou superior
Oracle Database
Visual Studio 2022 ou VS Code

# Configuração
Clone o repositório
Configure a connection string no appsettings.json:

```
{
  "ConnectionStrings": {
    "DefaultConnection": "User Id=seu_usuario;Password=sua_senha;Data Source=localhost:1521/XE;"
  }
}
 ```

# Comando para a Execução:

```
# Restaurar pacotes
dotnet restore

# Executar migrations
dotnet ef migrations add InitialCreate --project FiapInvest.Data --output-dir Migrations
dotnet ef database update --project FiapInvest.Data

# Executar a aplicação
dotnet run --project FiapInvest.API
```



<br>
 
### 👥 Integrantes
 
| Nome               | RM        |
|--------------------|-----------|
| David Denunci      | RM 98603  |
| Lucas de Toledo    | RM 97913  |
| Fernando Popolili  | RM 99919  |
| Augusto Milreu     | RM 98245  |
| Matheus Zanardi    | RM 98832  |
