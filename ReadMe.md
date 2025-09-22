# Projeto Cybersecurity - Sprint 3

Este repositório contém a implementação prática de técnicas de **Segurança em Aplicações** no contexto de CI/CD.  
Foram integradas ferramentas de **SAST, DAST e SCA** no pipeline, além da documentação da execução e relatórios gerados.

---

## Estrutura do Projeto
```
Sprint3-Cyber/
│
├── .github/
│   └── workflows/
│       ├── ci-sast-sca.yml       # Pipeline de CI para SAST + SCA
│       └── cd-dast.yml           # Pipeline de CD para DAST
│
├── ci/                           # Scripts auxiliares - Ainda não foi criado nenhum script .SH
│   ├── semgrep-rules/			  # Regras personalizadas SemGrep
│   │    └── custom-rule.yml  
│   ├── run-sast.sh               # Script para análise SAST (Semgrep)
│   ├── run-sca.sh                # Script para análise SCA (Dependency-Check)
│   └── run-dast.sh               # Script para análise DAST (OWASP ZAP)
│
├── docs/                         # Relatórios de segurança
│   └── Vazio                     
│
├── Sprint3-CSHARP/               # Projeto em C# analisado             
│   ├── CandidatosApi/
│   │   ├── bin/
│   │   ├── Controllers/
│   │   │   ├── ClientesController.cs
│   │   │   ├── DicasInvestimentosController.cs
│   │   │   └── InvestimentosController.cs
│   │   ├── obj/
│   │   ├── Properties/
│   │   ├── appsettings.Development.json
│   │   ├── appsettings.json
│   │   ├── CandidatesApi.http
│   │   ├── Program.cs
│   │   └── SprintApi.csproj
│   │
│   ├── CandidatosBusiness/
│   │   ├── bin/
│   │   ├── obj/
│   │   ├── ClienteService.cs
│   │   ├── DicalnvestimentoService.cs
│   │   ├── IClienteService.cs
│   │   ├── IDicalnvestimentoService.cs
│   │   ├── InvestimentoService.cs
│   │   └── SprintBusiness.csproj
│   │ 
│   ├── CandidatosData/
│   │   ├── bin/
│   │   ├── Migrations/
│   │   │   ├── 20250918005430_InitialCreate.cs
│   │   │   ├── 20250918005430_InitialCreate.Designer.cs
│   │   │   ├── ApplicationDbContextModelSnapshot.cs
│   │   ├── obj/
│   │   ├── ApplicationDbContext.cs
│   │   ├── ApplicationDbContextFactory.cs
│   │   └── SprintData.csproj
│   │
│   ├── CandidatosModel/
│   │   ├── bin/
│   │   ├── obj/
│   │   ├── ClienteModel.cs
│   │   ├── DicalnvestimentoModel.cs
│   │   ├── InvestimentosModel.cs
│   │   └── SprintModel.csproj
│   │ 
│   ├── README.md
│   └── Sprint.sln
│
└── README.txt
```

## Ferramentas Utilizadas

- **SAST (Static Application Security Testing)**  
  - [Semgrep](https://semgrep.dev/) para análise estática de código.  
  - Regras personalizadas em `ci/semgrep-rules/custom-rule.yml`.

- **DAST (Dynamic Application Security Testing)**  
  - [OWASP ZAP](https://www.zaproxy.org/) para varredura dinâmica em ambiente de execução.

- **SCA (Software Composition Analysis)**  
  - [OWASP Dependency-Check](https://jeremylong.github.io/DependencyCheck/) para detecção de vulnerabilidades em bibliotecas e dependências.

---

## Execução Local

### 1. SAST
```bash
chmod +x ci/run-sast.sh
./ci/run-sast.sh
Relatório gerado em docs/sast-report.json

### 2. SCA
chmod +x ci/run-sca.sh
./ci/run-sca.sh
Relatório gerado em docs/dependency-check-report.html

### 3. DAST
chmod +x ci/run-dast.sh
./ci/run-dast.sh
Relatório gerado em docs/zap-report.html

---

## Pipeline CI/CD

### CI (SAST + SCA):
Rodado a cada push e pull request para a branch main.
Gera relatórios automatizados de vulnerabilidades e dependências.

### CD (DAST):
Executado em pipeline separado.
Sobe a API em ambiente de staging e aplica o scan dinâmico com OWASP ZAP.

⚠️ **Observação: Em ambiente GitHub Actions, a execução pode falhar devido a restrições de rede e permissões de containers. 
No entanto, os relatórios foram gerados localmente e estão disponíveis no diretório docs/.

---

## Relatórios

**docs/sast-report.json → Vulnerabilidades encontradas no código.
**docs/dependency-check-report.html → Dependências e CVEs conhecidos.
**docs/zap-report.html → Relatório de varredura dinâmica.

---

## Observações

### Este trabalho foi desenvolvido no contexto acadêmico da disciplina de Cybersegurança.
O foco principal é demonstrar a integração das ferramentas no ciclo CI/CD e a produção de relatórios claros e objetivos, em vez da execução flawless em todos os ambientes.