using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Vanir.API.Configuration.Services
{
    public interface IServicesConfigurator
    {
        void ConfigureServices(IServiceCollection services, IConfiguration configuration);
    }
}