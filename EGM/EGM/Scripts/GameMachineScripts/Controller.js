app.controller("GMCtrl",function ($scope, GMService) {
    $scope.divGamemachine = false;
    $scope.currentPage = 0;        
    $scope.pageSizes = [
    { 'value': '5' },
    { 'value': '10' },
    { 'value': '15' },
    { 'value': '20' }
    ];
    $scope.pageSize = '5';

    GetAllGamemachines();

    //To Get all Gamemachines  
    function GetAllGamemachines() {        
        var getGamemachineData = GMService.getGamemachines();
        getGamemachineData.then(function (gamemachine) {
            $scope.gamemachines = gamemachine.data;            
        }, function () {
            alert('Error in getting Gamemachines');
        });
    }

    $scope.editGamemachine = function (gamemachine) {
        var getGamemachineData = GMService.getGamemachine(gamemachine.Id);
        getGamemachineData.then(function (_gamemachine) {
            $scope.gamemachine = _gamemachine.data;
            $scope.gamemachineid = gamemachine.Id;
            $scope.machinename = gamemachine.MachineName;
            $scope.description = gamemachine.Description;
            $scope.machinetype = gamemachine.MachineType;
            $scope.vendor = gamemachine.Vendor;
            $scope.Action = "Update";
            $scope.divGamemachine = true;            
        }, function () {
            alert('Error in getting gamemachine details.');
        });
    }

    $scope.AddUpdateGamemachine = function () {
        var Gamemachine = {
            MachineName: $scope.machinename,
            Description: $scope.description,
            MachineType: $scope.machinetype,
            vendor: $scope.vendor
        };
        var getGamemachineAction = $scope.Action;

        if (getGamemachineAction == "Update") {
            Gamemachine.Id = $scope.gamemachineid;
            var getGamemachineData = GMService.updateGamemachine(Gamemachine);
            getGamemachineData.then(function (msg) {
                GetAllGamemachines();
                alert(msg.data);
                $scope.divGamemachine = false;                
            }, function () {
                alert('Error in updating Gamemachine');
            });
        } else {
            var getGamemachineData = GMService.AddGamemachine(Gamemachine);
            getGamemachineData.then(function (msg) {
                GetAllGamemachines();
                alert(msg.data);
                $scope.divGamemachine = false;                
            }, function () {
                alert('Error in adding Gamemachine');
            });
        }       
    }

    $scope.AddGamemachineDiv = function () {
        ClearFields();
        $scope.Action = "Add";
        $scope.divGamemachine = true;
    }

    $scope.deleteGamemachine = function (gamemachine) {
        var getGamemachineData = GMService.DeleteGamemachine(gamemachine.Id);
        getGamemachineData.then(function (msg) {
            alert(msg.data);
            GetAllGamemachines();
        }, function () {
            alert('Error in deleting Gamemachine');
        });
    }

    function ClearFields() {
        $scope.gamemachineid = "";
        $scope.machinename = "";
        $scope.description = "";
        $scope.machinetype = "";
        $scope.vendor = "";
    }
    $scope.Cancel = function () {
        $scope.divGamemachine = false;
    };

    $scope.numberOfPages = function () {
        return Math.ceil($scope.gamemachines.length / $scope.pageSize);
    };
   
});