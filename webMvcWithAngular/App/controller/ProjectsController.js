app.controller('ProjectsController', 
function ($scope, 
$http, 
$location, $window) {
    $scope.message = '';
    $scope.result = "color-red";
    $scope.projects = null;
    getallData();

    //******=========Get All=========******
    function getallData() {
       $http({
            method: 'GET',
            url: '/Project/GetAllData'
        }).then(function (result) {
            $scope.projects = result.data;
        }, function (error) {
            $scope.message = 'Unexpected Error while loading data!!';
            $scope.result = "color-red";
            console.log($scope.message);
        });
    };

    $scope.add = function() {
        $window.location.href = '/Project/ProjectDetail';
    };


    $scope.edit = function (item) {
        if (!item) return;
        $window.location.href = '/Project/ProjectDetail?id=' + item.ProjectId;
    };

   
    


    //*******=====Delete====******
    $scope.delete = function (item) {
        $http({
            method: 'POST',
            url: '/Project/Delete?id=' + item.ProjectId,
            //data: $scope.project
        })
        .then(function (result) {
            $window.location.href = '/Project/Index';
        }, function (error) {
            $scope.message = error.statusText;
            $scope.result = "color-red";
            console.log($scope.message);
        });


    };

})
