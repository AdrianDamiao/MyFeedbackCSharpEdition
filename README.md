# MyFeedback C# Edition

🚧 Work in Progress 🚧

#### O projeto MyFeedback se trata do desenvolvimento de uma aplicação para avaliação anônima do trabalho colaboradores de uma empresa. A proposta do projeto surgiu durante um curso de NodeJS pelo professor ![Otávio Lube](https://github.com/otaviolube), porém, o objetivo aqui é implementá-lo utilizando outras tecnologias.

#### Diagrama de classe do projeto

![151083387-562c2eed-5cc1-4a5f-9f7c-71f08653e285](https://user-images.githubusercontent.com/86964732/152419189-26aa9b72-34d3-4fa1-af2a-8b40462f7d37.png)

#### 🎲 Criando o banco de dados:

Para criar o banco de dados utilizando o Docker é necessário executar o seguinte comando:

```bash
$ docker-compose -f .\__infra__\postgres.yml up -d
```
O comando criará dois containeres para o banco, um para testes(homolog) e o outro para produção de acordo com o arquivo `postgres.yml`. A flag `-f` especifica o caminho do arquivo e a flag `-d` serve para não perdermos o terminal e o processo ser feito em background.

Caso você não possua o docker será necessário fazer o download do [PostgreSQL](https://www.postgresql.org/download/) e criar uma conexão com as mesmas configurações do arquivo `postgres.yml`.

Após a criação do banco de dados, verifique se as **ConnectionStrings** nos arquivos `appsettings.json` e `appsettings.Development.json` estão corretas para que a aplicação possa conversar com ele.
