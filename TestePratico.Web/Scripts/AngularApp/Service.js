// Serviços para o Cadastro de Usuarios
app.service("CadastroUsuarioService", function ($http) {
    this.obterTodos = function () {
        return $http.get("/Cadastro/Usuario/ObterTodos");
    };

    this.novo = function () {
        return $http.get("/Cadastro/Usuario/Novo");
    };

    this.editar = function (codigo) {
        return $http.get("/Cadastro/Usuario/Obter/" + codigo);
    };

    this.excluir = function (codigo) {
        return $http.get("/Cadastro/Usuario/Excluir/" + codigo);
    };

    this.salvar = function (model) {
        return $http.post("/Cadastro/Usuario/Salvar", model);
    };

});

// Serviços para o Cadastro de Skills
app.service("CadastroSkillsService", function ($http) {
    this.obterTodos = function () {
        return $http.get("/Cadastro/Skill/ObterTodos");
    };

    this.novo = function () {
        return $http.get("/Cadastro/Skill/Novo");
    };

    this.editar = function (codigo) {
        return $http.get("/Cadastro/Skill/Obter/" + codigo);
    };

    this.excluir = function (codigo) {
        return $http.get("/Cadastro/Skill/Excluir/" + codigo);
    };

    this.salvar = function (model) {
        return $http.post("/Cadastro/Skill/Salvar", model);
    };

});


// Serviços para o Cadastro de Skills
app.service("CadastroSkillsUsuarioService", function ($http) {
    this.obterTodos = function () {
        return $http.get("/Cadastro/SkillsUsuarios/ObterTodos");
    };

    this.obterListagem = function () {
        return $http.get("/Cadastro/SkillsUsuarios/ObterListagem");
    };

    this.novo = function () {
        return $http.get("/Cadastro/SkillsUsuarios/Novo");
    };

    this.editar = function (codigo) {
        return $http.get("/Cadastro/SkillsUsuarios/Obter/" + codigo);
    };

    this.excluir = function (codigo) {
        return $http.get("/Cadastro/SkillsUsuarios/Excluir/" + codigo);
    };

    this.salvar = function (model) {
        return $http.post("/Cadastro/SkillsUsuarios/Salvar", model);
    };

});
