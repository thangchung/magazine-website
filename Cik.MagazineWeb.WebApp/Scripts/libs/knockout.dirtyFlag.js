// By: Hans Fjällemark and John Papa
// https://github.com/CodeSeven/KoLite
//
// Knockout.DirtyFlag
//
// John Papa 
//          http://johnpapa.net
//          http://twitter.com/@john_papa
//
// Depends on scripts:
//          Knockout 
//
//  Notes:
//          Special thanks to Steve Sanderson and Ryan Niemeyer for 
//          their influence and help.
//
//  Usage:      
//          To Setup Tracking, add this tracker property to your viewModel    
//              ===> viewModel.dirtyFlag = new ko.DirtyFlag(viewModel.model);
//
//          Hook these into your view ...
//              Did It Change?          
//              ===> viewModel.dirtyFlag().isDirty();
//
//          Hook this into your view model functions (ex: load, save) ...
//              Resync Changes
//              ===> viewModel.dirtyFlag().reset();
//
//          Optionally, you can pass your own hashFunction for state tracking.
//
////////////////////////////////////////////////////////////////////////////////////////
;(function (ko) {
        ko.DirtyFlag = function (objectToTrack, isInitiallyDirty, hashFunction) {

            hashFunction = hashFunction || ko.toJSON;

            var
                _objectToTrack = objectToTrack,
                _lastCleanState = ko.observable(hashFunction(_objectToTrack)),
                _isInitiallyDirty = ko.observable(isInitiallyDirty),

                result = function () {
                    var self = this;

                    self.isDirty = ko.computed(function () {
                        return _isInitiallyDirty() || hashFunction(_objectToTrack) !== _lastCleanState();
                    });

                    self.reset = function () {
                        _lastCleanState(hashFunction(_objectToTrack));
                        _isInitiallyDirty(false);
                    };

                    return self;
                };
            
            return result;
        };
    })(ko);