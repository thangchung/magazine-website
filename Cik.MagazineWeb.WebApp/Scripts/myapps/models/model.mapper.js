define('model.mapper',
['model'],
    function (model) {
        var
            category = {
                getDtoId: function(dto) { return dto.id; },
                fromDto: function (dto, item) {
                    item = item || new model.Category().id(dto.id);
                    item.name(dto.name);
                    item.createdBy(dto.createdBy);
                    item.dirtyFlag().reset();
                    
                    return item;
                }
            };

        return {
            category: category
        };
    });