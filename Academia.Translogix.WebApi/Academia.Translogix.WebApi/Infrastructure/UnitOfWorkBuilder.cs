using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase;

using Farsiman.Domain.Core.Standard.Repositories;
using Farsiman.Infraestructure.Core.Entity.Standard;

namespace Academia.Translogix.WebApi.Infrastructure
{
    public class UnitOfWorkBuilder
    {
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWorkBuilder (IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IUnitOfWork BuildDbTranslogix()
        {
            var dbContext = _serviceProvider.GetRequiredService<TranslogixDBContext>();
            return new UnitOfWork(dbContext);
        }
    }
}
