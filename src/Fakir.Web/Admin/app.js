/* 'app' MODULE DEFINITION */
var appModule = angular.module("app", [
    "ui.router",
    "ui.bootstrap",
    'ui.utils',
    "ui.jq",
    'ui.grid',
    'ui.grid.pagination',
    "oc.lazyLoad",
    "ngSanitize",
    'angularFileUpload',
    'daterangepicker',
    'angularMoment',
    'frapontillo.bootstrap-switch',
    'abp'
]);

appModule.config(['$ocLazyLoadProvider', function ($ocLazyLoadProvider) {
    $ocLazyLoadProvider.config({
        cssFilesInsertBefore: 'ng_load_plugins_before', // load the css files before a LINK element with this ID.
        debug: false,
        events: true,
        modules: []
    });
}]);

/* THEME SETTINGS */
App.setAssetsPath(abp.appPath + 'metronic/assets/');
appModule.factory('settings', ['$rootScope', function ($rootScope) {
    var settings = {
        layout: {
            pageSidebarClosed: false, // sidebar menu state
            pageContentWhite: true, // set page content layout
            pageBodySolid: false, // solid body color state
            pageAutoScrollOnLoad: 1000 // auto scroll to top on page load
        },
        layoutImgPath: App.getAssetsPath() + 'admin/layout4/img/',
        layoutCssPath: App.getAssetsPath() + 'admin/layout4/css/',
        assetsPath: abp.appPath + 'metronic/assets',
        globalPath: abp.appPath + 'metronic/assets/global',
        layoutPath: abp.appPath + 'metronic/assets/layouts/layout4'
    };

    $rootScope.settings = settings;

    return settings;
}]);

/* ROUTE DEFINITIONS */

appModule.config([
    '$stateProvider', '$urlRouterProvider',
    function ($stateProvider, $urlRouterProvider) {

        // Default route (overrided below if user has permission)
        $urlRouterProvider.otherwise("/dashboard");

        //Welcome page
        $stateProvider.state('dashboard', {
            url: '/dashboard',
            templateUrl: '~/Admin/views/dashboard/index.cshtml'
        });

        if (abp.auth.hasPermission('Pages.Administration.Roles')) {
            $stateProvider.state('roles', {
                url: '/roles',
                templateUrl: '~/Admin/views/roles/index.cshtml',
                menu: 'Administration.Roles'
            });
        }

        if (abp.auth.hasPermission('Pages.Administration.Users')) {
            $stateProvider.state('users', {
                url: '/users?filterText',
                templateUrl: '~/Admin/views/users/index.cshtml',
                menu: 'Administration.Users'
            });
        }


        if (abp.auth.hasPermission('Pages.Administration.AuditLogs')) {
            $stateProvider.state('auditLogs', {
                url: '/auditLogs',
                templateUrl: '~/Admin/views/auditLogs/index.cshtml',
                menu: 'Administration.AuditLogs'
            });
        }

        if (abp.auth.hasPermission('Pages.Administration.OrganizationUnits')) {
            $stateProvider.state('organizationUnits', {
                url: '/organizationUnits',
                templateUrl: '~/Admin/views/organizationUnits/index.cshtml',
                menu: 'Administration.OrganizationUnits'
            });
        }


        $stateProvider.state('notifications', {
            url: '/notifications',
            templateUrl: '~/Admin/views/notifications/index.cshtml'
        });

        $stateProvider.state('meeting', {
            url: '/meeting',
            templateUrl: '~/Admin/views/meeting/conference/index.cshtml'
        });

    }
]);

appModule.run(["$rootScope", "settings", "$state", "i18nService", "$uibModalStack", function ($rootScope, settings, $state, i18nService, $uibModalStack) {
    $rootScope.$state = $state;
    $rootScope.$settings = settings;
    $rootScope.$on("$stateChangeSuccess",function () {
        $uibModalStack.dismissAll();
    });

    i18nService.get(abp.localization.currentCulture.name) ? i18nService.setCurrentLang(abp.localization.currentCulture.name) : i18nService.setCurrentLang("en");

    $rootScope.safeApply = function (fn) {
        var phase = this.$root.$$phase;
        if (phase == '$apply' || phase == '$digest') {
            if (fn && (typeof (fn) === 'function')) {
                fn();
            }
        } else {
            this.$apply(fn);
        }
    };
}]);