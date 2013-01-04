define('vm.dashboard',
    ['ko', 'underscore', 'datacontext'],
    function (ko, _, datacontext) {
        var categories = ko.observableArray(),
            activate = function(routeData, callback) {
                refresh(callback);
            },
            canLeave = function() {
                return true;
            },
            forceRefreshCmd = ko.asyncCommand({
                execute: function(complete) {
                }
            }),
            getCategories = function() {
                categories = datacontext.dashboard.getData({
                    results: categories
                });
            },
            refresh = function(callback) {
                getCategories(callback);
            };

        return {
            activate: activate,
            canLeave: canLeave,
            categories: categories,
            forceRefreshCmd: forceRefreshCmd,
            //category : category
        };
    });