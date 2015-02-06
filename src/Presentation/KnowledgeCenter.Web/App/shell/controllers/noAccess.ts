module App.Shell.Controllers {
    "use strict";

    var controllerId: string = "noAccess";

    export interface INoAccessController extends Common.Interfaces.IController {
    }

    export class NoAccessController implements INoAccessController {
        private common: Common.Interfaces.ICommonService;

        constructor(common: Common.Interfaces.ICommonService) {
            this.common = common;

            this.activate();
        }

        public activate(): void {
            
            // activate the controller
            this.common.activateController([], controllerId, null);
        }
    }

    angular.module("knowledgeCenterApp")
        .controller(controllerId, ["common", NoAccessController]);

}