using System;

namespace Cik.Services.Magazine.MagazineService.Api.Category.Commands
{
    public class DeleteCategoryCommand : CoreLibs.Domain.Command
    {
        public Guid Id { get; set; }
    }
}