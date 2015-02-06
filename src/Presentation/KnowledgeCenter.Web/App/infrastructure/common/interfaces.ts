module Common.Interfaces {
    "use strict";

    export interface IController {
        activate(): void;
    }

    export interface IEvents {
        controllerActivateSuccessEvent: string;
        controllerActivatingEvent: string;
        spinnerToggleEvent: string;
    }

    export interface IControllerActivateData {
        controllerId: string;
        getBreadcrumb: Function;
        canSearch?: boolean;
    }

    export interface ICommonConfigProvider {
        config: Common.Interfaces.IEvents;
        $get(): ICommonConfigProvider;
    }

    export interface ICommonService {
        // common angular dependencies
        $broadcast(event: string, data: any): void;
        $q: ng.IQService;
        $timeout: ng.ITimeoutService;
        // generic
        logger: Common.ILoggerService;
        activateController(promises: ng.IPromise<any>[], controllerId: string,
            getBreadcrumb: Function, canSearch?: boolean): ng.IPromise<void>;
    }
} 