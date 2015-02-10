module App.Shell.Controllers {
    "use strict";

    var controllerId: string = "nav";

    export interface INavController {
        navRoutes: App.INavigationItem[];
        isCurrent(route: App.INavigationItem): string;
    }

    export class NavController implements INavController {
        private $state: ng.ui.IStateService;
        private config: App.IConfigProvider;

        constructor($state: ng.ui.IStateService, config: App.IConfigProvider) {
            this.$state = $state;
            this.config = config;

            this.activate();
        }

        public navRoutes: App.INavigationItem[] = null;

        public activate(): void {
            this.navRoutes = this.config.navigationItems;
        }

        public isCurrent(route: App.INavigationItem): string {
            if (!route.name || !this.$state.current || !this.$state.current.name) {
                return "";
            }

            var menuName: string = route.name;
            var routeName: string = this.$state.current.name;
            if (this.$state.current.data && this.$state.current.data.navigationParentName) {
                routeName = this.$state.current.data.navigationParentName;
            }
            return routeName === menuName ? "current" : "";
        }
    }

    angular.module("knowledgeCenterApp")
        .controller(controllerId, ["$state", "config", NavController]);

}