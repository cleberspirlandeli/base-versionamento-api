# Base Backend - API com versionamento de Endpoints

Objetivo:
- Criar uma estrutura base, separando responsabilidades de negocio, infraestrutura e endpoints.
- Realizar versionamento de `endpoints`
- Autenticação com `JWT`
- Retorno padronizado e inclusive erros
- Erros gerados serão incluidos em uma lista ao em vez de gerar um `New Error`
- Documentação da API utilizando `Swagger`
- Registro de Erros e Logis utilizando `Elmah.io` e `Logger`
- Implementar `ASP.NET Identity` com permissões de acesso com `Claims` e `Roles`
- Utilizar AutoMapper
- Subir aplicação no `IIS local` e no `Azure`
- TODO - ~~Implementar Testes automatizados~~

<br/>
<br/>

## Tecnologias
- Banco de dados SQL Server
- .Net Core 3.1

## Frameworks e Packages
- EntityFramework
- Elmah.io

<br/>
<br/>

<br>

## Como executar

Criar banco de dados com o nome `MinhaAppMvcCore`

Configurar conexão com o banco de dados no arquivo `appsettings.json`


```
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=MinhaAppMvcCore;Trusted_Connection=True;"
  },
```

<br>

Executar no terminal `Package Manager Console (PM)`:
<br>

```
update-dabase -Context ApplicationDbContext --verbose
```

```
update-dabase -Context MeuDbContext --verbose
```
<br>

`Build >> Clean Solution`
<br>
`Build >> Build Solution`

<br>

![alt text](https://github.com/cleberspirlandeli/versionamento-api/blob/master/images/swagger.png)

<br>

![alt text](https://github.com/cleberspirlandeli/versionamento-api/blob/master/images/elmah.png)

<br>
