# Encurtador de URL

Este é um Encurtador de URL simples desenvolvido em ASP.NET Core Razor Pages, que permite ao usuário encurtar URLs com opções de expiração. A expiração pode ser configurada para 1 hora, 1 dia, 3 dias. Para usuários premium, o sistema oferece a possibilidade de expiração de 7 dias, 1 mês ou nunca expirar.

---

## Funcionalidades

- **Encurtar URL**: O usuário pode inserir uma URL longa e gerar uma URL curta.
- **Pagamento**: Quando uma opção premium é escolhida, um modal é gerado como parte do fluxo de pagamento podendo ser preenchido com um qrcode de pagamento.
- **Armazenamento no MongoDB**: As URLs curtas e longas são armazenadas no MongoDB, com suporte para expiração automática usando um índice TTL.

---

## Requisitos para Rodar a Aplicação

### Pré-requisitos

- **.NET 9 SDK**: A aplicação foi desenvolvida utilizando o .NET 9. Caso não tenha o .NET 9 instalado, você pode baixá-lo [aqui](https://dotnet.microsoft.com/).
- **MongoDB**: O MongoDB é usado para armazenar os dados da URL encurtada. Você pode rodar o MongoDB localmente ou utilizar um serviço de banco de dados em nuvem como o MongoDB Atlas.
- **Docker (Opcional)**: Caso deseje rodar a aplicação e o MongoDB em contêineres Docker, o suporte para Docker está incluso.

### Configuração

- **Alterar o `appsettings.json`**: Caso utilize um domínio personalizado ou altere o banco de dados, não se esqueça de ajustar as configurações no arquivo `appsettings.json` ou `appsettings.Development.json` dependendo do seu env.

---

## MongoDB

- O MongoDB deve estar rodando na porta `27017` (padrão).
- Caso queira utilizar um MongoDB remoto ou alterado, modifique a string de conexão no arquivo `appsettings` para refletir a URL do seu MongoDB.

### Como Rodar o MongoDB com Docker

Se preferir rodar o MongoDB com Docker, pode usar o comando abaixo para criar e rodar um contêiner MongoDB:

```bash
docker run --name mongodb -d -p 27017:27017 mongo:latest
```

---

## Como Usar

### Encurtar uma URL

1. Acesse a página inicial da aplicação.
2. Insira a URL longa no campo de entrada.
3. Selecione o tipo de expiração para o link.
4. Clique em **"Encurtar"** para gerar a URL curta.

### Visualizar a URL Encurtada

- Após encurtar a URL, você verá a URL curta gerada.
- Você pode copiar e acessar a URL gerada diretamente.

### Para Usuários Premium

1. Ao selecionar opções de expiração premium (**7 dias**, **1 mês**, **nunca**), você será redirecionado a um modal que simula o pagamento fictício.
2. Após a validação de pagamento, o link gerado será válido conforme a opção de expiração escolhida.

---

## Redirecionamento

- Caso a URL encurtada expire ou não exista, a aplicação exibirá uma página **404**.

---

## Contribuindo

1. Fork o repositório.
2. Crie uma branch com sua feature:
3. Faça suas alterações e commit:
4. Faça o push para a branch:
5. Abra um pull request.