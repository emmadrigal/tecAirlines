// create the module and name it scotchApp
var tecAirlineApp = angular.module('tecAirlineApp', ['ngRoute', 'ui.bootstrap']);

tecAirlineApp.run(function($rootScope) {
  $rootScope.isLogged = false;
  $rootScope.rootUrl = "http://192.168.43.163:";
  $rootScope.modify = false;
  $rootScope.data;
  $rootScope.promotions;
  $rootScope.reservation;
  $rootScope.user = {
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
});

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

});

tecAirlineApp.controller('messageFormController', function($scope, $uibModalInstance, message) {

  $scope.message = message;

});

tecAirlineApp.controller('signupFormController', function($scope, $uibModalInstance, $uibModal) {

  $scope.goBack = function(){
    $uibModalInstance.dismiss('cancel');
  }

  $scope.openMessage = function(){

    var message = "Your account has been created succesfully, check your email to confirm and finish the process.";
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

});

tecAirlineApp.controller('loginFormController', function($scope, $uibModalInstance, $uibModal, $rootScope) {

  $scope.login = function(){
    $rootScope.isLogged = true;
    $scope.goBack();
  }

  $scope.goBack = function(){
    $uibModalInstance.dismiss('cancel');
  }

});

tecAirlineApp.controller('reservationFormController', function($http, $rootScope, $scope, $uibModalInstance, $uibModal, value) {
	
	$scope.getNumber = function() {
    return new Array(value[0]);   
  }

	$scope.openPaymentForm = function(){

    var reservation;

    if($rootScope.isLogged){
      reservation = {
        userName: $rootScope.user.username,
        userID: [1, 2, 3],
        flightNumber: value[1],
        date: value[2]
      };
    }
    else{
      reservation = {
        userName: "nouser",
        userID: [1, 2, 3],
        flightNumber: value[1],
        date: value[2]
      };
    }

    $scope.newData(JSON.stringify(reservation));

    var modalInstance = $uibModal.open({
			controller: 'paymentFormController',
      templateUrl: 'paymentForm.html'
      });
    }

  $scope.goBack = function(){
	  $uibModalInstance.dismiss('cancel');
  }

  $scope.newData = function(data) {
    $http.post($rootScope.rootUrl + "10000/Reservation/create", data).then(
      function(response) {
          console.log(response);
        }, function(error) {
            console.log(error);
        });
  };
  });

tecAirlineApp.controller('paymentFormController', function($uibModal, $scope, $uibModalInstance) {
	
  $scope.goBack = function(){
		$uibModalInstance.dismiss('cancel');
	}

  $scope.openMessage = function(){

    var message = "Your payment has been made succesfully, safe flight!";
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

});

tecAirlineApp.controller('cancelReservationFormController', function($http, $rootScope, $scope, $uibModalInstance) {

	$scope.goBack = function(){
		$uibModalInstance.dismiss('cancel');
	}

  $scope.cancelReservation = function(id){

    $http.delete($rootScope.rootUrl + "10000/Reservation/" + id).then(
      function(response){
        console.log(response);
      },
      function(response){
        console.log(error);
      }
    );

    $scope.getContent("10000/Reservation/Kevin");
  }

   $scope.getContent = function(url){
    $http.get($rootScope.rootUrl + url).then(
      function(response){
        $rootScope.data = response.data;
        console.log(response.data);

      },
      function(response){
        console.log(error);
      });
  };

});

tecAirlineApp.controller('modifyInfoFormController', function($rootScope, $scope, $uibModalInstance, user) {

  $scope.user = user;

  $scope.modifyInfo = function(){
    $rootScope.modify = true;
  }

	$scope.goBack = function(){
    $rootScope.modify = false;
		$uibModalInstance.dismiss('cancel');
	}
});

tecAirlineApp.controller('promotionController', function($rootScope, $http, $scope){


	$scope.myInterval = 10000;
  $scope.noWrapSlides = false;
  $scope.active = 0;
  var slides = $scope.slides = [];
  var currIndex = 0;

  $scope.addSlide = function(i) {

    var newWidth = 600 + slides.length + 1;
    slides.push({
      image: $rootScope.data[i].Link,
      id: currIndex++
    });
  };

  $scope.randomize = function() {
    var indexes = generateIndexesArray();
    assignNewIndexesToSlides(indexes);
  };

  setTimeout(function() {
    for (var i = 0; i < $rootScope.data.length; i++) {
      $scope.addSlide(i);
    }
  }, 400);

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

tecAirlineApp.controller('mainController', function($scope, $location, $uibModal, $http, $rootScope){

  $scope.openLoginForm = function () {
    var modalInstance = $uibModal.open({
      controller: 'loginFormController',
      templateUrl: 'loginForm.html'
    });
  }

  $scope.openSingupForm = function () {
    var modalInstance = $uibModal.open({
      controller: 'signupFormController',
      templateUrl: 'signupForm.html'
    });
  }


  $scope.openReservationForm = function(id, date){


    var value = [$scope.number, id, date]

  	var modalInstance = $uibModal.open({
		  controller: 'reservationFormController',
      templateUrl: 'reservationForm.html',
      backdrop: 'static',
      keyboard: false,
      resolve: {
      	value: function(){
      		return value;
      	}
      }
    });
  }

  $scope.openCancelReservationForm = function(){

    $scope.getContent("10000/Reservation/Kevin");

  	var listFlights = $scope.listFlights;
	  var modalInstance = $uibModal.open({
			controller: 'cancelReservationFormController',
      templateUrl: 'cancelReservationForm.html',
      backdrop: 'static',
      keyboard: false
    });
  }

  $scope.openProfileInfoForm = function(){

  	var user = $scope.user;
		var modalInstance = $uibModal.open({
			controller: 'modifyInfoFormController',
      templateUrl: 'profileInfoForm.html',
      backdrop: 'static',
      keyboard: false,
      resolve: {
      	user: function(){
      		return user;
      	}
      }
    });
  }

  //Variables for the pagination of the searchFlight page
  //$scope.totalItems = $scope.listFlights.length;
  $scope.currentPage = 1;

  $scope.filter = "Price";

  $scope.oneAtATime = true;
  $scope.isCollapsed = true;
  $scope.numberTickets;

  $scope.status = {
    isCustomHeaderOpen: false,
    isFirstOpen: false,
    isFirstDisabled: false
	};

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

  $scope.changeFilter = function(type){
    if (type == "Price") {
      $scope.filter = "Price";
    } 
    else if(type == "Stops") {
      $scope.filter = "Stops";
    }
    else {
      $scope.filter = "Distance";
    }
  };

	$scope.goTo = function ( path ) {
  	$location.path( path );
  };

  $scope.logout = function(){
    $rootScope.isLogged = false;
    $scope.goTo('');
  }

  $scope.searchFlights = function(){
    $scope.getContent("10002/getAllFlights");
    $scope.goTo("searchFlight");
  }

  $scope.getContent = function(url){
    $http.get($rootScope.rootUrl + url).then(
      function(response){
        $rootScope.data = response.data;
        console.log(response.data);
      },
      function(response){
        console.log(error);
      });
  };

  $scope.getPromotions = function(){
    $scope.getContent('10001/promotion/all');
  }

});