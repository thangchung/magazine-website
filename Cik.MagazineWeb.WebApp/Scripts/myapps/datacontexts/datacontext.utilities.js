define('datacontext.utilities',
    ['underscore', 'config', 'utils'],
    function(_, config, utils) {
    	var logger = config.logger,
    	    itemsToArray = function (items, observableArray, filter, sortFunction) {
            // Maps the memo to an observableArray, 
            // then returns the observableArray
            if (!observableArray) return;

            // Create an array from the memo object
            var underlyingArray = utils.mapMemoToArray(items);

            if (filter) {
                underlyingArray = _.filter(underlyingArray, function(o) {
                    var match = filter.predicate(filter, o);
                    return match;
                });
            }
    	        
            if (sortFunction) {
                underlyingArray.sort(sortFunction);
            }
    	        
            // logger.info('Fetched, filtered and sorted ' + underlyingArray.length + ' records');
            observableArray(underlyingArray);
        },
            mapToContext = function (dtoList, items, results, mapper, filter, sortFunction) {
                // Loop through the raw dto list and populate a dictionary of the items
                items = _.reduce(dtoList, function(memo, dto) {
                    var id = mapper.getDtoId(dto);
                    var existingItem = items[id];
                    memo[id] = mapper.fromDto(dto, existingItem);
                    return memo;
                }, {});
                
                itemsToArray(items, results, filter, sortFunction);
                // logger.success('received with ' + dtoList.length + ' elements');
                return items; // must return these
            };

        return {
            itemsToArray: itemsToArray,
            mapToContext: mapToContext
        };
    });