module App.Shell.Controllers {
    "use strict";

    var controllerId: string = "shell";

    export interface IShellController extends Common.Interfaces.IController {
        isBusy: boolean;
        showSplash: boolean;
        applicationName: string;
        busyMessage: string;
        debugEnabled: boolean;
        canEnableDebug: boolean;
        canSearch: boolean;
        lastSearchValue: string;
        debugChanged(): void;
        clearSearchEvent(): void;
        sendSearchEvent(): void;
    }

    export class ShellController implements IShellController {
        private $rootScope: App.IAppRootScopeService;
        private $scope: ng.IScope;
        private common: Common.Interfaces.ICommonService;
        private config: App.IConfigProvider;


        constructor(
            $rootScope: App.IAppRootScopeService,
            $scope: ng.IScope,
            common: Common.Interfaces.ICommonService,
            config: App.IConfigProvider) {
            this.$rootScope = $rootScope;
            this.$scope = $scope;
            this.common = common;
            this.config = config;

            this.activate();
        }
        public isBusy: boolean = true;
        public showSplash: boolean = true;
        public applicationName: string = "";
        public busyMessage: string = "";
        public debugEnabled: boolean = false;
        public canEnableDebug: boolean = false;
        public canSearch: boolean = false;
        public lastSearchValue: string = "";

        public activate(): void {
            this.debugEnabled = this.config.debugEnabled;
            this.canEnableDebug = this.config.debugEnabled;
            this.applicationName = this.config.applicationName;
            this.busyMessage = this.config.jsResources.busyMessage;

            // register for $stateChangeError event
            this.$rootScope.$on("$stateChangeError",
                (): void=> {
                    this.toggleSpinner(false);
                });

            // register for controllerActivating event
            this.$rootScope.$on(this.config.events.controllerActivating,
                (e: ng.IAngularEvent, data: string): void=> {
                    this.common.logger.logSuccess("controllerActivatingEvent", data, controllerId);
                    this.toggleSpinner(true);
                });

            // register for controllerActivateSuccess event
            this.$rootScope.$on(this.config.events.controllerActivateSuccess,
                (e: ng.IAngularEvent, data: Common.Interfaces.IControllerActivateData): void=> {
                    this.common.logger.logSuccess("controllerActivateSuccessEvent", data, controllerId);
                    this.processControllerActivation(e, data);
                });

            // register for spinnerToggle event
            this.$rootScope.$on(this.config.events.spinnerToggle,
                (e: ng.IAngularEvent, data: any): void=> {
                    this.toggleSpinner(data.show);
                });

            // activate the controller
            this.common.activateController([], controllerId, null)
                .then((): void=> {
                this.$rootScope.appStarted = true;
                this.showSplash = false;
                this.common.logger.logSuccess(this.config.applicationName + " " + this.config.jsResources.applicationStartedMessage, null, controllerId, true);
            });
        }

        public debugChanged(): void {
            this.config.debugEnabled = this.debugEnabled;
        }

        public clearSearchEvent(): void {
            this.lastSearchValue = "";
            this.sendSearchEvent();
        }

        public sendSearchEvent(): void {
            this.common.$broadcast(this.config.events.controllerSearch, this.lastSearchValue);
        }

        private processControllerActivation(e: ng.IAngularEvent, data: Common.Interfaces.IControllerActivateData): void {
            // set title
            var title: string = this.config.applicationName;
            this.$rootScope.title = title;

            // set search availability
            this.canSearch = data.canSearch;
            if (this.canSearch &&
                this.lastSearchValue) {
                this.sendSearchEvent();
            }

            this.toggleSpinner(false);
        }

        private toggleSpinner(on: boolean): void {
            this.isBusy = on;
        }
    }

    angular.module("knowledgeCenterApp")
        .controller(controllerId,
        ["$rootScope", "$scope", "common", "config",
            ShellController]);

}