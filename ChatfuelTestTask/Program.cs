using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatfuelTestTask
{
	internal class Program
	{
		private static string[] _args; // a bit hacky but is better to require input as key-value instead of positional

		public static void Main( string[] args )
		{
			_args = args;

			IConfigurationRoot config = new ConfigurationBuilder()
				.Build();

			IWebHostBuilder builder = new WebHostBuilder()
				.UseConfiguration( config )
				.UseStartup<Startup>()
				.UseUrls( "http://localhost:5001" )
				.UseKestrel();

			IWebHost host = builder.Build();
			host.Run();
		}

		private class Startup
		{
			public void ConfigureServices( IServiceCollection services )
			{
				services.AddMvcCore(); // is enough since we are using mvc just as fancy async input provider
				services.AddSingleton( new Lift( Config.FromArgs( _args ) ) );
			}

			public void Configure( IApplicationBuilder app )
			{
				app.UseMvc();
			}
		}
	}
}
