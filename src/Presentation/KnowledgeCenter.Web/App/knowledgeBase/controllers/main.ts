module App.Customers.Controllers {
    "use strict";

    var controllerId: string = "knowledgeBaseMain";

    export interface IknowledgeBaseMainController extends Common.Interfaces.IController {
    }

    export class KnowledgeBaseMainController implements IknowledgeBaseMainController {
        private $scope: ng.IScope;
        private common: Common.Interfaces.ICommonService;
        private config: App.IConfigProvider;

        constructor($scope: ng.IScope, common: Common.Interfaces.ICommonService,
            config: App.IConfigProvider) {
            this.$scope = $scope;
            this.common = common;
            this.config = config;

            this.activate();
        }

        public activate(): void {
            this.$scope.$on(this.config.events.controllerSearch,
                (event: ng.IAngularEvent, data: any): void => {
                    alert("Search: " + data);
                });

            this.common.activateController([], controllerId, null, true);
        }
    }

    angular.module("knowledgeCenterApp")
        .controller(controllerId, ["$scope", "common", "config", KnowledgeBaseMainController]);
}