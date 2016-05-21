(function () {
    appModule.controller('meet.views.showmeeting', [
        '$scope',
        function ($scope) {
            $scope.$on('$viewContentLoaded', function () {
                App.initComponents(); // init core components
            });

            var vm = this;
        }
    ]);
})();