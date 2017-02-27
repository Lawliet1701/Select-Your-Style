styleApp.controller("LoginCtrl", function ($scope, $localStorage) {

    $scope.saveLogin = function () {
        $localStorage.login = $scope.login;
    }

});