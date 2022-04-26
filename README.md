# RouteManager
Projeto desenvolvido para a criação e gerenciamento de uma frota de rotas de manutenção e instalação de equipamentos. Seu fluxo após o cadastro das cidades, pessoas e equipes
envolvidas no relátorio, é a importação de uma planilha no formato xls ou xlsx, e após a filtragem dos campos o retorno de um arquivo docx com as rotas dividias igualmente para cada equipe selecionada.

# Como usar?

Ao inciar o sistema um usuário é criado de forma automática para configuração do sistema, é recomendado que sua senha e usuário sejam trocados. 
 Para acessar use:
- usuário: testemvc
- senha: testemvc



## Tecnologias usadas:

- ASP.NET Core 5.0
- Internal API Gateway com Ocelot
- JWT Bearer Authentication com tokens assimétricos
- Swagger UI com suporte ao JWT
- FluentValidator
- HealthChecks
- Relátorios com NPOI
- MongoDB

## Boas práticas de segurança
- Security Headers
- Tokens Assimétricos
- Criptografia Hash HMAC-SHA-256 para senhas 
- Hashes unicos usando um salt diferente para cada hash
- Armazenamento de Logs para auditoria do sistema
- Utilização do UUID Identificador único universal 

## Arquitetura de Software

- Conceitos SOLID e Código Limpo
- Repository pattern
- Domain Driven Design
- Domain Notification
- Domain Validations

Estilizado usando o template open source Sb-Admin 2
- https://github.com/StartBootstrap/startbootstrap-sb-admin-2

## Referências de projetos que usei como ArchType
- https://github.com/dotnet-architecture/eShopOnContainers/
- https://github.com/desenvolvedor-io/dev-store/
