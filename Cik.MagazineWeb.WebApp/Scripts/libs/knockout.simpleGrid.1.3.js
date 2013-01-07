(function () {
    // Private function
    function getColumnsForScaffolding(data) {
        if ((typeof data.length != 'number') || data.length == 0)
            return [];
        var columns = [];
        for (var propertyName in data[0])
            columns.push({ headerText: propertyName, rowText: propertyName });
        return columns;
    }

    ko.simpleGrid = {
        // Defines a view model class you can use to populate a grid
        viewModel: function (configuration) {
            this.data = configuration.data;
            this.currentPageIndex = ko.observable(0);
            this.pageSize = configuration.pageSize || 5;
            this.pageNumberButtonCount = configuration.pageNumberButtonCount || 10;

            // If you don't specify columns configuration, we'll use scaffolding
            this.columns = configuration.columns || getColumnsForScaffolding(ko.utils.unwrapObservable(this.data));

            this.itemsOnCurrentPage = ko.computed(function () {
                var startIndex = this.pageSize * this.currentPageIndex();
                return this.data.slice(startIndex, startIndex + this.pageSize);
            }, this);

            this.maxPageIndex = ko.computed(function () {
                return Math.ceil(ko.utils.unwrapObservable(this.data).length / this.pageSize);
            }, this);

            this.pageButtons = ko.computed(function () {
                var buttons = [];
                var firstButtonIndex = this.pageNumberButtonCount * Math.floor(this.currentPageIndex() / this.pageNumberButtonCount);
                for (var i = 0; i < 10; i++) {
                    if (this.maxPageIndex() - firstButtonIndex > i) {
                        buttons.push({
                            index: i + firstButtonIndex,
                            number: 1 + i + firstButtonIndex,
                            isActive: i === (this.currentPageIndex() % this.pageNumberButtonCount)
                        });
                    }
                    else {
                        break;
                    }
                }
                return buttons;
            }, this);
        }
    };

    // Templates used to render the grid
    var templateEngine = new ko.jqueryTmplTemplateEngine();

    // The "simpleGrid" binding
    ko.bindingHandlers.simpleGrid = {
        // This method is called to initialize the node, and will also be called again if you change what the grid is bound to
        update: function (element, viewModelAccessor, allBindingsAccessor) {
            var viewModel = viewModelAccessor(), allBindings = allBindingsAccessor();

            // Empty the element
            while (element.firstChild)
                ko.removeNode(element.firstChild);

            // Allow the default templates to be overridden
            var gridTemplateName = allBindings.simpleGridTemplate || "ko_simpleGrid_grid",
                pageLinksTemplateName = allBindings.simpleGridPagerTemplate || "ko_simpleGrid_pageLinks";

            // Render the main grid
            var gridContainer = element.appendChild(document.createElement("DIV"));
            ko.renderTemplate(gridTemplateName, viewModel, { templateEngine: templateEngine }, gridContainer, "replaceNode");

            // Render the page links
            var pageLinksContainer = element.appendChild(document.createElement("DIV"));
            ko.renderTemplate(pageLinksTemplateName, viewModel, { templateEngine: templateEngine }, pageLinksContainer, "replaceNode");
        }
    };
})();