module App.Services {
    "use strict";

    var serviceId: string = "routermediator";

    export interface IRouterMediator {
        setRoutingHandlers(): void;
    }

    export class RouterMediator implements IRouterMediator {
        private $location: ng.ILocationService;
        private $rootScope: ng.IRootScopeService;
        private config: App.IConfigProvider;
        private common: Common.Interfaces.ICommonService;
        private handleRouteChangeError: boolean;

        constructor($location: ng.ILocationService, $rootScope: ng.IRootScopeService,
            config: App.IConfigProvider, common: Common.Interfaces.ICommonService) {
            this.$location = $location;
            this.$rootScope = $rootScope;
            this.config = config;
            this.common= common;
        }

        public setRoutingHandlers(): void {
            this.handleRoutingErrors();
        }

        private handleRoutingErrors() {
            this.$rootScope.$on("$stateChangeError",
                (event: any, current: any, previous: any, rejection: any) => {
                    if (this.handleRouteChangeError) { return; }
                    this.handleRouteChangeError = true;
                    var msg = "Error routing: " + (current && current.name)
                        + ". " + (rejection.msg || "");
                    this.common.logger.logWarning(msg, current, serviceId, true);
                    this.$location.path("/");
                });

            this.$rootScope.$on("$stateChangeSuccess",
                () => {
                    this.handleRouteChangeError = false;
                });
        }

    }

    angular.module("knowledgeCenterApp")
        .factory(serviceId, ["$location", "$rootScope", "config", "common",
            ($location: ng.ILocationService, $rootScope: ng.IRootScopeService,
                config: App.IConfigProvider, common: Common.Interfaces.ICommonService): IRouterMediator => {
                return new RouterMediator($location, $rootScope, config, common);
            }]);
} 