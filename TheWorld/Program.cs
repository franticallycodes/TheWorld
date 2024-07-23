using Serilog;
using Serilog.Formatting.Compact;
using TheWorld;

Log.Logger = new LoggerConfiguration()
	// .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
	// .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Warning)
	.Enrich.FromLogContext()
	.Enrich.WithProcessId()
	.Enrich.WithThreadId()
	.WriteTo.Console(new RenderedCompactJsonFormatter())
	.WriteTo.File("./LogFiles/WebAPI-.log", rollingInterval: RollingInterval.Day)
	.CreateLogger();

try
{
	var builder = WebApplication.CreateBuilder(args);

	// Add services to the container.
	builder.Services.AddLogging();

	builder.Services.AddControllers();

	builder.Services.AddHttpClient<ICountriesClient, RestCountriesApiClient>(client =>
	{
		client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]);
	});

	// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen();

	builder.Host.UseSerilog();

	var app = builder.Build();

	app.UseSerilogRequestLogging(options =>
	{
		// Customize the message template
		options.MessageTemplate = "API Handled {RequestPath}";

		// Emit debug-level events instead of the defaults
		// options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;

		// Attach additional properties to the request completion event
		options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
		{
			diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
			diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
		};
	});

	app.UseSwagger();
	app.UseSwaggerUI(); // access at /swagger

	app.UseHttpsRedirection();

	app.UseAuthorization();

	app.MapControllers();

	app.Run();
}
catch (Exception exception)
{
	Log.Fatal(exception, "Application terminated unexpectedly");
	throw;
}
finally
{
	Log.CloseAndFlush();
}