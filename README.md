# WebApi de Catalogo

Bem-vindo à minha Web API! Este projeto foi desenvolvido utilizando .NET 8 e o MySQL Workbench como banco de dados. A API possui endpoints para gerenciar catálogos e produtos, suportando todos os métodos HTTP (GET, POST, PUT, DELETE).

## Tecnologias Utilizadas

- **Linguagem:** C#
- **Framework:** .NET 8
- **Banco de Dados:** MySQL
- **Ferramenta de Banco de Dados:** MySQL Workbench

## Estrutura da API

### Catálogos

Os catálogos são coleções de produtos. Abaixo estão os endpoints disponíveis para gerenciar catálogos:

- **GET /catalogos**: Retorna a lista de todos os catálogos.
- **GET /catalogos/{id}**: Retorna os detalhes de um catálogo específico.
- **POST /catalogos**: Cria um novo catálogo.
- **PUT /catalogos/{id}**: Atualiza um catálogo existente.
- **DELETE /catalogos/{id}**: Remove um catálogo específico.
- Ainda em constante atualização, irei adicionar mais funcionalidades!

### Produtos

Os produtos são itens individuais que pertencem a um catálogo. Abaixo estão os endpoints disponíveis para gerenciar produtos:

- **GET /produtos**: Retorna a lista de todos os produtos.
- **GET /produtos/{id}**: Retorna os detalhes de um produto específico.
- **POST /produtos**: Cria um novo produto.
- **PUT /produtos/{id}**: Atualiza um produto existente.
- **DELETE /produtos/{id}**: Remove um produto específico.
- Ainda em constante atualização, irei adicionar mais funcionalidades!

## Configuração e Execução

### Pré-requisitos

- .NET 8 SDK
- MySQL Server
- MySQL Workbench

### Configuração do Banco de Dados

1. Abra o MySQL Workbench e conecte-se ao seu servidor MySQL.
2. Crie um novo banco de dados para o projeto.
3. Execute os scripts SQL fornecidos no diretório `sql` para criar as tabelas necessárias.
 
## Testes

Para testar a API, você pode usar ferramentas como Postman ou cURL para enviar requisições HTTP aos endpoints descritos acima.

## Contribuição

Se você quiser contribuir com este projeto, sinta-se à vontade para abrir issues e enviar pull requests.
 
