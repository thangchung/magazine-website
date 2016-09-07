using System;
using Cik.CoreLibs;

namespace Cik.Services.Magazine.MagazineService.Api.Category.Entities
{
    public class CategoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CategoryFactory(IServiceProvider serviceProvider)
        {
            Guard.NotNull(serviceProvider);
            _serviceProvider = serviceProvider;
        }
    }
}