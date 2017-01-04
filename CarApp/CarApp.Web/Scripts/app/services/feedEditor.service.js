angular.module('feedManager').factory('feedEditor', 
    function($resource) {
        return $resource('api/feeds/:id', {}, {
            edit: {
                method: 'PUT',
                isArray: false
            },
            delete: {
                method: 'DELETE',
                isArray: false,
                params: '@id'
            },
            find: {
                method: 'GET',
                isArray: false,
                params: '@id'
            }
        });
    }
)