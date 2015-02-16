module App.DataServices {
    "use strict";

    var serviceId: string = "knowledgeBaseDataService";

    export interface IKnowledgeBaseDataService {
        getRootItems(): ng.IPromise<any[]>;
        getChildItems(parent: any): ng.IPromise<any[]>;
        getRootTreeFromChild(childId: string): ng.IPromise<any>;
    }

    export class KnowledgeBaseDataService implements IKnowledgeBaseDataService {
        private baseDataservice: App.DataServices.IBaseDataService;

        constructor(baseDataservice: App.DataServices.IBaseDataService) {
            this.baseDataservice = baseDataservice;
        }

        public getRootItems(): ng.IPromise<any[]> {
            return this.baseDataservice.getData("/api/knowledgebase");
        }

        public getChildItems(parent: any): ng.IPromise<any[]> {
            return this.baseDataservice.getData("/api/knowledgebase/" + parent.domainId)
                .then((data: any[]) => {
                this.setParent(data, parent);
                return data;
            });
        }

        public getRootTreeFromChild(childId: string): ng.IPromise<any> {
            return this.baseDataservice.getData("/api/knowledgebase/roottree/" + childId)
                .then((data: any): void => {
                this.setParent(data.childItems, data);
                return data;
            });
        }


        // private Methods
        private setParent(childs: any[], parent: any): void {
            if (childs) {
                childs.forEach((value: any): void => {
                    value.parent = parent;
                    this.setParent(value.childItems, value);
                });
            }
        }
    }

    angular.module("knowledgeCenterApp")
        .factory(serviceId, ["baseDataservice",
        (baseDataservice: App.DataServices.IBaseDataService): IKnowledgeBaseDataService => {
            return new KnowledgeBaseDataService(baseDataservice);
        }]);
} 