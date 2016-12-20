function ViewFeedControlled(feedEditor, $window) {
    this.list = feedEditor.query();
    this.view = function(id) { $window.location.href = "/home/viewfeed/" + id; }
}

angular.module('feedManager').component('viewList', {
    templateUrl:'Scripts/app/components/viewList.template.html',
    controller:ViewFeedControlled
})