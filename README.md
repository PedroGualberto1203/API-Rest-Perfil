# API REST para Perfil Acessórios

![.NET](https://img.shields.io/badge/.NET-6.0-blueviolet) ![Entity Framework Core](https://img.shields.io/badge/EF%20Core-6.0-blue) ![SQL Server](https://img.shields.io/badge/SQL%20Server-blue) ![Status](https://img.shields.io/badge/status-online-success)

## 📖 Sobre o Projeto

Esta é uma API RESTful desenvolvida em ASP.NET Core 6 como back-end para um sistema de e-commerce e gerenciamento de perfil de usuários. O projeto foi construído do zero, desde a modelagem do banco de dados relacional até a implementação de regras de negócio complexas, autenticação, autorização e a publicação final no Microsoft Azure.

O objetivo foi criar uma API robusta, segura e documentada, aplicando as melhores práticas do mercado para desenvolvimento back-end.

## ✨ Funcionalidades

A API conta com um conjunto completo de funcionalidades para um sistema de gerenciamento:

* 🔐 **Autenticação e Autorização:** Sistema seguro baseado em Tokens JWT com autorização granular baseada em Permissões (Roles/Claims).
* 👤 **Gerenciamento de Usuários:** CRUD completo de usuários, com sistema de saldo e permissões.
* 📦 **Gerenciamento de Catálogo:** CRUD de Produtos e Categorias.
* 🛒 **Sistema de Vendas:** Lógica transacional complexa para processamento de compras, com validação de estoque e saldo do cliente.
* 📄 **Documentação Interativa:** Geração automática de documentação da API com Swagger (OpenAPI).

## 🚀 Tecnologias Utilizadas

Este projeto foi construído com um stack de tecnologias moderno e robusto do ecossistema .NET:

* **.NET 6**
* **ASP.NET Core** para a construção da API REST.
* **Entity Framework Core 6** como ORM, utilizando a abordagem **Code-First** com **Fluent API**.
* **SQL Server** (hospedado no Azure SQL) como banco de dados relacional.
* **Autenticação JWT Bearer** para segurança dos endpoints.
* **Swashbuckle** para a documentação via Swagger.
* **Microsoft Azure** para publicação (App Service & SQL Database).

## 🏛️ Arquitetura e Padrões

Durante o desenvolvimento, diversos padrões e conceitos de arquitetura foram aplicados para garantir um código limpo, seguro e manutenível:

* **Padrão de ViewModels/DTOs (Data Transfer Objects):** Para criar um contrato claro com o cliente da API, evitar o vazamento de dados sensíveis (como `SenhaHash`) e prevenir erros de referência circular.
* **Injeção de Dependência (Dependency Injection):** Utilizada extensivamente para desacoplar as camadas da aplicação.
* **Tratamento de Erros Robusto:** Estrutura de `try-catch` para lidar com diferentes tipos de exceções, retornando códigos de status HTTP apropriados (`400`, `403`, `404`, `500`).
* **Validação Customizada:** Implementação de `ValidationAttribute` customizado para regras de negócio específicas.

## 🧪 Testando a API Online

A API está publicada e pronta para ser testada! A maneira mais fácil de interagir com os endpoints é através da documentação interativa do Swagger.

* **URL Base da API:** `http://localhost:5126` *(Substitua pela sua URL do Azure)*
* **Documentação Interativa (Swagger):** `http://localhost:5126/swagger` *(Substitua pela sua URL do Azure)*

### Conta de Teste Disponível

**Nota:** O endpoint de registro de novos usuários (`POST /v1/accounts`) agora é restrito a administradores para fins de segurança. Por favor, utilize a conta de teste abaixo para se autenticar e interagir com a API.

* **Email:** `teste@email.com`
* **Password:** `123456`

### Guia Rápido de Teste

1.  **Acesse a Documentação:** Abra o link do **Swagger** no seu navegador.

2.  **Faça o Login para Obter o Token:**
    * Encontre a seção `Account`, expanda o endpoint `POST /v1/accounts/login`.
    * Clique no botão **"Try it out"**.
    * Preencha o corpo da requisição com as credenciais da conta de teste fornecida acima.
    * Clique no botão azul **"Execute"**.
    * A resposta de sucesso conterá seu **token JWT**. Copie a string completa do token (sem as aspas).

3.  **Autorize suas Requisições no Swagger:**
    * No topo da página do Swagger, clique no botão verde **"Authorize"**.
    * Na janela que abrir, no campo "Value", digite `Bearer ` (a palavra "Bearer", um espaço) e cole o seu token na frente.
    * Exemplo: `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`
    * Clique no botão **"Authorize"** na janela e depois em "Close".

4.  **Teste os Endpoints Protegidos:**
    * Pronto! Agora você está autenticado. Vá para qualquer endpoint com um cadeado (🔒), como `GET /v1/produtos`, clique em "Try it out" e "Execute". A requisição agora funcionará.

## Endpoints Principais

| Verbo | Rota | Descrição | Protegido? |
| :--- | :--- | :--- | :--- |
| `POST` | `/v1/accounts/` | Registra um novo usuário. | **Sim (Admin)** |
| `POST` | `/v1/accounts/login` | Autentica um usuário e retorna um token JWT. | Não |
| `GET` | `/v1/usuarios` | Lista todos os usuários. | Sim (`[Authorize]`) |
| `DELETE` | `/v1/usuarios/{id}` | Deleta um usuário específico. | **Sim (Admin)** |
| `GET` | `/v1/produtos` | Lista todos os produtos. | Sim (`[Authorize]`) |
| `POST`| `/v1/produtos/create` | Cria um novo produto. | Sim (`Admin`, `Gerente...`) |
| `PUT` | `/v1/produtos/edit/{id}` | Edita um produto existente. | Sim (`Admin`, `Gerente...`) |
| `POST` | `/v1/venda/create` | Processa uma nova venda/compra. | Sim (`[Authorize]`) |

---

## 📝 Autor

**Pedro Gualberto**

* [LinkedIn](https://www.linkedin.com/in/seu-perfil/)
* [GitHub](https://github.com/PedroGualberto1203)
