namespace Cik.MagazineWeb.WebApp.Infras.ViewModels
{
    using System;

    /// <summary>
    ///   Paging model using for pagination control
    /// </summary>
    public class PagingViewModel
    {
        private readonly int _defaultDisplayingPage = 10;

        public PagingViewModel(int defaultDisplayingPage = 10)
        {
            this._defaultDisplayingPage = defaultDisplayingPage;
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "PagingViewModel" /> class.
        /// </summary>
        /// <param name = "pageIndex">Index of the page.</param>
        /// <param name = "pageSize">Size of the page.</param>
        /// <param name = "totalCount">The total count.</param>
        /// <param name = "pageActionLink">The page action link.</param>
        /// <param name = "cssClass">The CSS class.</param>
        public PagingViewModel(int pageIndex, int pageSize, int totalCount, string pageActionLink, string cssClass)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.TotalCount = totalCount;
            this.PageActionLink = pageActionLink;
            this.CssClass = cssClass;
            this.PagesToDisplay = this.TotalPages < this._defaultDisplayingPage ? this.TotalPages : this._defaultDisplayingPage;
        }

        /// <summary>
        ///   Gets or sets the index of the page.
        ///   It's COUNT from 1
        /// </summary>
        /// <value>The index of the page.</value>
        public int PageIndex { get; set; }

        /// <summary>
        ///   Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize { get; set; }

        /// <summary>
        ///   Gets or sets the total count.
        /// </summary>
        /// <value>The total count.</value>
        public int TotalCount { get; set; }

        /// <summary>
        ///   Gets or sets the page action link.
        /// </summary>
        /// <value>The page action link.</value>
        public string PageActionLink { get; set; }

        /// <summary>
        ///   Gets or sets the pages to display.
        /// </summary>
        /// <value>The pages to display.</value>
        public int PagesToDisplay { get; set; }

        /// <summary>
        ///   Gets or sets the CSS class.
        /// </summary>
        /// <value>The CSS class.</value>
        public string CssClass { get; set; }

        /// <summary>
        ///   Gets the total pages.
        /// </summary>
        /// <value>The total pages.</value>
        public int TotalPages
        {
            get { return this.TotalCount / this.PageSize + (this.TotalCount % this.PageSize == 0 ? 0 : 1); }
        }

        /// <summary>
        ///   Gets a value indicating whether this instance has previous page.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has previous page; otherwise, <c>false</c>.
        /// </value>
        public bool HasPreviousPage
        {
            get { return this.PageIndex > 1; }
        }

        /// <summary>
        ///   Gets a value indicating whether this instance has next page.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has next page; otherwise, <c>false</c>.
        /// </value>
        public bool HasNextPage
        {
            get { return this.PageIndex < this.TotalPages; }
        }

        /// <summary>
        ///   Gets the min item index of current page.
        /// </summary>
        /// <value>The min item index of current page.</value>
        public int MinItemIndexOfCurrentPage
        {
            get
            {
                if (0 == this.TotalCount)
                {
                    return 0;
                }

                return ((this.PageIndex - 1) * this.PageSize) + 1;
            }
        }

        /// <summary>
        ///   Gets the max item index of current page.
        /// </summary>
        /// <value>The max item index of current page.</value>
        public int MaxItemIndexOfCurrentPage
        {
            get { return Math.Min(this.PageIndex * this.PageSize, this.TotalCount); }
        }

        /// <summary>
        ///   Gets the max page index to display.
        /// </summary>
        /// <value>The max page index to display.</value>
        public int MaxPageIndexToDisplay
        {
            get
            {
                var expectedMaxPageIndex = this.PageIndex + this.PagesToDisplay / 2;
                if (expectedMaxPageIndex > this.TotalPages)
                {
                    return this.TotalPages;
                }

                var maxPageIndex = this.PagesToDisplay > expectedMaxPageIndex ? this.PagesToDisplay : expectedMaxPageIndex;
                return maxPageIndex;
            }
        }

        /// <summary>
        ///   Gets the min page index for displaying.
        /// </summary>
        /// <value>The min page index for displaying.</value>
        public int MinPageIndexForDisplaying
        {
            get { return this.MaxPageIndexToDisplay - this.PagesToDisplay + 1; }
        } 
    }
}