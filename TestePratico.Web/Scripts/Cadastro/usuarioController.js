app.controller("usuarioCadastroCtrl", ['$scope', 'CadastroUsuarioService',
    function ($scope, CadastroUsuarioService) {

        $scope.disabledExcluir = true;

        $scope.obterTodos = function () {
            var obterTodos = CadastroUsuarioService.obterTodos();
            obterTodos.then(function (emp) {
                $scope.model = emp.data;
            }, function () {
                alert('Dados não encontrados');
            });
        }

        $scope.novo = function () {
            var novoUsuario = CadastroUsuarioService.novo();
            novoUsuario.then(function (emp) {
                $scope.disabledExcluir = true;
                $scope.model = emp.data;
            }, function () {
                alert('Dados não encontrados');
            });
        }

        $scope.editar = function (codigo) {
            var editarUsuario = CadastroUsuarioService.editar(codigo);
            editarUsuario.then(function (retorno) {
                $scope.disabledExcluir = false;
                $scope.model = retorno.data;
            }, function () {
                alert('Dados não encontrados');
            });
        }

        $scope.salvar = function () {
            var salvarUsuario = CadastroUsuarioService.salvar($scope.model);
            salvarUsuario.then(function (retorno) {
                if (retorno.data.ok) {
                    $scope.disabledExcluir = true;
                    $scope.obterTodos();
                    alert('Usuario salvo com sucesso');
                }
                else if (retorno.data.message) {
                    alert('Erro ao salvar Usuario' + retorno.data.message);
                }

            }, function () {
                alert('Erro ao salvar Usuario');
            });
        }

        $scope.excluir = function () {
            if ($scope.model.Id > 0) {
                var excluirUsuario = CadastroUsuarioService.excluir($scope.model.Id);
                excluirUsuario.then(function (retorno) {
                    if (retorno.data.ok) {
                        $scope.disabledExcluir = true;
                        $scope.obterTodos();
                        alert('Usuario excluído com sucesso');
                    }
                    else if (retorno.data.alerta) {
                        alert(retorno.data.message);
                    }
                    else if (retorno.data.message) {
                        alert('Erro ao excluir Usuario:\n ' + retorno.data.message);
                    }

                }, function () {
                    alert('Erro ao salvar Usuario');
                });
            }
        }

        $scope.isDisabled = function () {
            return $scope.disabledExcluir;
        }

    } ]);
