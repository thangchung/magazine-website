define('vm.dashboard',
    ['ko', 'underscore', 'datacontext'],
    function (ko, _, datacontext) {
        
        var categoriesPagedGridModel = function (cats) {
            // this.cats = ko.observableArray(cats);

            this.sortByName = function () {
                this.cats.sort(function (a, b) {
                    return a.name < b.name ? -1 : 1;
                });
            };

            this.jumpToFirstPage = function () {
                this.gridViewModel.currentPageIndex(0);
            };

            this.gridViewModel = new ko.simpleGrid.viewModel({
                data: this.cats,
                columns: [
                    { headerText: "Id", rowText: "id" },
                    { headerText: "Name", rowText: "name" },
                    { headerText: "Created By", rowText: "createdBy" }
                ],
                pageSize: 1
            });

            return {
                gridViewModel: gridViewModel
            };
        };
        
        var categories = ko.observableArray(),
            activate = function (routeData, callback) {
                refresh(callback);
            },
            canLeave = function() {
                return true;
            },
            forceRefreshCmd = ko.asyncCommand({
                execute: function(complete) {
                }
            }),
            getCategories = function () {
                return datacontext.dashboard.getData({
                    results: categories
                });
            },
            refresh = function (callback) {
                this.categories = getCategories();

                // console.log(new categoriesPagedGridModel(this.categories));
                this.categoriesPagedGrid = new categoriesPagedGridModel(this.categories);​
                // this.categoriesPagedGrid = 'abc';
            };
            // categoriesPagedGrid = null;

        return {
            activate: activate,
            canLeave: canLeave,
            categories: categories,
            forceRefreshCmd: forceRefreshCmd
            //categoriesPagedGrid: categoriesPagedGrid
        };
    });