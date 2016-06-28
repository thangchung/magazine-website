using System;
using System.Collections.Generic;
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
    private readonly ICommandHandler _commandBus;
    private readonly IQueryModelFinder<CategoryDto> _queryFinder;

    public CategoryController(
      ICommandHandler commandBus,
      IQueryModelFinder<CategoryDto> queryFinder)
    {
      Guard.NotNull(commandBus);
      Guard.NotNull(queryFinder);

      _commandBus = commandBus;
      _queryFinder = queryFinder;
    }

    [HttpGet]
    [Route("")]
    public async Task<IList<CategoryDto>> Get()
    {
      return await _queryFinder.Query();
    }

    [HttpGet("{id}")]
    public async Task<CategoryDto> Get(Guid id)
    {
      return await _queryFinder.Find(id);
    }

    [HttpPost]
    public void Post([FromBody] CreateCategoryCommand command)
    {
      var newGuid = Guid.NewGuid();
      command.Id = newGuid;
      _commandBus.Send(command);
    }

    [HttpPut]
    public void Put([FromBody] EditCategoryCommand command)
    {
      Guard.NotNullOrEmpty(command.Id.ToString());

      _commandBus.Send(command);
    }

    [HttpDelete("{id}")]
    public void Delete(Guid id)
    {
      Guard.NotNullOrEmpty(id.ToString());
      var command = new DeleteCategoryCommand {Id = id};
      _commandBus.Send(command);
    }
  }
}