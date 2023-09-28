using DATA_Layer.DBContexts;
using DATA_Layer.Interfaces;
using DATA_Layer.Repositories;
using SERVICE_Layer.Interfaces;
using SERVICE_Layer.Services;

namespace API_Layer.Extensions;

public static class ServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        //Repositories
        services.AddTransient<IDepartmentsRepository, DepartmentsRepository>();

        //Services        
        services.AddTransient<IDepartmentService, DepartmentService>();
    }
}
