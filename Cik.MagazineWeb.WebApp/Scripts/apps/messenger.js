define('messenger',
['amplify', 'config'],
    function (amplify, config) {
        var
            priority = 1,
            
            publish = function (topic, options) {
                amplify.publish(topic, options);
            },

            subscribe = function (options) {
                amplify.subscribe(
                    options.topic,
                    options.context,
                    options.callback,
                    priority);
            };

        publish.viewModelActivated = function(options) {
            amplify.publish(config.messages.viewModelActivated, options);
        };

        return {
            publish: publish,
            subscribe: subscribe
        };
    });
