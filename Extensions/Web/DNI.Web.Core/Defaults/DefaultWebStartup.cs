using DNI.Web.Core.Defaults.Builders;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace DNI.Web.Core.Defaults
{
    public class DefaultWebStartup
    {
        public void ConfigureContainer(DefaultWebServiceProviderBuilder builder)
        {
            // Register your own things directly with Autofac here. Don't
            // call builder.Populate(), that happens in AutofacServiceProviderFactory
            // for you.
            //builder.RegisterModule(new MyApplicationModule());
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            // If, for some reason, you need a reference to the built container, you
            // can use the convenience extension method GetAutofacRoot.
            //app.ApplicationServices = ;

        }
    }
}
