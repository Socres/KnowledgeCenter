module App {
    "use strict";

    angular.module("knowledgeCenterApp")
        .config([
            "$provide", ($provide: ng.auto.IProvideService): void => {
                $provide.decorator("$exceptionHandler",
                ["$delegate", "config", "logger", extendExceptionHandler]);
            }
        ]);

    // extend the $exceptionHandler service to also display a toast.
    function extendExceptionHandler($delegate: Function, config: App.IConfigProvider, logger: Common.ILoggerService): Function {
        var appErrorPrefix: string = config.appErrorPrefix;
        return (exception: Error, cause: string): void => {
            $delegate(exception, cause);
            if (appErrorPrefix && exception.message.indexOf(appErrorPrefix) === 0) { return; }

            var errorData: any = { exception: exception, cause: cause };
            var msg: string = appErrorPrefix + exception.message;
            logger.logError(msg, errorData, "knowledgeCenterApp", true);
        };
    }
}