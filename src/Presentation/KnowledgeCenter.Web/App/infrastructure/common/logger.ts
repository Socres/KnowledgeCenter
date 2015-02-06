module Common {
    "use strict";

    export interface ILoggerService {
        log(message: string, data: any, source: string, showToast?: boolean): void;
        logError(message: string, data: any, source: string, showToast?: boolean): void;
        logSuccess(message: string, data: any, source: string, showToast?: boolean): void;
        logWarning(message: string, data: any, source: string, showToast?: boolean): void;
    }

    export class LoggerService implements ILoggerService {
        private $log: ng.ILogService;

        constructor($log: ng.ILogService) {
            this.$log = $log;
        }

        public log(message: string, data: any, source: string, showToast?: boolean): void {
            this.logIt(message, data, source, "info", showToast);
        }

        public logWarning(message: string, data: any, source: string, showToast?: boolean): void {
            this.logIt(message, data, source, "warning", showToast);
        }

        public logSuccess(message: string, data: any, source: string, showToast?: boolean): void {
            this.logIt(message, data, source, "success", showToast);
        }

        public logError(message: string, data: any, source: string, showToast?: boolean): void {
            this.logIt(message, data, source, "error", showToast);
        }

        private logIt(message: string, data: any, source: string, toastType: string, showToast?: boolean): void {
            var write: any = (toastType === "error") ? this.$log.error : this.$log.log;
            source = source ? "[" + source + "] " : "";
            var dataAsString: string = null;
            if (data)
                dataAsString = " (" + data + ")";
            write(source, message, dataAsString);
            if (showToast) {
                if (toastType === "error") {
                    toastr.error(message);
                } else if (toastType === "warning") {
                    toastr.warning(message);
                } else if (toastType === "success") {
                    toastr.success(message);
                } else {
                    toastr.info(message);
                }
            }
        }
    }

    angular.module("common")
        .factory("logger", ["$log",
            ($log: ng.ILogService): ILoggerService => {
                return new LoggerService($log);
            }
        ]);
}