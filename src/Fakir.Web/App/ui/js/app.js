var app = angular.module("app", [
    'oc.lazyLoad',
    'ui.router'
]);
app.config(["$provide", "$compileProvider", "$controllerProvider", "$filterProvider",
    function($provide, $compileProvider, $controllerProvider, $filterProvider) {
        app.controller = $controllerProvider.register;
        app.directive = $compileProvider.register;
        app.filter = $filterProvider.register;
        app.factory = $provide.factory;
        app.service = $provide.service;
        app.constant = $provide.constant;
    }
]);

app.run(["$rootScope", "$state", "$stateParams", function($rootScope, $state, $stateParams) {
    $rootScope.$state = $state;
    $rootScope.$stateParams = $stateParams;
}]);

app.controller("MainCtrl", ["$scope", function($scope) {

}]);

app.directive("uiNav", ["$timeout", function() {
    return {
        restrict: "AC",
        link: function(a, b) {
            b.find("a").bind("click", function() {
                var b = angular.element(this).parent();
                b.parent().find("li").removeClass("active");
                b.toggleClass("active");
            })
        }
    }
}])

app.config([
    '$stateProvider', '$urlRouterProvider',
    function($stateProvider, $urlRouterProvider) {
        $urlRouterProvider
            //.when('/c?id', '/contacts/:id')
            //.when('/user/:id', '/contacts/:id')
            .otherwise('/app/dashboard');

        $stateProvider
            .state("app", {
                "abstract": true, // abstract: true 表明此状态不能被显性激活，只能被子状态隐性激活
                url: "/app",
                views: {
                    "": {
                        templateUrl: "views/layout.html"
                    },
                    aside: {
                        templateUrl: "views/pages/dashboardasider.html"
                    }
                }
            })
            .state('app.dashboard', {
                url: "/dashboard",
                templateUrl: 'views/pages/dashboard.html',
            })
            .state('user', {
                url: "/user",
                views: {
                    "": {
                        templateUrl: "views/layout.html"
                    },
                    aside: {
                        templateUrl: 'views/pages/usersider.html'
                    }
                }
            })
            .state('user.main', {
                url: '/main',
                templateUrl: 'views/pages/user.html'
            })
            .state('user.list', {
                url: '/list',
                templateUrl: 'views/pages/userlist.html'
            })
            .state('user.log', {
                url: '/log',
                templateUrl: 'views/pages/log.html',
                controller: 'LogCtrl',
                resolve: {
                    deps: ["$ocLazyLoad", function(a) {
                        return a.load(["js/test.js"])
                    }]
                }
            });
    }
]);
