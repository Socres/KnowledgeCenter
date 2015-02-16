module App.DataServices {
    "use strict";

    var serviceId: string = "baseDataservice";

    export interface IBaseDataService {
        getData<T>(remoteResource: string, params?: any): ng.IPromise<T>;
        postData(remoteResource: string, params: any):
            ng.IPromise<any>;
    }

    export class BaseDataService implements IBaseDataService {
        private $http: ng.IHttpService;
        private config: App.IConfigProvider;
        private common: Common.Interfaces.ICommonService;

        constructor($http: ng.IHttpService, config: App.IConfigProvider, common: Common.Interfaces.ICommonService) {
            this.$http = $http;
            this.config = config;
            this.common = common;
        }

        public getData<T>(remoteResource: string, params?: any): ng.IPromise<T> {
            var deferred: ng.IDeferred<T> = this.common.$q.defer();

            this.$http.get<T>(remoteResource,
                {
                    params: params
                }).
                success(data => {
                    this.common.logger.logSuccess("Success getting " + remoteResource, params, serviceId, this.config.debugEnabled);
                    deferred.resolve(data);
                }).
                error((data: any, status: number): void => {
                    this.common.logger.logError("Error getting " + remoteResource, status, serviceId, true);
                    deferred.reject(status);
                });

            return deferred.promise;
        }

        public postData(remoteResource: string, params: any):
            ng.IPromise<any> {
            var deferred: ng.IDeferred<any> = this.common.$q.defer();

            this.$http.post<any>(remoteResource, params).
                success(data => {
                    if (data.IsSuccessful) {
                        this.common.logger.logSuccess("Success posting " + remoteResource, params, serviceId, this.config.debugEnabled);
                        deferred.resolve(data);
                    } else {
                        this.common.logger.logError(data, status, serviceId, true);
                        deferred.reject(data);
                    }
                }).
                error((data: any, status: number): void => {
                    this.common.logger.logError("Error posting " + remoteResource, status, serviceId, true);
                    deferred.reject(status);
                });

            return deferred.promise;
        }
    }

    angular.module("knowledgeCenterApp")
        .factory(serviceId, ["$http", "config", "common",
            ($http: ng.IHttpService, config: App.IConfigProvider,
                common: Common.Interfaces.ICommonService): IBaseDataService => {
                return new BaseDataService($http, config, common);
            }]);
}