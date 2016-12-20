function EditFeedListController(feedEditor, $log, $window) {
    this.feeds = feedEditor.query();
    this.delete = function (id) {
        feedEditor.delete({id:id});
        $log.log('delete');
        $route.reload();
    };
    this.edit = function (id) {
        $log.log('edit');
    };

}

angular.module('feedManager').component(
    'editList', {
        templateUrl:'Scripts/app/components/editList.template.html',
        controller: EditFeedListController
        }
)