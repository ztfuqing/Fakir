(function () {
    appModule.controller('common.views.dashboard.conferenceDetailModal', [
        '$scope', '$uibModalInstance', 'abp.services.meet.conference', 'conferenceId',
        function ($scope, $uibModalInstance, conferenceService, conferenceId) {
            var vm = this;
            vm.item = null;

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };

            function init()
            {
                conferenceService.getConferenceDetail({
                    id:conferenceId
                }).success(function(result)
                {
                    vm.item = result;
                })
            }

            init();
        }
    ]);
})();