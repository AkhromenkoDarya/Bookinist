using Bookinist.DAL.Entities;
using Bookinist.DAL.Repositories;
using Bookinist.DAL.Repositories.RelatedEntityRepositories;
using Bookinist.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Bookinist.DAL.Services.Registration
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositoriesInDatabase(this IServiceCollection services) 
            => services
                .AddTransient<IRepository<Category>, DatabaseRepository<Category>>()
                .AddTransient<IRepository<Book>, BookRepository>()
                .AddTransient<IRepository<Seller>, DatabaseRepository<Seller>>()
                .AddTransient<IRepository<Buyer>, DatabaseRepository<Buyer>>()
                .AddTransient<IRepository<Deal>, DealRepository>()
            ;
    }
}
