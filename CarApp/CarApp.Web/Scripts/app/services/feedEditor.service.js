angular.module('feedManager').factory('feedEditor', 
    function($resource) {
        return $resource('api/feeds/:id', {}, {
            edit: {
                method: 'PUT',
                isArray: false,
                params: '@id',
            },
            delete: {
                method: 'DELETE',
                isArray: false,
                params: '@id'
            }
        });
    }
)