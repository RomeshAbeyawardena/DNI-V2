using DNI.Data.Modules;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using DNI.Modules.Shared.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DNI.Mapper.Modules.Extensions;
using System.Threading.Tasks;
using System.Threading;
using DNI.Shared.Abstractions;
using DNI.Shared.Test;
using System;

namespace DNI.Test.App
{
    public class MyDbModule : ModuleBase
    {
        private readonly IRepository<Customer> userRepository;

        public override void ConfigureServices(IServiceCollection serviceCollection, IModuleConfiguration moduleConfiguration)
        {
            
        }

        public MyDbModule(
            IConfiguration configuration,
            IRepository<Customer> userRepository)
        {
            this.userRepository = userRepository;
        }

        public override void ConfigureModuleBuilder(IServiceCollection services, IModuleConfigurationBuilder moduleConfigurationBuilder)
        {
            moduleConfigurationBuilder.ConfigureDbContextModule(builder => builder.AddDbContext<MyDbContext>(ConfigureDbBuilder, ServiceLifetime.Scoped))
                .ConfigureMapperModule(c => c.AddModuleAssemblies());
        }

        private void ConfigureDbBuilder(IServiceProvider serviceProvider, DbContextOptionsBuilder builder)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            builder
                .UseSqlServer(configuration.GetConnectionString("default"));
        }

        public override Task OnStart(CancellationToken cancellationToken)
        {
            userRepository.Update(new Customer
            {
                Id = new Guid("8b25976a-08be-426d-b9e0-2e417b99f2c7"),
                BusinessEmailAddress = "test",
                DateOfBirth = new DateTime(2020, 01, 01),
                EmailAddress = "test",
                FirstName = "firstName",
                LastName = "lastname",
                MiddleName = "midle",
                MobileNumber = "012892347234",
                NationalInsuranceNumber = "ifjrijf",
                TelephoneNumber = "034893894",
                Title = "fewrj"
            });
            userRepository.SaveChanges();
            return base.OnStart(cancellationToken);
        }
    }
}
