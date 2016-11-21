var carModule = angular.module('carApp');
carModule.config(['$routeProvider',
    function config($routeProvider) {
        $routeProvider.
        when('/list', {
            template: '<list-component></list-component>'
        }).
        when('/edit-list', {
            template: '<p>The functionality is not implemented yet</p>'
        }).
        otherwise('/list');
    }
]);