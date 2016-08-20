using System;

namespace Cik.Services.Magazine.MagazineService.Features.Category.Commands
{
    public class EditCategoryCommand : CoreLibs.Domain.Command
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}