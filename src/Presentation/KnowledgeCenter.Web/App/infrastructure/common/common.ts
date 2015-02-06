module Common {
    "use strict";

    var commonModule: ng.IModule = angular.module("common", []);

    export class CommonConfigProvider implements Common.Interfaces.ICommonConfigProvider {
        public config: Common.Interfaces.IEvents = {
            controllerActivateSuccessEvent: "",
            controllerActivatingEvent: "",
            spinnerToggleEvent: ""
        };
        public $get(): Common.Interfaces.ICommonConfigProvider {
            return this;
        }
    }

    export class CommonService implements Common.Interfaces.ICommonService {
        private $rootScope: ng.IRootScopeService;
        private commonConfig: Common.Interfaces.ICommonConfigProvider;

        public $q: ng.IQService = null;
        public $timeout: ng.ITimeoutService = null;
        public logger: Common.ILoggerService = null;

        constructor($q: ng.IQService, $rootScope: ng.IRootScopeService, $timeout: ng.ITimeoutService,
            commonConfig: Common.Interfaces.ICommonConfigProvider, logger: Common.ILoggerService) {
            this.$rootScope = $rootScope;
            this.commonConfig = commonConfig;

            this.$q = $q;
            this.$timeout = $timeout;
            this.logger = logger;
        }

        public activateController(promises: ng.IPromise<any>[], controllerId: string,
            getBreadcrumb: Function, canSearch?: boolean): ng.IPromise<void> {
            this.$broadcast(this.commonConfig.config.controllerActivatingEvent, controllerId);

            return this.$q.all(promises).then((): void => {

                var data: Common.Interfaces.IControllerActivateData = {
                    controllerId: controllerId,
                    canSearch: canSearch,
                    getBreadcrumb: getBreadcrumb
                };
                this.logger.log("Activated " + controllerId + " controller.", data, controllerId);

                this.$broadcast(this.commonConfig.config.controllerActivateSuccessEvent, data);
            });
        }

        public $broadcast(event: string, data: any): any {
            return this.$rootScope.$broadcast.apply(this.$rootScope, [event, data]);
        }
    }

    commonModule.factory("common", [
        "$q", "$rootScope", "$timeout", "commonConfig", "logger",
        ($q: ng.IQService, $rootScope: ng.IRootScopeService, $timeout: ng.ITimeoutService,
            commonConfig: Common.Interfaces.ICommonConfigProvider, logger: Common.ILoggerService): Common.Interfaces.ICommonService => {
            return new CommonService($q, $rootScope, $timeout, commonConfig, logger);
        }
    ]);

    commonModule.provider("commonConfig", CommonConfigProvider);
}