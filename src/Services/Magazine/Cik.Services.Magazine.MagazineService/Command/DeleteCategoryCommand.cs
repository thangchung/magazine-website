using System;

namespace Cik.Services.Magazine.MagazineService.Command
{
    public class DeleteCategoryCommand : Shared.Domain.Command
    {
        public Guid Id { get; set; }
    }
}