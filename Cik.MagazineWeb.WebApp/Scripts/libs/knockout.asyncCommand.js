// By: Hans Fjällemark and John Papa
// https://github.com/CodeSeven/KoLite

(function(ko) {
    ko.asyncCommand = function(options) {
        var
        self = ko.observable(),
            canExecuteDelegate = options.canExecute,
            executeDelegate = options.execute,

            completeCallback = function() {
                self.isExecuting(false);
            };

        self.isExecuting = ko.observable();

        self.canExecute = ko.computed(function() {
            return canExecuteDelegate ? canExecuteDelegate(self.isExecuting()) : true;
        });

        self.execute = function(argument) {
            var args = []; // Allow for this argument to be passed on to execute delegate
            if (executeDelegate.length === 2) {
                args.push(argument);
            }

            args.push(completeCallback);
            self.isExecuting(true);
            executeDelegate.apply(this, args);
        };
        return self;
    };
})(ko);

;(function (ko) {
    ko.utils.wrapAccessor = function (accessor) {
        return function () {
            return accessor;
        };
    };
    
    ko.bindingHandlers.command = {
        init: function(element, valueAccessor, allBindingsAccessor, viewModel) {
            var value = valueAccessor();
            var commands = value.execute ? {
                click: value
            } : value;

            for (var command in commands) {
                if (ko.bindingHandlers[command]) {
                    ko.bindingHandlers[command].init(
                    element, ko.utils.wrapAccessor(commands[command].execute), allBindingsAccessor, viewModel);
                }
                else {
                    var events = {};

                    for (var command in commands) {
                        events[command] = commands[command].execute;
                    }

                    ko.bindingHandlers.event.init(
                    element, ko.utils.wrapAccessor(events), allBindingsAccessor, viewModel);
                }
            }
        },

        update: function(element, valueAccessor, allBindingsAccessor, viewModel) {
            var commands = valueAccessor();
            var canExecute = commands.canExecute || (commands.click ? commands.click.canExecute : null);
            if (!canExecute) {
                return;
            }

            ko.bindingHandlers.enable.update(element, canExecute, allBindingsAccessor, viewModel);
        }
    };
})(ko);