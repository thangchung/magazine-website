define('dataprimer',
    ['ko', 'datacontext', 'config'],
    function (ko, datacontext, config) {

        var logger = config.logger,
            
            fetch = function () {
                
                return $.Deferred(function (def) {

                    var data = {
                        categories: ko.observable()
                    };

                    $.when(
                        datacontext.dashboard.getData({ results: data.categories })
                    )

                    .pipe(function() {
                        logger.success('Fetched data for: '
                            + '<div>' + data.categories().length + ' categories </div>'
                        );
                    })

                    .fail(function () { def.reject(); })

                    .done(function () { def.resolve(); });

                }).promise();
            };

        return {
            fetch: fetch
        };
    });