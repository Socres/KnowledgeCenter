module App {
    "use strict";

    export interface IEvents {
        controllerActivateSuccess: string;
        controllerActivating: string;
        controllerSearch: string;
        spinnerToggle: string;
    }

    export interface INavigationItem {
        name: string;
        url: string;
        titleIconCss: string;
        title: string;
    }

    export interface IConfigProvider {
        appErrorPrefix: string;
        applicationName: string;
        documentTitle: string;
        debugEnabled: boolean;
        culture: string;
        startModule: string;
        events: IEvents;
        jsResources: any;
        navigationItems: INavigationItem[];
    }

    var knowledgeCenterApp: ng.IModule = angular.module("knowledgeCenterApp");

    // configure Toastr
    toastr.options.timeOut = 4000;
    toastr.options.positionClass = "toast-bottom-right";
    toastr.options.backgroundpositionClass = "toast-bottom-right";

    var config: IConfigProvider = {
        appErrorPrefix: "[Error] ",
        applicationName: "",
        documentTitle: "",
        debugEnabled: false,
        culture: "",
        startModule: "",
        events: {
            controllerActivateSuccess: "controller.activateSuccess",
            controllerActivating: "controller.activating",
            controllerSearch: "controller.search",
            spinnerToggle: "spinner.toggle"
        },
        jsResources: {},
        navigationItems: []
    };

    knowledgeCenterApp.value("config", config);

    knowledgeCenterApp.config(["$logProvider", ($logProvider: any): void => {
        // turn debugging off/on (no info or warn)
        if ($logProvider.debugEnabled) {
            $logProvider.debugEnabled(true);
        }
    }]);

    knowledgeCenterApp.config([
        "configDataProvider", (configDataProvider: any) => {
            var configData: any = configDataProvider.$get();

            /*
            * Recursively merge properties of two objects 
            */
            function MergeRecursive(obj1: Object, obj2: Object) {
                for (var propertyName in obj2) {
                    if (obj2.hasOwnProperty(propertyName)) {
                        try {
                            // property in destination object set; update its value.
                            if (obj2[propertyName].constructor === Object) {
                                obj1[propertyName] = MergeRecursive(obj1[propertyName], obj2[propertyName]);
                            } else {
                                obj1[propertyName] = obj2[propertyName];
                            }

                        } catch (e) {
                            // property in destination object not set; create it and set its value.
                            obj1[propertyName] = obj2[propertyName];
                        }
                    }
                }
                return obj1;
            }

            MergeRecursive(config, configData);
        }
    ]);

    // #region Configure the common services via commonConfig
    knowledgeCenterApp.config(["commonConfigProvider", (cfg: Common.Interfaces.ICommonConfigProvider) => {
        cfg.config.controllerActivateSuccessEvent = config.events.controllerActivateSuccess;
        cfg.config.controllerActivatingEvent = config.events.controllerActivating;
        cfg.config.spinnerToggleEvent = config.events.spinnerToggle;
    }]);
    // #endregion
}