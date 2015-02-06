module App {
    "use strict";

    export interface IAppRootScopeService extends ng.IRootScopeService {
        appStarted: boolean;
        title: string;
    }

    var knowledgeCenterApp: ng.IModule = angular.module("knowledgeCenterApp", [
    // angular modules 
        "ngAnimate",
        "ngSanitize",
        "ui.router",
        "ui.bootstrap",
        "ui.bootstrap.popover",
        "ui.bootstrap.tpls",
    // custom modules 
        "common"
    ]);

    // handle routing errors and success events
    knowledgeCenterApp.run([
        "$rootScope", "$document", "$state", "config", "routermediator",
        ($rootScope: IAppRootScopeService, $document: ng.IDocumentService, $state: ng.ui.IStateService,
            config: App.IConfigProvider, routermediator: App.Services.IRouterMediator): void => {
            $rootScope.title = config.applicationName;
            $rootScope.$watch("title",(newValue: string, oldValue: string) => {
                document.title = newValue;
            });

            routermediator.setRoutingHandlers();
            if (config.startModule) {
                $state.go(config.startModule);
            }
        }
    ]);
} 