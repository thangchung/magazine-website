define('bootstrapper',
    ['jquery', 'config', 'presenter', 'dataprimer', 'binder'],
    function ($, config, presenter, dataprimer, binder) {
        var
            run = function () {
                presenter.toggleActivity(true);

                config.dataserviceInit();

                $.when(dataprimer.fetch())
                    .done(binder.bind)
                    //.done(routeConfig.register)
                    .always(function () {
                        presenter.toggleActivity(false);
                    });
            };

        return {
            run: run
        };
    });