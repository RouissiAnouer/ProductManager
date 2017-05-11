app.controller('ProjectController', function ($scope, $http, $routeParams, $window, $location) {
    var id = $routeParams.id;
    $scope.message = "";
    $scope.result = "color-default";
    $scope.project = {ProjectId:0,Title:"",Description:""};
    
    if (id) getProjectData();

    //******=========Get All=========******
    function getProjectData() {
       $http({
            method: 'GET',
            url: '/Project/GetProjectbyId?id=' + id
        }).then(function (result) {
            $scope.project = result.data;
        }, function (error) {
            $scope.message = error.statusText;
            console.log($scope.message);
        });
    };

    //******=========Edit=========******
    $scope.validate = function () {
        $http({
            method: 'POST',
            url: '/Project/Save',
            data: $scope.project
        })
        .then(function (result) {
             $window.location.href = '/Project/Index';
        }, function (error) {
            $scope.message = error.statusText;
            $scope.result = "color-red";
            console.log($scope.message);
        });

        
    };

    
   
    $scope.cancel = function () {
        $window.location.href = '/Project/Index';
    };
})
