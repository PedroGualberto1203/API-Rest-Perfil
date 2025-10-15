# API REST para Perfil Acessórios

![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet) ![Entity Framework Core](https://img.shields.io/badge/EF%20Core-8.0-blue) ![SQL Server](https://img.shields.io/badge/SQL%20Server-blue) ![Status](https://img.shields.io/badge/status-em%20desenvolvimento-brightgreen)

## 📖 Sobre o Projeto

Esta é uma API RESTful desenvolvida em ASP.NET Core 8 como back-end para um sistema de e-commerce e gerenciamento de perfil de usuários. O projeto foi construído do zero, desde a modelagem do banco de dados relacional até a implementação de regras de negócio complexas, autenticação, autorização e documentação.

O objetivo principal foi aplicar e solidificar conhecimentos em arquitetura de APIs, boas práticas de desenvolvimento e segurança, criando um projeto de portfólio robusto, bem documentado e pronto para ser executado.

## ✨ Funcionalidades

A API conta com um conjunto completo de funcionalidades para um sistema de gerenciamento:

* 🔐 **Autenticação e Autorização:** Sistema seguro baseado em Tokens JWT com autorização granular baseada em Permissões (Roles/Claims).
* 👤 **Gerenciamento de Usuários:** CRUD completo de usuários, com sistema de saldo e permissões. O registro de novos usuários é uma ação restrita a administradores.
* 📦 **Gerenciamento de Catálogo:** CRUD de Produtos e Categorias.
* 🛒 **Sistema de Vendas:** Lógica transacional complexa para processamento de compras, garantindo a atomicidade das operações com validação de estoque e saldo do cliente.
* 📄 **Documentação Interativa:** Geração automática de documentação da API com Swagger (OpenAPI) para facilitar os testes e o consumo dos endpoints.

## 🚀 Tecnologias Utilizadas

Este projeto foi construído com um stack de tecnologias moderno e robusto do ecossistema .NET:

* **.NET 8**
* **ASP.NET Core** para a construção da API REST.
* **Entity Framework Core 8** como ORM, utilizando a abordagem **Code-First** com **Fluent API** e **Data Seeding**.
* **SQL Server** como banco de dados relacional.
* **Autenticação JWT Bearer** para segurança dos endpoints.
* **SecureIdentity** para geração e verificação de hashes de senha.
* **Swashbuckle** para a documentação via Swagger.

## 🏛️ Arquitetura e Padrões

Durante o desenvolvimento, diversos padrões e conceitos de arquitetura foram aplicados para garantir um código limpo, seguro e manutenível:

* **Padrão de ViewModels/DTOs (Data Transfer Objects):** Para criar um contrato claro com o cliente da API, evitar o vazamento de dados sensíveis (como `SenhaHash`) e prevenir erros de referência circular na serialização.
* **Injeção de Dependência (Dependency Injection):** Utilizada extensivamente para desacoplar as camadas da aplicação.
* **Tratamento de Erros Robusto:** Estrutura de `try-catch` para lidar com exceções, retornando códigos de status HTTP apropriados (`400`, `403`, `404`, `500`).
* **Validação Customizada:** Implementação de `ValidationAttribute` customizado para regras de negócio específicas.

## ▶️ Como Executar e Testar Localmente

Siga os passos abaixo para clonar, configurar e executar a API em seu ambiente de desenvolvimento.

### Pré-requisitos

* **[.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)**
* **[SQL Server Express](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)** ou **[Developer Edition](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)**.
* Um cliente de API como **[Postman](https://www.postman.com/downloads/)** (opcional, pois o Swagger já está configurado).
* **[Git](https://git-scm.com/downloads)**.

### Passo a Passo da Instalação e Execução

**1. Clone o Repositório:**
```sh
git clone [https://github.com/PedroGualberto1203/API-Rest-Perfil.git](https://github.com/PedroGualberto1203/API-Rest-Perfil.git)
cd API-Rest-Perfil
```

**2. Configure a Conexão com o Banco (`appsettings.Development.json`):**
Por segurança, este arquivo não está no repositório. Você precisa criá-lo na raiz do projeto.

* Crie um arquivo chamado `appsettings.Development.json`.
* Copie e cole o conteúdo abaixo e **altere a `DefaultConnection`** para apontar para a sua instância local do SQL Server.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ApiPerfilDB;Integrated Security=True;TrustServerCertificate=True"
  },
  "JwtKey": "ZGhpZWd1aW5ob2RhbHZlc2VxdWVyYWx2ZXNkaWVndWlub2RhbHZl"
}
```
*(Nota: `Integrated Security=True` funciona para o SQL Server Express padrão no Windows. Se você usa autenticação com usuário e senha, ajuste a string para: `Server=SUA_INSTANCIA;Database=ApiPerfilDB;User ID=SEU_USUARIO;Password=SUA_SENHA;TrustServerCertificate=True`)*

**3. Crie e Popule o Banco de Dados (Passo Mágico):**
Este comando único fará tudo por você: criará o banco de dados `ApiPerfilDB` (se não existir), executará todas as migrações para criar as tabelas e relacionamentos, e **populará o banco com os dados essenciais**, incluindo as permissões padrão e um **usuário Administrador pronto para testes**.
```sh
dotnet ef database update
```

**4. Execute a Aplicação:**
```sh
dotnet run
```
A API estará rodando nos endereços indicados no terminal (geralmente `http://localhost:5126` e `https://localhost:7074`).

### Testando a API com o Swagger

Com a API rodando, a forma mais fácil de testar é pelo Swagger.

1.  **Acesse a Documentação:**
    Abra seu navegador e vá para: **http://localhost:5126/swagger**

2.  **Faça o Login com a Conta Admin Padrão:**
    O banco de dados já foi criado com um usuário Administrador para testes. Use as seguintes credenciais:
    * **Email:** `admin@email.com`
    * **Password:** `Admin@123`

    No Swagger, vá para o endpoint `POST /v1/accounts/login`, clique em "Try it out", use as credenciais acima e clique em "Execute".

3.  **Copie o Token JWT:**
    A resposta do login conterá seu token. Copie a string completa do token (sem as aspas).

4.  **Autorize o Swagger:**
    * No topo da página, clique no botão verde **"Authorize"**.
    * Na janela que abrir, digite `Bearer ` (a palavra "Bearer" e um espaço) e cole o seu token.
    * Clique no botão **"Authorize"** na janela e depois em "Close".

5.  **Explore a API!**
    Pronto! Agora você está autenticado como Admin e pode testar **todos** os endpoints da API, incluindo o registro de novos usuários, criação de produtos, etc.

## Endpoints Principais

| Verbo | Rota | Descrição | Protegido? |
| :--- | :--- | :--- | :--- |
| `POST` | `/v1/accounts/` | Registra um novo usuário. | **Sim (Admin)** |
| `POST` | `/v1/accounts/login` | Autentica um usuário e retorna um token JWT. | Não |
| `GET` | `/v1/usuarios` | Lista todos os usuários. | Sim (`[Authorize]`) |
| `DELETE` | `/v1/usuarios/{id}` | Deleta um usuário específico. | **Sim (Admin)** |
| `GET` | `/v1/produtos` | Lista todos os produtos. | Sim (`[Authorize]`) |
| `POST`| `/v1/produtos/create` | Cria um novo produto. | Sim (`Admin`) |
| `PUT` | `/v1/produtos/edit/{id}` | Edita um produto existente. | Sim (`Admin`) |
| `POST` | `/v1/venda/create` | Processa uma nova venda/compra. | Sim (`[Authorize]`) |

---

## 📝 Autor

**Pedro Gualberto**

* **LinkedIn:** `https://www.linkedin.com/in/seu-perfil/`
* **GitHub:** `https://github.com/PedroGualberto1203`
