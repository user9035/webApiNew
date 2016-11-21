var dataLoaderModule = angular.module('carManufacturerDataLoader');
dataLoaderModule.factory('carManufacturer', ['$resource',
    function ($resource) {
        return $resource('appData/:id.json',{},{
            query:{
                method:'GET',
                params:{id:'list'},
                isArray:'true'
            }
        })
    }
])