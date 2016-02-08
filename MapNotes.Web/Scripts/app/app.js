(function () {

    var app = angular.module("mapApp", []);

    app.service("noteService", function ($q, $http) {

        var config = { headers: { 'Content-Type': "application/x-www-form-urlencoded" } };

        this.loadNotes = function (latitude, longitude, distance) {
            var deferred = $q.defer();
            var data = $.param({ lattitude: latitude, longitude: longitude, distance: distance });

            $http.post("api/notes/getnearest", data, config).success(function (data) {
                deferred.resolve(data);
            }).error(function (data, status) {
                deferred.reject("Error: " + status);
            });
            return deferred.promise;
        };
        
        this.saveNote = function (latitude, longitude, title) {
            var deferred = $q.defer();
            var data = $.param({ lattitude: latitude, longitude: longitude, title: title });

            $http.post("api/notes", data, config).success(function (data) {
                deferred.resolve(data);
            }).error(function (data, status) {
                deferred.reject("Error: " + status);
            });
            return deferred.promise;
        };
    });

    app.controller("mapController", function ($scope, $timeout, noteService) {

        $scope.isLoaded = false;
        $scope.map = null;
        $scope.notes = [];
        $scope.nearestNotes = [];

        $scope.addNoteMarker = null;
        $scope.addNoteTitle = "";
        $scope.addNoteActive = false;


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
                $scope.isLoaded = false;

                $scope.refreshMapNotes();
                $scope.refreshNearestNotes();

                $scope.isLoaded = true;
            });
        }

        $scope.refreshMapNotes = function () {
            var center = $scope.map.getCenter();
            var radius = $scope.map.zoom * $scope.map.zoom;

            noteService.loadNotes(center.lat(), center.lng(), radius).then(function (data) {
                $scope.deleteNotes();

                angular.forEach(data.notes, function (item) {
                    var note = new MarkerWithLabel({
                        noteId: item.Id,
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

        $scope.refreshNearestNotes = function () {
            var center = $scope.map.getCenter();
            var radius = 2; //km

            noteService.loadNotes(center.lat(), center.lng(), radius).then(function (data) {
                $scope.nearestNotes = [];
                angular.forEach(data.notes, function (item) {
                    $scope.nearestNotes.push(item.title);
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
        }

        $scope.addNote = function () {
            var center = $scope.map.getCenter();

            var note = new MarkerWithLabel({
                noteId: -1,
                map: $scope.map,
                position: new google.maps.LatLng(center.lat(), center.lng()),
                draggable: true,
                raiseOnDrag: true,
                labelContent: "Новая заметка",
                labelAnchor: new google.maps.Point(90, 0),
                labelClass: "infobox infobox-new",
                labelStyle: { opacity: 0.9 },
                labelInBackground: true
            });
            $scope.addNoteMarker = note;
            $scope.addNoteActive = true;
        }

        $scope.saveNote = function () {
            noteService.saveNote($scope.addNoteMarker.position.lat(), $scope.addNoteMarker.position.lng(), $scope.addNoteTitle).then(function (data) {
                $scope.addNoteActive = false;
                $scope.addNoteMarker.setMap(null);
                $scope.addNoteTitle = "";

                $scope.refreshMapNotes();
                $scope.refreshNearestNotes();
            });
        }

        $timeout(function () {
            $scope.initialize();
        }, 100);

    });

})();
