(function () {
    appModule.controller('common.views.layout.header', [
        '$rootScope', '$scope', '$uibModal', 'appSession', 'appUserNotificationHelper', 'abp.services.app.notification',
        function ($rootScope, $scope, $uibModal, appSession, appUserNotificationHelper, notificationService) {
            var vm = this;

            $scope.$on('$includeContentLoaded', function () {
                Layout.initHeader(); // init header
            });

        }
    ]);
})();