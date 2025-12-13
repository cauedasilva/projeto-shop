# Projeto-Shop

Um sistema simples de gestão de loja, frontend em Angular e backend em .NET + SQL Server com funcionalidades de CRUD para estoque (produtos) e clientes.

## O que é

O Projeto-Shop é uma aplicação full-stack para gerenciamento de inventário e clientes.  
Permite cadastrar, editar, listar e deletar produtos e clientes através de uma interface web (Angular) consumindo uma API REST (ASP.NET).

## Funcionalidades principais

- CRUD de Produtos (Estoque)  
  - Adicionar novo produto (ProductId, nome, quantidade disponível, ponto de reposição)  
  - Listar todos os produtos  
  - Atualizar dados de um produto existente  
  - Remover um produto  

- CRUD de Clientes  
  - Adicionar novo cliente (CustomerId, nome, sobrenome, email, telefone, data de cadastro)  
  - Listar todos os clientes  
  - Atualizar dados do cliente  
  - Remover cliente  

- API REST com endpoints para “/inventory” e “/customer”  
- Frontend com Angular: interface para navegação, cadastro, edição e deleção.  

## Tecnologias utilizadas

- **Backend**: ASP.NET Core, C#, Microsoft.Data.SqlClient / System.Data.SqlClient, SQL Server   
- **Frontend**: Angular, TypeScript, Bootstrap (para o layout e responsividade)  
- **Comunicação**: JSON via HTTP, com chamadas GET / POST / PUT / DELETE  
- **Banco de dados**: SQL Server (tabelas de Inventory e Customer)  
