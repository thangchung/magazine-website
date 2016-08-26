using System;
using System.Threading.Tasks;
using Cik.CoreLibs.Bus.Amqp;
using Cik.CoreLibs.Domain;

namespace Cik.Services.Magazine.MagazineService.Api.Category.Commands.Handlers
{
    public class DeleteCategoryCommandHandler : RabbitMqCommandHandler<DeleteCategoryCommand>
    {
        private readonly IRepository<Entities.Category, Guid> _categoryRepository;

        public DeleteCategoryCommandHandler(
            IServiceProvider serviceProvider,
            IRepository<Entities.Category, Guid> repo) : base(serviceProvider)
        {
            _categoryRepository = repo;
        }

        public override Task HandleAsync(DeleteCategoryCommand message)
        {
            throw new NotImplementedException();
        }
    }
}