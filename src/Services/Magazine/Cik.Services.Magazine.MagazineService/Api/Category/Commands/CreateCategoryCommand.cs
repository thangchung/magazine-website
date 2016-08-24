using System;

namespace Cik.Services.Magazine.MagazineService.Api.Category.Commands
{
    public class CreateCategoryCommand : CoreLibs.Domain.Command
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}