module App.Customers.Controllers {
    "use strict";

    var controllerId: string = "knowledgeBaseMain";

    export interface IknowledgeBaseMainController extends Common.Interfaces.IController {
        items: any[];
        selectedItem: any;
        markdown: string;

        selectItem(item: any): void;
    }

    export class KnowledgeBaseMainController implements IknowledgeBaseMainController {
        private $scope: ng.IScope;
        private $stateParams: any;
        private $state: ng.ui.IStateService;
        private common: Common.Interfaces.ICommonService;
        private config: App.IConfigProvider;
        private knowledgeBaseService: App.DataServices.IKnowledgeBaseDataService;
        private markdownService: App.Services.IMarkdownService;

        constructor($scope: ng.IScope, $stateParams: any, $state: ng.ui.IStateService,
            common: Common.Interfaces.ICommonService, config: App.IConfigProvider,
            knowledgeBaseService: App.DataServices.IKnowledgeBaseDataService,
            markdownService: App.Services.IMarkdownService) {
            this.$scope = $scope;
            this.$stateParams = $stateParams;
            this.$state = $state;
            this.common = common;
            this.config = config;
            this.knowledgeBaseService = knowledgeBaseService;
            this.markdownService = markdownService;

            this.activate();
        }

        public items: any[] = [];
        public selectedItem: any = null;
        public markdown: string = "";

        public activate(): void {
            this.$scope.$on(this.config.events.controllerSearch,
                (event: ng.IAngularEvent, data: any): void => {
                    alert("Search: " + data);
                });

            var controller = this;
            this.$scope.$watch("vm.selectedItem", function (current: any, original: any) {
                controller.markdown =
                current
                    ? controller.markdownService.createHtml(current.markdown)
                    : "";
            });

            var domainId = this.$stateParams.domainId;
            this.common.activateController(
                [
                    this.getRootItems()
                        .then((): ng.IPromise<void> => {
                        // after we received the root items,
                        // check if we need to get an entire tree to a child
                        if (domainId) {
                            return this.getRootTreeFromChild(domainId);
                        }
                    })
                ],
                controllerId,
                null,
                true);
        }

        public selectItem(item: any): void {
            var newValue = !item.isOpen;
            if (newValue) {
                if (item.childItems.length === 0) {
                    // get the childItems from a rest call
                    this.knowledgeBaseService.getChildItems(item)
                        .then((data: any[]): void => {
                            item.childItems = data;
                        });
                }

                // if we're opening this item
                // close all root items
                this.closeChildren(this.items);
                // and make sure ít's parents are open
                this.openParent(item);
            } else {
                // if we're closing this item
                this.closeChildren(item.childItems);
            }
            item.isOpen = newValue;
            this.selectedItem = item;
            this.$state.go(this.$state.current.name,
            {
                domainId: this.selectedItem.domainId
            },
            {
                notify: false
            });
        }   

        // private methods
        private getRootItems(): ng.IPromise<void> {
            return this.knowledgeBaseService.getRootItems()
                .then((data: any[]): void => {
                this.items = data;
            });
        }

        private getRootTreeFromChild(domainId: string): ng.IPromise<void> {
            return this.knowledgeBaseService.getRootTreeFromChild(domainId)
                .then((data: any): void => {
                if (this.items) {
                    for (var i = 0; i < this.items.length; i++) {
                        // replace the root item with the root tree
                        if (this.items[i].domainId === data.domainId) {
                            this.items[i] = data;
                            // find the specified childItem and select it
                            var childItem = this.getItem(this.items[i].childItems, domainId);
                            if (childItem) {
                                this.selectItem(childItem);
                            }
                            i = this.items.length;
                        }
                    }
                }
            });
        }

        private getItem(items: any[], domainId: string): any {
            for (var i = 0; i < items.length; i++) {
                if (items[i].domainId === domainId) {
                    return items[i];
                }
                if (items[i].childItems) {
                    return this.getItem(items[i].childItems, domainId);
                }
            }
            return null;
        }

        private closeChildren(items: any[]): void {
            if (items) {
                items.forEach((value: any): void => {
                    // no need to recursivly loop through the children when the item is already closed
                    if (value.isOpen) {
                        value.isOpen = false;
                        this.closeChildren(value.childItems);
                    }
                });
            }
        }

        private openParent(item: any): void {
            if (item.parent) {
                item.parent.isOpen = true;
                this.openParent(item.parent);
            }
        }
    }

    angular.module("knowledgeCenterApp")
        .controller(controllerId,
        [
            "$scope",
            "$stateParams",
            "$state",
            "common",
            "config",
            "knowledgeBaseDataService",
            "markdownService",
            KnowledgeBaseMainController
        ]);
}