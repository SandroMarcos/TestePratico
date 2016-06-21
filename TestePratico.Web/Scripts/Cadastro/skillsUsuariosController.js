app.controller("skillsUsuariosCadastroCtrl", ['$scope', 'CadastroSkillsUsuarioService',
    function ($scope, CadastroSkillsUsuarioService) {

        $scope.disabledExcluir = true;
        $scope.listagem = [];

        $scope.obterListagem = function () {
            var obterListagem = CadastroSkillsUsuarioService.obterListagem();
            obterListagem.then(function (emp) {
                $scope.listagem = emp.data;
            }, function () {
                alert('Dados não encontrados');
            });
        }

        $scope.obterTodos = function () {
            var obterTodos = CadastroSkillsUsuarioService.obterTodos();
            obterTodos.then(function (emp) {
                $scope.model = emp.data;
                $scope.obterListagem();
            }, function () {
                alert('Dados não encontrados');
            });
        }

        $scope.novo = function () {
            var novoAssociar = CadastroSkillsUsuarioService.novo();
            novoAssociar.then(function (emp) {
                $scope.disabledExcluir = true;
                $scope.model = emp.data;
            }, function () {
                alert('Dados não encontrados');
            });
        }

        $scope.editar = function () {
            if ($scope.model.CodUsuario > 0) {
                var editarAssociar = CadastroSkillsUsuarioService.editar($scope.model.CodUsuario);
                editarAssociar.then(function (retorno) {
                    $scope.disabledExcluir = false;
                    $scope.model = retorno.data;
                }, function () {
                    alert('Dados não encontrados');
                });
            }
        }

        $scope.salvar = function () {
            var model = $scope.model;
            model.TodosUsuarios = null;
            model.TodosSkills = null;
            model.listagem = null;

            var salvarAssociar = CadastroSkillsUsuarioService.salvar(model);
            salvarAssociar.then(function (retorno) {
                if (retorno.data.ok) {
                    $scope.disabledExcluir = true;
                    $scope.obterTodos();
                    alert('Associação salva com sucesso');
                }
                else if (retorno.data.message) {
                    alert('Erro ao salvar Associação' + retorno.data.message);
                }

            }, function () {
                alert('Erro ao salvar Associação');
            });
        }

        $scope.excluir = function () {
            if ($scope.model.CodUsuario > 0) {
                var excluirAssociar = CadastroSkillsUsuarioService.excluir($scope.model.CodUsuario);
                excluirAssociar.then(function (retorno) {
                    if (retorno.data.ok) {
                        $scope.disabledExcluir = true;
                        $scope.obterTodos();
                        alert('Associação excluído com sucesso');
                    }
                    else if (retorno.data.alerta) {
                        alert(retorno.data.message);
                    }
                    else if (retorno.data.message) {
                        alert('Erro ao excluir Associação:\n ' + retorno.data.message);
                    }

                }, function () {
                    alert('Erro ao salvar Associação');
                });
            }
        }

        $scope.isDisabled = function () {
            return $scope.disabledExcluir;
        }

    } ]);
