# 📚 GestaoEscolar-Backend

Este repositório contém o código-fonte do backend da aplicação **Gestão Escolar**, desenvolvido com **.NET 8 e Entity Framework Core**. Ele fornece a API REST que gerencia os dados de alunos, cursos e matrículas para o frontend.

📌 **Frontend:** [GestaoEscolar-Frontend](https://github.com/TavaresLuc/GestaoEscolar-Frontend)

---

## 🛠 Requisitos
Antes de iniciar, certifique-se de ter os seguintes requisitos instalados:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) (ou um banco de dados compatível)
- [Visual Studio](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)
- [Postman](https://www.postman.com/) (opcional, para testar as rotas)
- [Git](https://git-scm.com/)

---

## 🚀 Como Rodar o Projeto Localmente

### 1️⃣ Clone o Repositório
```sh
git clone https://github.com/TavaresLuc/GestaoEscolar-Backend.git
cd GestaoEscolar-Backend
```

### 2️⃣ Configure a String de Conexão
Abra o arquivo **`appsettings.json`** e ajuste a string de conexão do banco de dados com os seus dados correto de usuário e senha:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=BaseEscola;User Id=sa;Password=sua_senha;TrustServerCertificate=True"
}
```

---

### 3️⃣ Restaurar Dependências
No terminal, rode o comando:
```sh
dotnet restore
```

---

### 4️⃣ Aplicar Migrations
Se este for seu primeiro uso, execute:
```sh
dotnet ef database update
```
Isso irá criar as tabelas no banco de dados.

Caso precise gerar novas migrations no futuro:
```sh
dotnet ef migrations add NomeDaMigration
```

---

### 5️⃣ Executar a API
Para iniciar o servidor, rode:
```sh
dotnet run
```

A API estará rodando em `http://localhost:7194` (Certifique-se no `appsettings.json` de que está nessa porta mesmo, pois o front-end envia para essa porta 7194).

---

## 🔥 Endpoints Disponíveis
A API possui os seguintes endpoints:

#### 📍 Alunos
- `GET /api/aluno` → Lista todos os alunos
- `POST /api/aluno` → Adiciona um novo aluno
- `PUT /api/aluno/{id}` → Atualiza um aluno
- `DELETE /api/aluno/{id}` → Remove um aluno

#### 📍 Cursos
- `GET /api/curso` → Lista todos os cursos
- `POST /api/curso` → Adiciona um novo curso

#### 📍 Matrículas
- `POST /api/matricula` → Matricula um aluno em um curso

---

## ✅ Tecnologias Utilizadas
- **.NET 8.0**
- **Entity Framework Core**
- **SQL Server**
- **C#**

---

## 📜 Licença
Este projeto está sob a licença MIT.

Caso tenha dúvidas, entre em contato ou abra uma issue. 🚀
