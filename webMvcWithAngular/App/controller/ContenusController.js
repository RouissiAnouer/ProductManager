app.controller('ContenusController',
function ($scope,
$http,
$location, $window) {
    $scope.selectedProject = null;
    $scope.Projects = [];
    $scope.searchText = null;
    $scope.message = '';
    $scope.result = "color-red";
    $scope.contenus = null;
    GetAllProject();
    getallData();
    //******=========Get All=========******
    function getallData() {
        $http({
            method: 'GET',
            url: '/Contenu/GetAllData'
        }).then(function (result) {
            $scope.contenus = result.data;
        }, function (error) {
            $scope.message = 'Unexpected Error while loading data!!';
            $scope.result = "color-red";
            console.log($scope.message);
        });
    };

    function GetAllProject() {
        $http({
            method: 'GET',
            url: '/Project/GetAllData'
        }).then(function (result) {
            $scope.Projects = result.data;
        }, function (error) {
            $scope.message = 'Unexpected Error while loading data!!';
            $scope.result = "color-red";
            console.log($scope.message);
        });
    }

    $scope.search = function () {
        var selectedProjectId = null;
        if ($scope.selectedProject) {
            selectedProjectId = $scope.selectedProject;
        }
        $http({
            method: 'GET',
            url: '/Contenu/GetAllData?title=' + $scope.searchText + '&projectId=' + selectedProjectId
        }).then(function (result) {
            $scope.contenus = result.data;
        }, function (error) {
            $scope.message = 'Unexpected Error while loading data!!';
            $scope.result = "color-red";
            console.log($scope.message);
        });
    }

    $scope.add = function () {
        $window.location.href = '/Contenu/ContenuDetail';
    };


    $scope.edit = function (item) {
        if (!item) return;
        $window.location.href = '/Contenu/ContenuDetail?id=' + item.ContenuId;
    };


    

    //*******=====Delete====******
    $scope.delete = function (item) {
        if(confirm('étes vous sure de vouloir supprimer ce contenu ?'))
        $http({
            method: 'POST',
            url: '/Contenu/Delete',
            data: item
        })
        .then(function (result) {
            $window.location.href = '/Contenu/Index';
        }, function (error) {
            $scope.message = error.statusText;
            $scope.result = "color-red";
            console.log($scope.message);
        });


    };

})
