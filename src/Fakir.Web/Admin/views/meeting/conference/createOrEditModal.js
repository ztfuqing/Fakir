(function () {
    appModule.controller('meet.views.conference.createOrEditModal', [
        '$scope', '$uibModalInstance', 'abp.services.meet.conference', 'conferenceId',
        function ($scope, $uibModalInstance, conferenceService, conferenceId) {
            var vm = this;

            vm.saving = false;
            vm.conference = null;
            vm.users = [];
            vm.agendas = [];
            vm.agendas[0] = { subject: "议程一", spokesman: "付庆", speakTime: "2015-12-11", speakLength: "8分钟", department: "市场部", order: 1 }
            vm.users[0] = { userId: 1, displayName: "付庆" };
            vm.save = function () {
                vm.saving = true;
                conferenceService.createOrUpdateConference({
                    conference: vm.conference,
                    users: vm.users,
                    agendas: vm.agendas
                }).success(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    $uibModalInstance.close();
                }).finally(function () {
                    vm.saving = false;
                });
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };

            vm.getAgendasCount = function () {
                return _.where(vm.agendas, {}).length;
            };

            function init() {
                if (conferenceId != null) {
                    conferenceService.getConferenceForEdit({
                        id: conferenceId
                    }).success(function (result) {
                        vm.conference = result.conference;
                        vm.users = result.users;
                        vm.agendas = result.agendas;
                    });
                }
            }

            init();
        }
    ]);
})();