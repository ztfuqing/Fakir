(function () {
    appModule.controller('meet.views.agenda.uploadfile', [
        '$scope', '$uibModalInstance', 'FileUploader', 'abp.services.meet.agenda', 'agendaId',
        function ($scope, $uibModalInstance, fileUploader, agendaService, agendaId) {
            var vm = this;

            vm.saving = false;
            vm.fileItem = null;
            vm.item = null;

            vm.uploader = new fileUploader({
                url: '/api/fileupload/fileupload',
                queueLimit: 1,
                removeAfterUpload: true,
                formData: [{ agendaId: agendaId }],
                onAfterAddingFile: function (item) {
                    console.log(item.file);
                    vm.fileItem = item.file;
                },
                onSuccessItem: function (item, response, status, headers) {
                    console.log(status);
                    vm.fileItem = null;
                    init();
                },
                onErrorItem: function () {

                }
            });


            vm.uploadFile = function () {
                vm.uploader.uploadAll();
            }

            vm.deleteFle = function (id) {
                agendaService.deleteFile({
                    id: id
                }).success(function (result) {
                    init();
                });
            }

            vm.save = function () {
                vm.saving = true;
                console.log(vm.item);
                agendaService.saveAgenda({
                    agendaId: vm.item.agendaId,
                    content: vm.item.content
                }).success(function () {
                    abp.notify.info("保存成功");
                    $uibModalInstance.close();
                }).finally(function () {
                    vm.saving = false;
                });
            }

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            }

            function init() {
                agendaService.getAgendaForEdit({
                    id: agendaId
                }).success(function (result) {
                    vm.item = result;
                });
            }

            init();
        }
    ]);
})();