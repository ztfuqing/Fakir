(function () {

    appModule.controller('common.views.auditLogs.index', [
        '$scope', '$uibModal', 'uiGridConstants', 'abp.services.app.auditLog',
        function ($scope, $uibModal, uiGridConstants, auditLogService) {
            var vm = this;

            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;
            vm.advancedFiltersAreShown = false;

            vm.requestParams = {
                userName: '',
                serviceName: '',
                methodName: '',
                browserInfo: '',
                hasException: '',
                skipCount: 0,
                maxResultCount: app.consts.grid.defaultPageSize,
                sorting: null
            };

            var todayAsString = moment().format('YYYY-MM-DD');
            vm.dateRangeOptions = app.createDateRangePickerOptions();
            vm.dateRangeModel = {
                startDate: todayAsString,
                endDate: todayAsString
            };

            vm.gridOptions = {
                enableHorizontalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                enableVerticalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                paginationPageSizes: app.consts.grid.defaultPageSizes,
                paginationPageSize: app.consts.grid.defaultPageSize,
                useExternalPagination: true,
                useExternalSorting: true,
                appScopeProvider: vm,
                columnDefs: [
                    {
                        name: "操作",
                        enableSorting: false,
                        width: 50,
                        headerCellTemplate: '<span></span>',
                        cellTemplate:
                            '<div class=\"ui-grid-cell-contents text-center\">' +
                                '  <button class="btn btn-default btn-xs" ng-click="grid.appScope.showDetails(row.entity)"><i class="fa fa-search"></i></button>' +
                                '</div>'
                    },
                    {
                        field: "状态",
                        enableSorting: false,
                        width: 30,
                        headerCellTemplate: '<span></span>',
                        cellTemplate:
                            '<div class=\"ui-grid-cell-contents text-center\">' +
                                '  <i class="fa fa-check-circle font-green" ng-if="!row.entity.exception"></i>' +
                                '  <i class="fa fa-warning font-yellow-gold" ng-if="row.entity.exception"></i>' +
                                '</div>'
                    },
                    {
                        name: "时间",
                        field: 'executionTime',
                        cellFilter: 'momentFormat: \'YYYY-MM-DD HH:mm:ss\'',
                        minWidth: 150
                    },
                    {
                        name: "用户",
                        field: 'userName',
                        minWidth: 150
                    },
                    {
                        name: "服务",
                        enableSorting: false,
                        field: 'serviceName',
                        minWidth: 200
                    },
                    {
                        name: "方法",
                        enableSorting: false,
                        field: 'methodName',
                        minWidth: 200
                    },
                    {
                        name: "耗时",
                        field: 'executionDuration',
                        width: 80,
                        cellTemplate:
                            '<div class=\"ui-grid-cell-contents\">' +
                                app.localize('Xms', '{{COL_FIELD CUSTOM_FILTERS}}') +
                                '</div>'
                    },
                    {
                        name: "IP地址",
                        field: 'clientIpAddress',
                        enableSorting: false,
                        minWidth: 120
                    },
                    {
                        name: "客户端",
                        field: 'clientName',
                        enableSorting: false,
                        minWidth: 140
                    },
                    {
                        name: "浏览器",
                        field: 'browserInfo',
                        enableSorting: false,
                        minWidth: 200
                    }
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    $scope.gridApi.core.on.sortChanged($scope, function (grid, sortColumns) {
                        if (!sortColumns.length || !sortColumns[0].field) {
                            vm.requestParams.sorting = null;
                        } else {
                            vm.requestParams.sorting = sortColumns[0].field + ' ' + sortColumns[0].sort.direction;
                        }

                        vm.getAuditLogs();
                    });
                    gridApi.pagination.on.paginationChanged($scope, function (pageNumber, pageSize) {
                        vm.requestParams.skipCount = (pageNumber - 1) * pageSize;
                        vm.requestParams.maxResultCount = pageSize;

                        vm.getAuditLogs();
                    });
                },
                data: []
            };

            vm.getAuditLogs = function () {
                vm.loading = true;
                auditLogService.getAuditLogs($.extend({}, vm.requestParams, vm.dateRangeModel))
                    .success(function (result) {
                        vm.gridOptions.totalItems = result.totalCount;
                        vm.gridOptions.data = result.items;
                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            vm.exportToExcel = function () {
                auditLogService.getAuditLogsToExcel(vm.requestParams)
                    .success(function (result) {
                        app.downloadTempFile(result);
                    });
            };

            vm.showDetails = function (auditLog) {
                $uibModal.open({
                    templateUrl: '~/Admin/views/auditLogs/detailModal.cshtml',
                    controller: 'common.views.auditLogs.detailModal as vm',
                    backdrop: 'static',
                    resolve: {
                        auditLog: function () {
                            return auditLog;
                        }
                    }
                });
            };

            vm.getAuditLogs();
        }
    ]);
})();