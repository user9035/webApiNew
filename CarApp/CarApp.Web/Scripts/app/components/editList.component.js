function EditFeedListController(feedEditor, $route, $location) {
    this.feeds = feedEditor.query();

    this.delete = function (id) {
        feedEditor.delete({ id: id })
        $route.reload();
    };
    
    this.edit = function (id) {
        let path = $location.path();
        $location.path('edit/' + id);
        path = location.path();
        $route.reload();
    };
};

angular.module('feedManager').component(
    'editList', {
        templateUrl:'Scripts/app/components/editList.template.html',
        controller: EditFeedListController
        }
)