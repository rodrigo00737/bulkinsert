## Instalação
Instalar a ultima versão do Postgres database.

## Utilização

Criar o data base usando a sequinte connection string:

```connection
Host=localhost;Username=postgres;Password=postgres;Database=exchange 
```
Criar a tabela exchange usando o script da migrations

```
 CREATE TABLE exchange (
	id int4 NULL,
	"data" timestamp NULL,
	tipooperacao varchar NULL,
	ativo varchar NULL,
	quantidade int4 NULL,
	preco numeric NULL,
	conta int4 NULL
);
```
## Resumo

Este projeto utiliza Asp.net 6 e exemplifica a gravação de dados usando ado de forma simples e via bulk insert.
