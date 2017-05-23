app.service("GMService", function ($http) {

    //get All Gamemachines
    this.getGamemachines = function () {
        return $http.get("Home/GetAllGamemachines");
    };

    // get Gamemachine by gamemachineId
    this.getGamemachine = function (gamemachineId) {
        var response = $http({
            method: "post",
            url: "Home/GetGamemachineById",
            params: {
                id: JSON.stringify(gamemachineId)
            }
        });
        return response;
    }

    // Update Gamemachine 
    this.updateGamemachine = function (gamemachine) {
        var response = $http({
            method: "post",
            url: "Home/UpdateGamemachine",
            data: JSON.stringify(gamemachine),
            dataType: "json"
        });
        return response;
    }

    // Add Gamemachine
    this.AddGamemachine = function (gamemachine) {
        var response = $http({
            method: "post",
            url: "Home/AddGamemachine",
            data: JSON.stringify(gamemachine),
            dataType: "json"
        });
        return response;
    }

    //Delete Gamemachine
    this.DeleteGamemachine = function (gamemachineId) {
        var response = $http({
            method: "post",
            url: "Home/DeleteGamemachine",
            params: {
                gamemachineId: JSON.stringify(gamemachineId)
            }
        });
        return response;
    }
});