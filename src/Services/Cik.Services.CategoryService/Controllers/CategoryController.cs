using System.Collections.Generic;
using Cik.Data.Abstraction;
using Cik.Services.CategoryService.Model;
using Microsoft.AspNetCore.Mvc;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Cik.Services.CategoryService.Controllers
{
    [Route("api/categories")]
    public class CategoryController : Controller
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryController(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET api/values
        [HttpGet]
        [Route("")]
        public async Task<IList<Category>> Get()
        {
            var categoryObserable = _categoryRepository.GetAll();
            return await categoryObserable.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}