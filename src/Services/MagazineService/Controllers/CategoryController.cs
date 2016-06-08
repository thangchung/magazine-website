using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cik.Services.MagazineService.Model;
using Cik.Services.MagazineService.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Reactive.Linq;
using Cik.Services.MagazineService.Service;

namespace Cik.Services.MagazineService.Controllers
{
    [Route("api/categories")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IList<CategoryViewModel>> Get()
        {
            var categoryObserable = _categoryService.GetAll();
            return await categoryObserable.ToList();
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public async Task<Guid> Post(Category cat)
        {
            cat.Id = Guid.NewGuid(); //TODO: temporary to put here
            return await _categoryService.Create(cat);
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