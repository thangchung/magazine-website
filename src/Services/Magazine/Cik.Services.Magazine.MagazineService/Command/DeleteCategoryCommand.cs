using System;

namespace Cik.Services.Magazine.MagazineService.Command
{
    public class DeleteCategoryCommand : CoreLibs.Domain.Command
    {
        public Guid Id { get; set; }
    }
}