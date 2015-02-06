module App {
    "use strict";

    // configure the routes and route resolvers
    angular.module("knowledgeCenterApp")
        .config(["$stateProvider", "$urlRouterProvider", routeConfigurator]);

    function routeConfigurator(
        $stateProvider: ng.ui.IStateProvider,
        $urlRouterProvider: ng.ui.IUrlRouterProvider): void {

        //var routeState: ng.ui.IState = {
        //    name: "",
        //    url: "/"
        //};
        //$stateProvider.state(name, routeState);

        $urlRouterProvider.otherwise("/");
    }

}