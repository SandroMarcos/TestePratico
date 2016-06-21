app.controller("skillCadastroCtrl", ['$scope', 'CadastroSkillsService',
    function ($scope, CadastroSkillsService) {

        $scope.disabledExcluir = true;

        $scope.obterTodos = function () {
            var obterTodos = CadastroSkillsService.obterTodos();
            obterTodos.then(function (emp) {
                $scope.model = emp.data;
            }, function () {
                alert('Dados não encontrados');
            });
        }

        $scope.novo = function () {
            var novoBanco = CadastroSkillsService.novo();
            novoBanco.then(function (emp) {
                $scope.disabledExcluir = true;
                $scope.model = emp.data;
            }, function () {
                alert('Dados não encontrados');
            });
        }

        $scope.editar = function (codigo) {
            var editarBanco = CadastroSkillsService.editar(codigo);
            editarBanco.then(function (retorno) {
                $scope.disabledExcluir = false;
                $scope.model = retorno.data;
            }, function () {
                alert('Dados não encontrados');
            });
        }

        $scope.salvar = function () {
            var salvarBanco = CadastroSkillsService.salvar($scope.model);
            salvarBanco.then(function (retorno) {
                if (retorno.data.ok) {
                    $scope.disabledExcluir = true;
                    $scope.obterTodos();
                    alert('Skill salvo com sucesso');
                }
                else if (retorno.data.message) {
                    alert('Erro ao salvar skill' + retorno.data.message);
                }

            }, function () {
                alert('Erro ao salvar skill');
            });
        }

        $scope.excluir = function () {
            if ($scope.model.Id > 0) {
                var excluirBanco = CadastroSkillsService.excluir($scope.model.Id);
                excluirBanco.then(function (retorno) {
                    if (retorno.data.ok) {
                        $scope.disabledExcluir = true;
                        $scope.obterTodos();
                        alert('Skill excluído com sucesso');
                    }
                    else if (retorno.data.alerta) {
                        alert(retorno.data.message);
                    }
                    else if (retorno.data.message) {
                        alert('Erro ao excluir skill:\n ' + retorno.data.message);
                    }

                }, function () {
                    alert('Erro ao salvar skill');
                });
            }
        }

        $scope.isDisabled = function () {
            return $scope.disabledExcluir;
        }

    } ]);
