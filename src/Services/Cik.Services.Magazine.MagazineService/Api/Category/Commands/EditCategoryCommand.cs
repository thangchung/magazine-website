using System;
using Cik.CoreLibs.Bus;

namespace Cik.Services.Magazine.MagazineService.Api.Category.Commands
{
    public class EditCategoryCommand : Command
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}