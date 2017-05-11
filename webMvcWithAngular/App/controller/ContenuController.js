app.controller('ContenuController', function ($scope, $http, $routeParams, $window, $location) {
    var id = $routeParams.id;
    $scope.message = "";
    $scope.result = "color-default";
    $scope.contenu = { ContenuID: 0, Titre: "", Description: "", ProjectID: 0 };

    if (id) getContenuData();

    //******=========Get All=========******
    function getContenuData() {
        $http({
            method: 'GET',
            url: '/Contenu/GetContenubyId?id=' + id
        }).then(function (result) {
            $scope.contenu = result.data;
        }, function (error) {
            $scope.message = error.statusText;
            console.log($scope.message);
        });
        
    };

    //******=========Edit=========******
    $scope.validate = function () {
        $http({
            method: 'POST',
            url: '/Contenu/Save',
            data: $scope.contenu
        })
        .then(function (result) {
            $window.location.href = '/Contenu/Index';
        }, function (error) {
            $scope.message = error.statusText;
            $scope.result = "color-red";
            console.log($scope.message);
        });
    };



    $scope.cancel = function () {
        $window.location.href = '/Contenu/Index';
    };
})
