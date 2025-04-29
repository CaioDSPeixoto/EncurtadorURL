# Encurtador de URL

Este � um Encurtador de URL simples desenvolvido em ASP.NET Core Razor Pages, que permite ao usu�rio encurtar URLs com op��es de expira��o. A expira��o pode ser configurada para 1 hora, 1 dia, 3 dias. Para usu�rios premium, o sistema oferece a possibilidade de expira��o de 7 dias, 1 m�s ou nunca expirar.

---

## Funcionalidades

- **Encurtar URL**: O usu�rio pode inserir uma URL longa e gerar uma URL curta.
- **Pagamento**: Quando uma op��o premium � escolhida, um modal � gerado como parte do fluxo de pagamento podendo ser preenchido com um qrcode de pagamento.
- **Armazenamento no MongoDB**: As URLs curtas e longas s�o armazenadas no MongoDB, com suporte para expira��o autom�tica usando um �ndice TTL.

---

## Requisitos para Rodar a Aplica��o

### Pr�-requisitos

- **.NET 9 SDK**: A aplica��o foi desenvolvida utilizando o .NET 9. Caso n�o tenha o .NET 9 instalado, voc� pode baix�-lo [aqui](https://dotnet.microsoft.com/).
- **MongoDB**: O MongoDB � usado para armazenar os dados da URL encurtada. Voc� pode rodar o MongoDB localmente ou utilizar um servi�o de banco de dados em nuvem como o MongoDB Atlas.
- **Docker (Opcional)**: Caso deseje rodar a aplica��o e o MongoDB em cont�ineres Docker, o suporte para Docker est� incluso.

### Configura��o

- **Alterar o `appsettings.json`**: Caso utilize um dom�nio personalizado ou altere o banco de dados, n�o se esque�a de ajustar as configura��es no arquivo `appsettings.json` ou `appsettings.Development.json` dependendo do seu env.

---

## MongoDB

- O MongoDB deve estar rodando na porta `27017` (padr�o).
- Caso queira utilizar um MongoDB remoto ou alterado, modifique a string de conex�o no arquivo `appsettings` para refletir a URL do seu MongoDB.

### Como Rodar o MongoDB com Docker

Se preferir rodar o MongoDB com Docker, pode usar o comando abaixo para criar e rodar um cont�iner MongoDB:

```bash
docker run --name mongodb -d -p 27017:27017 mongo:latest
```

---

## Como Usar

### Encurtar uma URL

1. Acesse a p�gina inicial da aplica��o.
2. Insira a URL longa no campo de entrada.
3. Selecione o tipo de expira��o para o link.
4. Clique em **"Encurtar"** para gerar a URL curta.

### Visualizar a URL Encurtada

- Ap�s encurtar a URL, voc� ver� a URL curta gerada.
- Voc� pode copiar e acessar a URL gerada diretamente.

### Para Usu�rios Premium

1. Ao selecionar op��es de expira��o premium (**7 dias**, **1 m�s**, **nunca**), voc� ser� redirecionado a um modal que simula o pagamento fict�cio.
2. Ap�s a valida��o de pagamento, o link gerado ser� v�lido conforme a op��o de expira��o escolhida.

---

## Redirecionamento

- Caso a URL encurtada expire ou n�o exista, a aplica��o exibir� uma p�gina **404**.

---

## Contribuindo

1. Fork o reposit�rio.
2. Crie uma branch com sua feature:
3. Fa�a suas altera��es e commit:
4. Fa�a o push para a branch:
5. Abra um pull request.