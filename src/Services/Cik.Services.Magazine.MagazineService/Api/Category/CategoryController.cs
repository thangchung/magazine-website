using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Cik.CoreLibs;
using Cik.CoreLibs.Bus;
using Cik.CoreLibs.Model;
using Cik.Services.Magazine.MagazineService.Api.Category.Commands;
using Cik.Services.Magazine.MagazineService.Api.Category.Dtos;
using Microsoft.AspNetCore.Mvc;
using ControllerBase = Cik.CoreLibs.Api.ControllerBase;

namespace Cik.Services.Magazine.MagazineService.Api.Category
{
    [Route("api/categories") /* Authorize */]
    public class CategoryController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryModelFinder<CategoryDto> _queryFinder;

        public CategoryController(
            ICommandBus commandBus,
            IQueryModelFinder<CategoryDto> queryFinder)
        {
            Guard.NotNull(commandBus);
            Guard.NotNull(queryFinder);

            _commandBus = commandBus;
            _queryFinder = queryFinder;
        }

        [Route(""), HttpGet /* Authorize("data_category_records_user") */]
        public async Task<IList<CategoryDto>> Get()
        {
            return await _queryFinder.QueryItemStream().ToList();
        }

        [HttpGet("{id}") /* Authorize("data_category_records_user") */]
        public async Task<CategoryDto> Get(Guid id)
        {
            return await _queryFinder.FindItemStream(id);
        }

        [HttpPost /* Authorize("data_category_records_admin") */]
        public async Task<IActionResult> Post([FromBody] CreateCategoryCommand command)
        {
            command.Id = Guid.NewGuid();
            return await _commandBus
                .Send(command)
                .SelectMany(x => OkResult());
        }

        [HttpPut /* Authorize("data_category_records_admin") */]
        public async Task<IActionResult> Put([FromBody] EditCategoryCommand command)
        {
            Guard.NotNullOrEmpty(command.Id.ToString());
            return await _commandBus
                .Send(command)
                .SelectMany(x => OkResult());
        }

        [HttpDelete("{id}") /* Authorize("data_category_records_admin") */]
        public async Task<IActionResult> Delete(Guid id)
        {
            Guard.NotNullOrEmpty(id.ToString());
            return await _commandBus
                .Send(new DeleteCategoryCommand {Id = id})
                .SelectMany(x => OkResult());
        }
    }
}