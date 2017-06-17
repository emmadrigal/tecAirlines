
var tecAirlineApp = angular.module('tecAirlineApp', ['ngRoute', 'ui.bootstrap']);

tecAirlineApp.run(function($rootScope) {
    $rootScope.isLogged = false;
    $rootScope.admin = true;
});

// configure our routes
tecAirlineApp.config(function($routeProvider) {
	$routeProvider

		// route for the home page
		.when('/', {
			templateUrl : 'pages/login.html',
			controller  : 'mainController'
		})

		.when('/flights', {
			templateUrl : 'pages/flights.html',
			controller  : 'mainController'
		})
		.when('/staff', {
			templateUrl : 'pages/staff.html',
			controller  : 'mainController'
		})
		.when('/checkin', {
			templateUrl : 'pages/checkin.html',
			controller  : 'mainController'
		});
});

tecAirlineApp.controller('messageFormController', function($scope, $uibModalInstance, message){
	$scope.message = message;
})

tecAirlineApp.controller('staffFormController', function($scope, $uibModal, $uibModalInstance){
	
	$scope.goBack = function(){
		$uibModalInstance.dismiss('cancel');
    };

    $scope.save = function(){
		var message = "The "+$scope.type+" account was created succesfully";

		var modalInstance = $uibModal.open({
			controller: 'messageFormController',
	  		templateUrl: 'messageForm.html',
	  		resolve: {
      			message: function(){
      				return message;
      			}
			}
		});

		$scope.goBack();
	};
})

tecAirlineApp.controller('flightFormController', function($scope, $uibModal, $uibModalInstance, infoFlight){
	$scope.infoFlight1 = infoFlight;

	$scope.save = function(){
		var message = "Changes have been saved succesfully";

		var modalInstance = $uibModal.open({
			controller: 'messageFormController',
	  		templateUrl: 'messageForm.html',
	  		resolve: {
      			message: function(){
      				return message;
      			}
			}
		});

		$scope.goBack();
	}

	$scope.goBack = function(){
		$uibModalInstance.dismiss('cancel');
    }
});
tecAirlineApp.controller('mainController', function($rootScope, $scope, $location, $uibModal, $http){

  	$scope.goTo = function ( path ) {
	  	$location.path( path );
	};

	$scope.login = function(){
		if ($scope.validate()) {
			$rootScope.isLogged = true;
			$scope.goTo('flights');
		}
		else{
			console.log("Nope");
		}
	}

	$scope.logout = function(){
		$rootScope.isLogged = false;
		$scope.goTo('');
	}

	$scope.validate = function(){
		if($scope.loginUsername == "admin" && $scope.loginPassword == "pass"){
			$rootScope.admin = true;
			return true;
		}
		if($scope.loginUsername == "flightattendant" && $scope.loginPassword == "pass"){
			$rootScope.admin = false;
			return true;
		}
		else{
			return false;
		}
	}

	$scope.getStatus = function(value){
		if (value==0) {
			return "Created"
		}
		if (value==1) {
			return "Open"
		}
		if (value==2) {
			return "Closed"
		} else {
			return "Cancelled"
		}
	}

	$scope.listFlights = [{
		id: 123,
		depATO: "SJO",
		arrivATO: "LAX",
		airline: "Delta",
		depTime: "hora",
		arrivTime: "hora",
		status: 1,
		price: 10000,
		distance: 10000,
		airplane: "4-25"
	}];

	$scope.listStaff = [{
		id: 123,
		name: "Emma",
		firstSurname: "Madrigal",
		secondSurname: "Cerdas",
		role: "Administrator"
	},
	{
		id: 124,
		name: "Diego",
		firstSurname: "Navarro",
		secondSurname: "Masís",
		role: "Flight Attendant"
	}];

	$scope.cancelFlight = function(ID){
		for (var i = $scope.listFlights.length - 1; i >= 0; i--) {
			if($scope.listFlights[i].id == ID){
				$scope.listFlights[i].status = 3;
				return;
			} 
		}
	}

	$scope.openStaffForm = function(){
		var modalInstance = $uibModal.open({
			controller: 'staffFormController',
	  		templateUrl: 'staffForm.html',
	  		backdrop: 'static',
	  		keyboard: false
	  	});
	}

	$scope.removeAccount = function(){
		var message = "The account was eliminated succesfully";

		var modalInstance = $uibModal.open({
			controller: 'messageFormController',
	  		templateUrl: 'messageForm.html',
	  		resolve: {
      			message: function(){
      				return message;
      			}
			}
		});
	}

	$scope.openFlightForm = function(ID){
		var infoFlight = [];
	  	if (ID != 0){
	  		for (var i = $scope.listFlights.length - 1; i >= 0; i--) {
				if($scope.listFlights[i].id == ID){
					infoFlight = $scope.listFlights[i];
					break;
				} 
			}
	  	}

		var modalInstance = $uibModal.open({
			controller: 'flightFormController',
	  		templateUrl: 'flightForm.html',
	  		backdrop: 'static',
	  		keyboard: false,
	  		resolve: {
      			infoFlight: function(){
      				return infoFlight;
      			}
      		}
	  	});
  	}

  	$scope.save = function(){
		var message = "The user was checked-in succesfully";

		var modalInstance = $uibModal.open({
			controller: 'messageFormController',
	  		templateUrl: 'messageForm.html',
	  		resolve: {
      			message: function(){
      				return message;
      			}
			}
		});
	};

  	$scope.openFlightStatusForm = function(){

		var modalInstance = $uibModal.open({
			controller: 'mainController',
	  		templateUrl: 'flightStatusForm.html'
		});
  	}

});