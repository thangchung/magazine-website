using System;

namespace Cik.Services.Magazine.MagazineService.Features.Category.Commands
{
    public class DeleteCategoryCommand : CoreLibs.Domain.Command
    {
        public Guid Id { get; set; }
    }
}