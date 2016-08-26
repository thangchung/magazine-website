using System;
using Cik.CoreLibs.Bus;

namespace Cik.Services.Magazine.MagazineService.Api.Category.Commands
{
    public class DeleteCategoryCommand : Command
    {
        public Guid Id { get; set; }
    }
}