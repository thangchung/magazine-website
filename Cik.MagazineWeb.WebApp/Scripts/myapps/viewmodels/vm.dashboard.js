define('vm.dashboard',
    ['ko', 'underscore', 'datacontext'],
    function (ko, _, datacontext) {
        var
            categories = ko.observableArray(),
            activate = function(routeData, callback) {
                refresh(callback);
            },
            canLeave = function() {
                return true;
            },
            forceRefreshCmd = ko.asyncCommand({
                execute: function(complete) {
                }
            }),
            getCategories = function() {
                return datacontext.dashboard.getData({
                    results: categories
                });
            },
            refresh = function(callback) {
                this.categories = getCategories();
            },
            categoriesGridViewModel = new ko.simpleGrid.viewModel({
                data: categories,
                columns: [
                    { headerText: "id", rowText: "id" },
                    { headerText: "name", rowText: "name" },
                    { headerText: "createdBy", rowText: "createdBy" }
                ],
                pageSize: 1
            }),
            addItem = function() {
                this.items.push({ name: "New item", sales: 0, price: 100 });
            },
            sortByName = function() {
                this.items.sort(function(a, b) {
                    return a.name < b.name ? -1 : 1;
                });
            },
            jumpToFirstPage = function() {
                this.gridViewModel.currentPageIndex(0);
            };

        return {
            activate: activate,
            canLeave: canLeave,
            categories: categories,
            forceRefreshCmd: forceRefreshCmd,
            categoriesGridViewModel: categoriesGridViewModel,
            addItem: addItem,
            sortByName: sortByName,
            jumpToFirstPage: jumpToFirstPage
        };
    });