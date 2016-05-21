(function () {
    appModule.controller('meet.views.showmeeting.show', [
        '$rootScope', '$scope', '$stateParams', 'abp.services.meet.conference', 'abp.services.meet.agenda',
        function ($rootScope, $scope, $stateParams, conferenceService, agendaService) {
            $scope.$on('$viewContentLoaded', function () {
                App.initComponents(); // init core components
            });
            $rootScope.settings.layout.pageSidebarClosed = true;
            var vm = this;

            vm.item = null;
            vm.currentAgenda = null;
            vm.showfile = true;

            vm.changeFileState = function () {
                vm.showfile = !vm.showfile;
            }

            vm.setAgenda = function (id) {
                agendaService.getAgendaForEdit({
                    id: id
                }).success(function (result) {
                    vm.currentAgenda = result;
                    console.log(vm.currentAgenda);
                });
            }

            function init() {
                conferenceService.getConferenceDetail({
                    id: $stateParams.id
                }).success(function (result) {
                    vm.item = result;
                })
            }

            init();
        }
    ]);
})();