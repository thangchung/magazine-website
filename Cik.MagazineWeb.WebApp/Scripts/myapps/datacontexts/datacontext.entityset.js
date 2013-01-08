define('datacontext.entityset',
    ['datacontext.utilities', 'jquery', 'ko', 'config', 'utils'],
    function (dcutils, $, ko, config, utils) {
        var logger = config.logger,
            entitySet = function (getFunction, mapper, nullo, updateFunction) {
            var items = { },
                // returns the model item produced by merging dto into context
                mapDtoToContext = function (dto) {
                    var id = mapper.getDtoId(dto);
                    var existingItem = items[id];
                    items[id] = mapper.fromDto(dto, existingItem);
                    return items[id];
                },
                add = function(newObj) {
                    items[newObj.id()] = newObj;
                },
                removeById = function(id) {
                    delete items[id];
                },
                getLocalById = function(id) {
                    // This is the only place we set to NULLO
                    return !!id && !!items[id] ? items[id] : nullo;
                },
                getAllLocal = function() {
                    return utils.mapMemoToArray(items);
                },
                getData = function(options) {
                    return $.Deferred(function (def) {
                        var results = options && options.results,
                            sortFunction = options && options.sortFunction,
                            filter = options && options.filter,
                            forceRefresh = options && options.forceRefresh,
                            param = options && options.param,
                            getFunctionOverride = options && options.getFunctionOverride;

                        getFunction = getFunctionOverride || getFunction;

                        // If the internal items object doesnt exist, 
                        // or it exists but has no properties, 
                        // or we force a refresh
                        if (forceRefresh || !items || !utils.hasProperties(items)) {
                            getFunction({
                                success: function (dtoList, xhr) {
                                    items = dcutils.mapToContext(dtoList, items, results, mapper, filter, sortFunction);
                                    def.resolve(results);
                                },
                                error: function(response, xhr) {
                                    logger.error(config.toasts.errorGettingData);
                                    def.reject();
                                }
                            }, param);
                        } else {
                            dcutils.itemsToArray(items, results, filter, sortFunction);
                            def.resolve(results);
                        }
                    }).promise();
                },
                updateData = function(entity, callbacks) {
                    var entityJson = ko.toJSON(entity);

                    return $.Deferred(function(def) {
                        if (!updateFunction) {
                            logger.error('updateData method not implemented');
                            if (callbacks && callbacks.error) {
                                callbacks.error();
                            }
                            def.reject();
                            return;
                        }

                        updateFunction({
                            success: function (response, xhr) {
                                logger.success(config.toasts.savedData);
                                entity.dirtyFlag().reset();
                                if (callbacks && callbacks.success) {
                                    callbacks.success();
                                }
                                def.resolve(response);
                            },
                            error: function(response, xhr) {
                                logger.error(config.toasts.errorSavingData);
                                if (callbacks && callbacks.error) {
                                    callbacks.error();
                                }
                                def.reject(response);
                                return;
                            }
                        }, entityJson);
                    }).promise();
                };

            return {
                mapDtoToContext: mapDtoToContext,
                add: add,
                getAllLocal: getAllLocal,
                getLocalById: getLocalById,
                getData: getData,
                removeById: removeById,
                updateData: updateData
            };
        };

        createEntitySet = function(getFunction, mapper, nullo, updateFunction) {
            return new entitySet(getFunction, mapper, nullo, updateFunction);
        };

        return {
            EntitySet: entitySet,
            createEntitySet: createEntitySet
        };
    });