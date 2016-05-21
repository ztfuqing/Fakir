(function () {
    appModule.controller('common.views.dashboard.index', [
        '$scope', '$uibModal', 'abp.services.meet.conference',
        function ($scope, $uibModal, conferenceService) {
            var vm = this;
            vm.loading = false;
            vm.enCompleteConferences = null;
            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.getConferenceDetail = function (id)
            {
                openConferenceDetailModal(id);
            }


            function openConferenceDetailModal(conferenceId) {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/Admin/views/dashboard/conferenceDetailModal.cshtml',
                    controller: 'common.views.dashboard.conferenceDetailModal as vm',
                    backdrop: 'static',
                    size: 'conference-detail',
                    resolve: {
                        conferenceId: function () {
                            return conferenceId;
                        }
                    }
                });

                modalInstance.result.then(function (result) {
                    vm.getUsers();
                });
            }

            function init() {
                vm.loading = true;
                conferenceService.getEnCompleteConference({
                }).success(function (result) {
                    vm.enCompleteConferences = result;
                }).finally(function () {
                    vm.loading = false;
                });
            }

            init();
        }
    ]);
})();