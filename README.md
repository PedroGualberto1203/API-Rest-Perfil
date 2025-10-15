# API REST para Perfil Acessórios

![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet) ![Entity Framework Core](https://img.shields.io/badge/EF%20Core-8.0-blue) ![SQL Server on Docker](https://img.shields.io/badge/SQL%20Server-on%20Docker-blue.svg) ![Versão](https://img.shields.io/badge/versão-1.0.0-blue)

## 📖 Sobre o Projeto

Esta é uma API RESTful desenvolvida em ASP.NET Core 8 como back-end para um sistema de e-commerce e gerenciamento de perfil de usuários. O projeto foi construído do zero, desde a modelagem do banco de dados relacional até a implementação de regras de negócio complexas, autenticação, autorização e documentação.

O objetivo principal foi aplicar e solidificar conhecimentos em arquitetura de APIs, boas práticas de desenvolvimento e segurança, criando um projeto de portfóflio robusto e bem documentado.

## ✨ Funcionalidades

A API conta com um conjunto completo de funcionalidades para um sistema de gerenciamento:

* 🔐 **Autenticação e Autorização:** Sistema seguro baseado em Tokens JWT com autorização granular baseada em Permissões (Roles/Claims).
* 👤 **Gerenciamento de Usuários:** CRUD completo de usuários, com sistema de saldo e permissões. O registro de novos usuários é uma ação restrita a administradores.
* 📦 **Gerenciamento de Catálogo:** CRUD de Produtos e Categorias.
* 🛒 **Sistema de Vendas:** Lógica transacional complexa para processamento de compras, garantindo a atomicidade das operações com validação de estoque e saldo do cliente.
* 📄 **Documentação Interativa:** Geração automática de documentação da API com Swagger (OpenAPI) para facilitar os testes.

## 🚀 Tecnologias Utilizadas

Este projeto foi construído com um stack de tecnologias moderno e robusto do ecossistema .NET:

* **.NET 8**
* **ASP.NET Core** para a construção da API REST.
* **Entity Framework Core 8** como ORM, utilizando a abordagem **Code-First** com **Fluent API** e **Data Seeding**.
* **SQL Server rodando em um container Docker** como banco de dados relacional.
* **Autenticação JWT Bearer** para segurança dos endpoints.
* **SecureIdentity** para geração e verificação de hashes de senha.
* **Swashbuckle** para a documentação via Swagger.

## 🏛️ Arquitetura e Padrões

Durante o desenvolvimento, diversos padrões e conceitos de arquitetura foram aplicados para garantir um código limpo, seguro e manutenível:

* **Padrão de ViewModels/DTOs (Data Transfer Objects):** Para criar um contrato claro com o cliente da API, evitar o vazamento de dados sensíveis e prevenir erros de referência circular.
* **Injeção de Dependência (Dependency Injection):** Utilizada extensivamente para desacoplar as camadas da aplicação.
* **Tratamento de Erros Robusto:** Estrutura de `try-catch` para lidar com exceções, retornando códigos de status HTTP apropriados.
* **Validação Customizada:** Implementação de `ValidationAttribute` customizado para regras de negócio específicas.

## ▶️ Como Executar e Testar Localmente

Siga os passos abaixo para clonar, configurar e executar a API em seu ambiente de desenvolvimento.

### Pré-requisitos

* **[.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)**
* **[Docker Desktop](https://www.docker.com/products/docker-desktop/)** para executar o banco de dados SQL Server.
* Um cliente de API como **[Postman](https://www.postman.com/downloads/)** (opcional, pois o Swagger já está configurado).
* **[Git](https://git-scm.com/downloads)**.

### Passo a Passo da Instalação e Execução

**1. Clone o Repositório:**
```sh
git clone [https://github.com/PedroGualberto1203/API-Rest-Perfil.git](https://github.com/PedroGualberto1203/API-Rest-Perfil.git)
cd API-Rest-Perfil
```
**2. Inicie o Banco de Dados com Docker:**
O projeto é configurado para se conectar a uma instância do SQL Server. A forma mais fácil de rodar uma é usando Docker. Execute o comando abaixo no seu terminal (certifique-se de que o Docker Desktop está em execução):
```sh
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=SuaSenhaForte!123" -p 1433:1433 --name sqlserver-apiperfil -d [mcr.microsoft.com/mssql/server:2022-latest](https://mcr.microsoft.com/mssql/server:2022-latest)
```
*(Lembre-se de substituir `SuaSenhaForte!123` pela senha que você vai configurar no próximo passo).*

**3. Configure a Conexão com o Banco (`appsettings.Development.json`):**
Por segurança, este arquivo não está no repositório. Você precisa criá-lo na raiz do projeto.

* Crie um arquivo chamado `appsettings.Development.json`.
* Copie e cole o conteúdo abaixo e **altere a `DefaultConnection`** para apontar para a sua instância do SQL Server no Docker.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=ApiPerfilDB;User ID=sa;Password=SuaSenhaForte!123;TrustServerCertificate=True"
  },
  "JwtKey": "ZGhpZWd1aW5ob2RhbHZlc2VxdWVyYWx2ZXNkaWVndWlub2RhbHZl"
}
```
*(Importante: a `Password` aqui deve ser a mesma que você definiu no comando `docker run`).*

**4. Crie e Popule o Banco de Dados:**
Este comando irá se conectar ao seu container Docker, criar o banco de dados, aplicar todas as migrações e popular os dados iniciais essenciais, incluindo as permissões e um usuário Administrador.
```sh
dotnet ef database update
```

**5. Execute a Aplicação:**
```sh
dotnet run
```
A API estará rodando nos endereços indicados no terminal (geralmente `http://localhost:5126`).

### Testando a API com o Swagger

1.  **Acesse a Documentação:**
    Abra seu navegador e vá para: **http://localhost:5126/swagger**

2.  **Faça o Login com a Conta Admin Padrão:**
    O banco de dados já foi criado com um usuário Administrador. Use as seguintes credenciais:
    * **Email:** `admin@email.com`
    * **Password:** `Admin123`

    No Swagger, vá para o endpoint `POST /v1/accounts/login`, clique em "Try it out", use as credenciais acima e clique em "Execute".

3.  **Copie o Token JWT e Autorize o Swagger:**
    * Copie o token da resposta do login.
    * No topo da página, clique em **"Authorize"**.
    * Na janela, digite `Bearer ` e cole seu token.
    * Clique em "Authorize" e "Close".

4.  **Explore a API!**
    Agora você está autenticado como Admin e pode testar todos os endpoints.

## Endpoints Principais
*(A tabela de endpoints permanece a mesma)*

---

## 📝 Autor

**Pedro Gualberto**

* **LinkedIn:** `https://www.linkedin.com/in/seu-perfil/`
* **GitHub:** `https://github.com/PedroGualberto1203`
