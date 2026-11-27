# 📚 Sistema de Gerenciamento de Livraria

Projeto avaliativo desenvolvido em equipe utilizando **ASP.NET Core MVC** e **MySQL** para o gerenciamento de acervo de livros e autenticação de usuários.

---

## 🚀 Funcionalidades

* **Autenticação de Usuários:** Cadastro e login de contas com proteção de rotas via Cookies.
* **Gerenciamento de Livros (CRUD):**
  * Listagem completa de livros cadastrados (ISBN, Título, Descrição e Valor).
  * Cadastro de novos livros no sistema.
  * Validação de regra de negócio para impedir o cadastro de **ISBN duplicado**.
* **Navegação Integrada:** Tela inicial (`Home`) conectando rapidamente os módulos de conta e acervo.

---

## 🛠️ Tecnologias Utilizadas

* **Back-end:** C# / .NET (ASP.NET Core MVC)
* **Front-end:** HTML5, CSS3, Bootstrap e Razor Views
* **Banco de Dados:** MySQL (`MySql.Data`)
* **Arquitetura:** Padrão Repository e Injeção de Dependência (`AddScoped`)
* **Versionamento:** Git e GitHub (Commits Semânticos)

---

## ⚙️ Como executar o projeto

1. **Clone o repositório:**
   ```bash
   git clone [https://github.com/Trabalho-Avaliativo/Livraria.git](https://github.com/Trabalho-Avaliativo/Livraria.git)
