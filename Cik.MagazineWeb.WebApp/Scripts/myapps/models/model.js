define('model',
    [
        'model.category'
    ],
    function (category) {
        var
            model = {
                Category: category
            };

        model.setDataContext = function (dc) {
            // Model's that have navigation properties 
            // need a reference to the datacontext.
            model.Category.datacontext(dc);
        };

        return model;
    });