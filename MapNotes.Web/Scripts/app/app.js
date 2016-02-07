(function () {

    var app = angular.module("mapApp", []);

    app.service("noteService", function ($q, $http) {

        this.loadNotes = function () {
            var deferred = $q.defer();

            $http.get("/api/notes").success(function (data) {
                deferred.resolve(data);
            }).error(function (data, status) {
                deferred.reject("Error: request returned status " + status);
            });
            return deferred.promise;
        };
        
    });

    app.controller("mapController", function ($scope, $timeout, noteService) {

        $scope.isLoaded = false;
        $scope.map = null;
        $scope.notes = [];

        $scope.initialize = function () {

            var defaultLocation = new google.maps.LatLng(55.0300946066829, 82.92119800799789);

            var mapOptions = {
                zoom: 16,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };

            $scope.map = new google.maps.Map(document.getElementById("map"), mapOptions);

            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    $scope.map.setCenter(new google.maps.LatLng(position.coords.latitude, position.coords.longitude));
                }, function () {
                    $scope.map.setCenter(defaultLocation);
                });
            } else {
                $scope.map.setCenter(defaultLocation);
            }

            $scope.map.addListener("idle", function () {
                $scope.updateNotes();
            });
        }

        $scope.updateNotes = function () {
            var center = $scope.map.getCenter();

            noteService.loadNotes().then(function (data) {
                $scope.deleteNotes();

                angular.forEach(data, function (item) {

                    var note = new MarkerWithLabel({
                        map: $scope.map,
                        position: new google.maps.LatLng(item.latitude, item.longitude),
                        draggable: false,
                        raiseOnDrag: true,
                        labelContent: item.title,
                        labelAnchor: new google.maps.Point(90, 0),
                        labelClass: "infobox",
                        labelStyle: { opacity: 0.9 },
                        labelInBackground: true
                    });
                    $scope.notes.push(note);
                });
            });
        }

        $scope.deleteNotes = function () {
            if ($scope.notes.length === 0) {
                return;
            }

            for (var i = 0; i < $scope.notes.length; i++) {
                $scope.notes[i].setMap(null);
            }

            $scope.notes.length = 0;
        }

        $scope.addOnClick = function(event) {
            
        }

        $timeout(function () {
            $scope.initialize();
        }, 100);

    });

})();
