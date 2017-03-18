styleApp.controller("StyleCtrl", function ($scope, $http, $localStorage) {

    $scope.style = "";
    $scope.login = $localStorage.login || "USER";

    loadHttpImages();

    $scope.next = function () {

        if ($scope.step < 10) {

            loadHttpImages();

        } else {

            $http.get("/Home/CheckResult",
              {
                  headers: {
                      'X-Requested-With': 'XMLHttpRequest'
                  },
                  params: {
                      selectedStyle: $scope.style
                  }
              }).then(function (response) {
                  if (response.data.hasOwnProperty('step')) {
                      $scope.step = response.data["step"];
                      $scope.image1 = response.data["Images"][0];
                      $scope.image2 = response.data["Images"][1];
                  }
                  else if (response.data.hasOwnProperty('Action')) {
                      window.location.href = $scope.requestUrl;
                  }
                  
              });

        }

    }

    $scope.setStyle = function (t) {
        if (t == "left") {
            $scope.style = $scope.image1.style;
        }
        else {
            $scope.style = $scope.image2.style;
        }
    }

    function loadHttpImages() {
        $http.get("/Home/GetImages",
        {
            headers: {
                'X-Requested-With': 'XMLHttpRequest'
            },
            params: {
                selectedStyle: $scope.style
            }
        }).then(function (response) {
            $scope.step = response.data["step"];
            $scope.image1 = response.data["Images"][0];
            $scope.image2 = response.data["Images"][1];
            $scope.style = $scope.image1.style;
            $scope.tempStyle = $scope.image2.style;
        });
    }
    

    


});