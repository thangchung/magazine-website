define('dataservice.dashboard',
    ['amplify', 'utils'],
    function(amplify, utils) {
        var init = function() {
            amplify.request.define('dashboard-categories', 'ajax', {
                url: utils.resolvingUrl('/api/dashboard/getcategories'),
                dataType: 'json',
                type: 'GET',
                decoder: function (data, status, xhr, success, error) {
                    utils.amplifyDecoder(data, status, xhr, success, error);
                }
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


