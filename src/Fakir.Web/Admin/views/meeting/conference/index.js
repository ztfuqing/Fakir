(function () {

    appModule.controller('meet.views.conference.index', [
        '$scope', '$uibModal', '$stateParams', 'uiGridConstants', 'abp.services.meet.conference',
        function ($scope, $uibModal, $stateParams, uiGridConstants, conferenceService) {
            var vm = this;
            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;
            vm.filterText = $stateParams.filterText || '';
            vm.currentUserId = abp.session.userId;

            vm.permissions = {
                create:true,
                edit: true,
                'delete': true
            };

            var requestParams = {
                skipCount: 0,
                maxResultCount: app.consts.grid.defaultPageSize,
                sorting: null
            };

            vm.conferenceGridOptions = {
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
                        name: "名称",
                        field: 'name',
                        minWidth: 120,
                        enableSorting: false
                    },
                    {
                        name: "主题",
                        field: 'subject',
                        minWidth: 120,
                        enableSorting: false,
                    },
                    {
                        name: "开始时间",
                        field: 'startTime',
                        cellFilter: 'momentFormat: \'L\'',
                        minWidth: 100                        
                    },
                    {
                        name: "地点",
                        field: 'site',
                        minWidth: 120,
                        enableSorting: false
                    }
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    $scope.gridApi.core.on.sortChanged($scope, function (grid, sortColumns) {
                        if (!sortColumns.length || !sortColumns[0].field) {
                            requestParams.sorting = null;
                        } else {
                            requestParams.sorting = sortColumns[0].field + ' ' + sortColumns[0].sort.direction;
                        }

                        vm.getConferences();
                    });
                    gridApi.pagination.on.paginationChanged($scope, function (pageNumber, pageSize) {
                        requestParams.skipCount = (pageNumber - 1) * pageSize;
                        requestParams.maxResultCount = pageSize;

                        vm.getConferences();
                    });
                },
                data: []
            };

            if (!vm.permissions.edit &&!vm.permissions.delete) {
                vm.conferenceGridOptions.columnDefs.shift();
            }

            vm.getConferences = function () {
                vm.loading = true;
                conferenceService.getConferences({
                    skipCount: requestParams.skipCount,
                    maxResultCount: requestParams.maxResultCount,
                    sorting: requestParams.sorting,
                    filter: vm.filterText
                }).success(function (result) {
                    vm.conferenceGridOptions.totalItems = result.totalCount;
                    vm.conferenceGridOptions.data = result.items;
                }).finally(function () {
                    vm.loading = false;
                });
            };

            vm.createConference = function () {
                openCreateOrEditConferenceModal(null);
            };

            function openCreateOrEditConferenceModal(conferenceId) {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/Admin/views/meeting/conference/createOrEditModal.cshtml',
                    controller: 'meet.views.conference.createOrEditModal as vm',
                    backdrop: 'static',
                    resolve: {
                        conferenceId: function () {
                            return conferenceId;
                        }
                    }
                });

                modalInstance.result.then(function (result) {
                    vm.getConferences();
                });
            }

            vm.getConferences();
        }]);
})();