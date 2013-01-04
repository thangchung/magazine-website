define('route-config',
    ['config', 'router', 'vm'],
    function (config, router, vm) {
        var
            logger = config.logger,
            
            register = function() {

                var routeData = [

                    // Dashboard routes
                    {
                        view: config.viewIds.dashboard,
                        routes: [{
                            isDefault: true,
                            route: config.hashes.dashboard,
                            title: 'Dashboard',
                            callback: vm.dashboard.activate,
                            group: '.route-top'
                        }]
                    },

                    // Invalid routes
                    {
                        view: '',
                        route: /.*/,
                        title: '',
                        callback: function() {
                            logger.error(config.toasts.invalidRoute);
                        }
                    }
                ];

                for (var i = 0; i < routeData.length; i++) {
                    router.register(routeData[i]);
                }

                // Crank up the router
                router.run();
            };
            

        return {
            register: register
        };
    });