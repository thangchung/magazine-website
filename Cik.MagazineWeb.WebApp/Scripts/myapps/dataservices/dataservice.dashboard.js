define('dataservice.dashboard',
    ['amplify'],
    function(amplify) {
        var init = function() {

            amplify.request.define('dashboard-categories', 'ajax', {
                url: '/api/dashboard/getcategories',
                dataType: 'json',
                type: 'GET'
                //cache: true
            });
        },
            getCategories = function(callbacks) {
                return amplify.request({
                    resourceId: 'dashboard-categories',
                    success: callbacks.success,
                    error: callbacks.error
                });
            };

        init();

        return {
            getCategories: getCategories
        };
    });


