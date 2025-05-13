using AI.Services;

namespace AI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddProblemDetails();

            builder.Services.AddTransient<ILlamaService, LlamaService>();

            var app = builder.Build();
            app.UseRouting();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseEndpoints(endpoints =>
                {
                    var _ = endpoints.MapGet("/", async context =>
                    {
                        context.Response.Redirect("/swagger");
                    });
                });
            }

            app.MapControllers();

            app.Run();
        }
    }
}