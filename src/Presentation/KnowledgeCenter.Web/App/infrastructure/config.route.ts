module App {
    "use strict";

    // configure the routes and route resolvers
    angular.module("knowledgeCenterApp")
        .config(["$stateProvider", "$urlRouterProvider", "configDataProvider", routeConfigurator]);

    function routeConfigurator(
        $stateProvider: ng.ui.IStateProvider,
        $urlRouterProvider: ng.ui.IUrlRouterProvider,
        configDataProvider: any): void {

        var configData = configDataProvider.$get();

        addRoutes($stateProvider, configData.navigationItems);

        function addRoutes($stateProvider: ng.ui.IStateProvider, navigationItems: any) {
            angular.forEach(navigationItems,(value: any, key: any) => {
                $stateProvider.state(value.name, value);
            });
        };

        $urlRouterProvider.otherwise("/");
    }

}