using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSharp_Aspnetcore_DockerAndSqlServer
{
	public class Startup
	{
		private IConfiguration _config;

		public Startup(IConfiguration config)
		{
			_config = config;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddHealthChecks()
				.AddSqlServer(_config["connectionStrings:sqlserver"]);
					//TODO CARLOS - you'll see that if you use this one, it fails even though I have port 1433.  If you find your IP and replace the name with it, check will change to healthy.  To find IP:
					//> docker network ls
					//> docker network inspect <your network here - mine is dockercompose9318822996251545401_default>
					//> find the ip for sqlserverContainer
					//you can also get into your container to verify ping resolves into the proper IP:
					//> docker ps
					//> docker exec -it <your web api container here - mine is ece485c89701> bash
					//> ping 
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHealthChecks("/healthz");

			app.Run(async (context) =>
			{
				await context.Response.WriteAsync("Hello World!");
			});
		}
	}
}
