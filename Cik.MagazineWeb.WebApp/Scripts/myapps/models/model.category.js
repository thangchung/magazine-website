define('model.category',
    ['ko'],
    function(ko) {

        var _dc = null,
            categoryMakeId = function(id) {
                return id;
            },
            Category = function() {
                var self = this;
                self.id = ko.observable();
                self.name = ko.observable();
                self.createdBy = ko.observable();

                self.isNullo = false;
                self.dirtyFlag = new ko.DirtyFlag([self.name]);
                return self;
            };

        Category.Nullo = new Category()
            .id(0)
            .name('')
            .createdBy('');

        Category.Nullo.isNullo = true;
        Category.Nullo.dirtyFlag().reset();

        Category.makeId = categoryMakeId;

        Category.datacontext = function(dc) {
            if (dc) {
                _dc = dc;
            }
            return _dc;
        };

        Category.prototype = function() {
            return {
                isNullo: false
            };
        }();

        return Category;
    });

