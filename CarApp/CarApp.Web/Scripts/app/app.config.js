angular.module('feedManager').config(
    function config($routeProvider) {
        $routeProvider.when('/list', {
            template: '<view-list></view-list>'
        }).
        when('/edit-list', {
            template: '<edit-list></edit-list>'
        }).
        when('/new', {
            template:'<new-feed></new-feed>'
        }).
        otherwise('/list');
    }
);