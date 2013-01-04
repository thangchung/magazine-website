define('presenter',
    ['jquery'],
    function ($) {
        var
            transitionOptions = {
                ease: 'swing',
                fadeOut: 100,
                floatIn: 500,
                offsetLeft: '20px',
                offsetRight: '-20px'
            },

            entranceThemeTransition = function ($view) {
                $view.css({
                    display: 'block',
                    visibility: 'visible'
                }).addClass('view-active').animate({
                    marginRight: 0,
                    marginLeft: 0,
                    opacity: 1
                }, transitionOptions.floatIn, transitionOptions.ease);
            },

            highlightActiveView = function (route, group) {
                // Reset top level nav links
                // Find all NAV links by CSS classname 
                var $group = $(group);
                if ($group) {
                    $(group + '.route-active').removeClass('route-active');
                    if (route) {
                        // Highlight the selected nav that matches the route
                        $group.has('a[href="' + route + '"]').addClass('route-active');
                    }
                }

            },

            resetViews = function () {
                $('.view').css({
                    marginLeft: transitionOptions.offsetLeft,
                    marginRight: transitionOptions.offsetRight,
                    opacity: 0
                });
            },

            toggleActivity = function (show) {
                $('#busyindicator').activity(show);
            },

            transitionTo = function ($view, route, group) {
                var $activeViews = $('.view-active');

                toggleActivity(true);

                if ($activeViews.length) {
                    $activeViews.fadeOut(transitionOptions.fadeOut, function () {
                        resetViews();
                        entranceThemeTransition($view);
                    });
                    $('.view').removeClass('view-active');
                } else {
                    resetViews();
                    entranceThemeTransition($view);
                }

                highlightActiveView(route, group);

                toggleActivity(false);
            };


        return {
            toggleActivity: toggleActivity,
            transitionOptions: transitionOptions,
            transitionTo: transitionTo
        };
    });
