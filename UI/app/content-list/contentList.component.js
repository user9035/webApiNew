var listModule = angular.module('contentList');
listModule.component('listComponent', {
    templateUrl:'content-list/contentList.template.html',
    controller:['carManufacturer',
        function manufacturerController(carManufacturer) {
            this.list = carManufacturer.query();
        }
    ]
})