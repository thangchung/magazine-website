using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Cik.Domain;
using Cik.Services.Magazine.MagazineService.Command;
using Cik.Services.Magazine.MagazineService.Model;
using Cik.Services.Magazine.MagazineService.QueryModel;
using Microsoft.AspNetCore.Mvc;

namespace Cik.Services.Magazine.MagazineService.Controllers
{
    [Route("api/categories")]
    public class CategoryController : Controller
    {
        private readonly CategoryQueryModelFinder _categoryQueryModelFinder;
        private readonly ICommandHandler _commandHandler;

        public CategoryController(
            ICommandHandler commandHandler,
            CategoryQueryModelFinder categoryQueryModelFinder)
        {
            _commandHandler = commandHandler;
            _categoryQueryModelFinder = categoryQueryModelFinder;
        }

        [HttpGet]
        [Route("")]
        public async Task<IList<CategoryDto>> Get()
        {
            return await _categoryQueryModelFinder.Query().ToList();
        }

        [HttpGet("{id}")]
        public async Task<CategoryDto> Get(Guid id)
        {
            return await _categoryQueryModelFinder.Find(id);
        }

        [HttpPost]
        public Guid Post([FromBody] CreateCategoryCommand command)
        {
            var newGuid = Guid.NewGuid();
            command.Id = newGuid;
            _commandHandler.Send(command);
            return newGuid;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}