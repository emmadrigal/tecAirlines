// create the module and name it scotchApp
var tecAirlineApp = angular.module('tecAirlineApp', ['ngRoute', 'ui.bootstrap']);

// configure our routes
tecAirlineApp.config(function($routeProvider) {
	$routeProvider

		// route for the home page
		.when('/', {
			templateUrl : 'pages/home.html',
			controller  : 'mainController'
		})

		// route for the about page
		.when('/searchFlight', {
			templateUrl : 'pages/searchFlight.html',
			controller  : 'mainController'
		})

		.when('/user', {
			templateUrl : 'pages/userMain.html',
			controller : 'mainController'
		});
});

tecAirlineApp.controller('signupFormController', function($scope, $uibModalInstance) {

	$scope.addNewUser = function(){
		/*var newFile = {
			Id_Number:  $scope.newClientID,
			Code: $scope.newClientCode,
			Name: $scope.newClientName,
			Phone_Number: $scope.newClientPhoneNumber,
			Role_usuario: role
		};

		$scope.newData(JSON.stringify(newFile));*/
		$uibModalInstance.dismiss('cancel');
	}

	$scope.newData = function(data) {
		$http.post('http://localhost:50484/Usuario', data).then(
			function(response) {
	   			console.log(response);
    		}, function(error) {
        		console.log(error);
    		});
	};
});

tecAirlineApp.controller('loginFormController', function($location, $scope, $uibModalInstance) {

	$scope.addNewUser = function(){
		$uibModalInstance.dismiss('cancel');
	}

	$scope.goTo = function ( path ) {
    	$location.path( path );
    	$uibModalInstance.dismiss('cancel');
    };
});

tecAirlineApp.controller('reservationFormController', function($scope, $uibModalInstance, $uibModal, value) {
	
	$scope.getNumber = function() {
        return new Array(value);   
    }

	$scope.openPaymentForm = function(){
    	var modalInstance = $uibModal.open({
			controller: 'paymentFormController',
            templateUrl: 'paymentForm.html'
        });
    }

    $scope.goBack = function(){
		$uibModalInstance.dismiss('cancel');
	}
});

tecAirlineApp.controller('paymentFormController', function($scope, $uibModalInstance) {
	$scope.goBack = function(){
		$uibModalInstance.dismiss('cancel');
	}
});

tecAirlineApp.controller('cancelReservationFormController', function($scope, $uibModalInstance, listFlights) {
	$scope.listFlights = listFlights;
	$scope.goBack = function(){
		$uibModalInstance.dismiss('cancel');
	}
});

tecAirlineApp.controller('modifyInfoFormController', function($scope, $uibModalInstance, user) {
	$scope.user = user;
	$scope.goBack = function(){
		$uibModalInstance.dismiss('cancel');
	}
});

tecAirlineApp.controller('promotionController', function($scope){
	$scope.myInterval = 5000;
    $scope.noWrapSlides = false;
  $scope.active = 0;
  var slides = $scope.slides = [];
  var currIndex = 0;

  $scope.addSlide = function() {
    var newWidth = 600 + slides.length + 1;
    slides.push({
      image: '//unsplash.it/' + newWidth + '/300',
      id: currIndex++
    });
  };

  $scope.randomize = function() {
    var indexes = generateIndexesArray();
    assignNewIndexesToSlides(indexes);
  };

  for (var i = 0; i < 4; i++) {
    $scope.addSlide();
  }

  // Randomize logic below

  function assignNewIndexesToSlides(indexes) {
    for (var i = 0, l = slides.length; i < l; i++) {
      slides[i].id = indexes.pop();
    }
  }

  function generateIndexesArray() {
    var indexes = [];
    for (var i = 0; i < currIndex; ++i) {
      indexes[i] = i;
    }
    return shuffle(indexes);
  }

  // http://stackoverflow.com/questions/962802#962890
  function shuffle(array) {
    var tmp, current, top = array.length;

    if (top) {
      while (--top) {
        current = Math.floor(Math.random() * (top + 1));
        tmp = array[current];
        array[current] = array[top];
        array[top] = tmp;
      }
    }

    return array;
  }

})

tecAirlineApp.controller('mainController', function($scope, $location, $uibModal){

	$scope.openSignupForm = function () {
        var modalInstance = $uibModal.open({
			controller: 'signupFormController',
            templateUrl: 'signupForm.html'
        });
    }

    $scope.openLoginForm = function () {
        var modalInstance = $uibModal.open({
			controller: 'loginFormController',
            templateUrl: 'loginForm.html'
        });
    }

    $scope.openReservationForm = function(value){

    	var modalInstance = $uibModal.open({
			controller: 'reservationFormController',
            templateUrl: 'reservationForm.html',
            resolve: {
            	value: function(){
            		return value;
            	}
            }
        });
    	console.log(value);
    }

    $scope.openCancelReservationForm = function(){

    	var listFlights = $scope.listFlights;
		var modalInstance = $uibModal.open({
			controller: 'cancelReservationFormController',
            templateUrl: 'cancelReservationForm.html',
            resolve: {
            	listFlights: function(){
            		return listFlights;
            	}
            }
        });
    }

    $scope.openProfileInfoForm = function(){

    	var user = $scope.user;
		var modalInstance = $uibModal.open({
			controller: 'modifyInfoFormController',
            templateUrl: 'profileInfoForm.html',
            resolve: {
            	user: function(){
            		return user;
            	}
            }
        });
    }

    $scope.oneAtATime = true;
    $scope.isCollapsed = true;

    $scope.status = {
	    isCustomHeaderOpen: false,
	    isFirstOpen: false,
	    isFirstDisabled: false
  	};

  	$scope.listFlights = [
  		{id: 123, depATO: "SJO", arrivATO: "LAX", duration: 1200, price: "$100 or 1000 miles", depTime: "12:00 am", arrivTime: "8:00 pm"},
  		{id: 124, depATO: "SJO", arrivATO: "LAX", duration: 1200, price: "$100 or 1000 miles", depTime: "12:00 am", arrivTime: "8:00 pm"},
  		{id: 125, depATO: "SJO", arrivATO: "LAX", duration: 1200, price: "$100 or 1000 miles", depTime: "12:00 am", arrivTime: "8:00 pm"}
  	];

  	$scope.user = {
  		name: "Emma",
  		firstSurname: "Madrigal",
  		secondSurname: "Cerdas",
  		id: 123,
  		telephoneNumber: 680,
  		username: "emma123",
  		password: "pass",
  		birthdate: "12/12/1999",
  		email: "email@email.com",
  		identification: 1234,
  		frequentFlyerMiles: 10000
  	};

  	$scope.goTo = function ( path ) {
    	$location.path( path );
    };

});