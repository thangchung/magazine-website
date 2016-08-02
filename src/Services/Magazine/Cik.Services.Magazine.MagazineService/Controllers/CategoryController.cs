using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cik.CoreLibs.Domain;
using Cik.Services.Magazine.MagazineService.Command;
using Cik.Services.Magazine.MagazineService.Model;
using Cik.Services.Magazine.MagazineService.QueryModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cik.Services.Magazine.MagazineService.Controllers
{
    // [Authorize]
    [Route("api/categories")]
    public class CategoryController : Cik.CoreLibs.Api.ControllerBase
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

        [HttpGet]
        [Route("")]
        // [Authorize("data_category_records_user")]
        public async Task<IList<CategoryDto>> Get()
        {
            return await _queryFinder.Query();
        }

        [HttpGet("{id}")]
        // [Authorize("data_category_records_user")]
        public async Task<CategoryDto> Get(Guid id)
        {
            return await _queryFinder.Find(id);
        }

        [HttpPost]
        // [Authorize("data_category_records_admin")]
        public async Task<OkResult> Post([FromBody] CreateCategoryCommand command)
        {
            command.Id = Guid.NewGuid();
            await _commandBus.SendAsync(command);
            return await OkResult();
        }

        [HttpPut]
        // [Authorize("data_category_records_admin")]
        public async Task<OkResult> Put([FromBody] EditCategoryCommand command)
        {
            Guard.NotNullOrEmpty(command.Id.ToString());
            await _commandBus.SendAsync(command);
            return await OkResult();
        }

        [HttpDelete("{id}")]
        // [Authorize("data_category_records_admin")]
        public async Task<OkResult> Delete(Guid id)
        {
            Guard.NotNullOrEmpty(id.ToString());
            await _commandBus.SendAsync(new DeleteCategoryCommand { Id = id });
            return await OkResult();
        }
    }
}