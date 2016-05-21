(function () {

    appModule.controller('meet.views.agenda.index', [
        '$scope', '$uibModal', '$stateParams', 'uiGridConstants', 'abp.services.meet.agenda',
        function ($scope, $uibModal, $stateParams, uiGridConstants, agendaService) {
            var vm = this;
            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;

            vm.permissions = {
                create:true,
                edit: true,
                'delete': true
            };

            var requestParams = {
            };

            vm.agendaGridOptions = {
                enableHorizontalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                enableVerticalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                paginationPageSizes: app.consts.grid.defaultPageSizes,
                paginationPageSize: app.consts.grid.defaultPageSize,
                useExternalPagination: true,
                useExternalSorting: true,
                appScopeProvider: vm,
                rowTemplate: '<div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader, \'text-muted\': !row.entity.isActive }"  ui-grid-cell></div>',
                columnDefs: [
                    {
                        name: "会议名称",
                        field: 'conferenceName',
                        minWidth: 120,
                        enableSorting: false
                    },
                    {
                        name: "议程名称",
                        field: 'subject',
                        minWidth: 120,
                        enableSorting: false
                        },
                    {
                        name: "发言人",
                        field: 'spokesman',
                        minWidth: 120,
                        enableSorting: false
                     },
                    {
                        name: "开始时间",
                        field: 'speakTime',
                        cellFilter: 'momentFormat: \'L\'',
                        minWidth: 100                        
                    },
                    {
                        name: "发言时长",
                        field: 'speakLength',
                        minWidth: 120,
                        enableSorting: false
                    },
                    {
                        name: "操作",
                        enableSorting: false,
                        headerCellTemplate: '<span></span>',
                        width: 120,
                        cellTemplate:
                             '<div class=\"ui-grid-cell-contents text-center\">' +
                             '<a class="btn btn-xs btn-primary blue" ng-if="grid.appScope.permissions.edit" ng-click="grid.appScope.editAgenda(row.entity)">上传资料</a>'+
                              '</div>'
                    },
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                },
                data: []
            };


            vm.getAgendas = function () {
                vm.loading = true;
                agendaService.getUserDepartmentAgendas({
                    id: abp.session.userId
                }).success(function (result) {
                    vm.agendaGridOptions.data = result;
                }).finally(function () {
                    vm.loading = false;
                });
            };


            vm.editAgenda = function (row) {
                openUpdateFileModal(row.id);
            };

            function openUpdateFileModal(agendaId) {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/Admin/views/meeting/agenda/uploadfile.cshtml',
                    controller: 'meet.views.agenda.uploadfile as vm',
                    backdrop: 'static',
                    //size:'lg',
                    resolve: {
                        agendaId: function () {
                            return agendaId;
                        }
                    }
                });

                modalInstance.result.then(function (result) {
                    vm.getAgendas();
                });
            }

            vm.getAgendas();
        }]);
})();