(function () {
    appModule.controller('meet.views.conference.createOrEditModal', [
        '$scope', '$uibModalInstance', 'abp.services.meet.conference', 'conferenceId',
        function ($scope, $uibModalInstance, conferenceService, conferenceId) {
            var vm = this;

            vm.saving = false;
            vm.conference = null;
            vm.agenda = {};
            vm.users = [];
            vm.agendas = [];
            vm.departmentUserData = null;
            vm.departmentOptions = null;
            vm.selectDepartment=null;
            vm.save = function () {
                vm.saving = true;
                conferenceService.createOrUpdateConference({
                    conference: vm.conference,
                    users: _.map(vm.departmentUserData.selectedUsers, function (item) { return { userId: item, displayName: "" } }),
                    agendas: vm.agendas

                }).success(function () {
                    abp.notify.info("保存成功");
                    $uibModalInstance.close();
                }).finally(function () {
                    vm.saving = false;
                });
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };

            vm.getAgendasCount = function () {
                return vm.agendas.length;
            };

            vm.getUsersCount = function () {
                if (vm.departmentUserData && vm.departmentUserData.selectedUsers)
                    return vm.departmentUserData.selectedUsers.length;
                return "";
            };

            vm.deleteAgenda = function (row) {
                vm.agendas = _.without(vm.agendas, row);
            }

            vm.addAgenda = function (row) {
                var department = _.chain(vm.departmentOptions).where({ value: vm.selectDepartment.value }).first().value();
                row.departmentId=department.value;
                row.departmentName=department.displayText;
                vm.agendas[vm.agendas.length] = row;
                vm.agenda = {};
            }

            vm.setStartTime = function onTimeSet(newDate, oldDate)
            {
                vm.conference.startTime = newDate;
            }

            vm.setEndTime = function onTimeSet(newDate, oldDate) {
                vm.conference.endTime = newDate;
            }

            function init() {
                conferenceService.getConferenceForEdit({
                    id: conferenceId
                }).success(function (result) {
                    vm.conference = result.conference;
                    vm.users = result.users;
                    vm.departmentUserData = result.departmentUserData;
                    vm.departmentOptions = result.departments;
                    vm.agendas = result.agendas ? result.agendas : {};
                });
            }

            init();
        }
    ]);
})();