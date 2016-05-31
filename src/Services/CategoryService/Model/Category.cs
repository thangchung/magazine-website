using System;
using Cik.Data.Entity.Abstraction;

namespace Cik.Services.CategoryService.Model
{
    public class Category : EntityBase
    {
        public string Name { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}