using ALTENBooking.Data;
using ALTENBooking.Data.Context;
using ALTENBooking.Domain.Interfaces;
using ALTENBooking.Domain.Models;
using ALTENBooking.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace ALTENBooking.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<EFInMemoryDbContext>(options => options.UseInMemoryDatabase("alten.db"));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            AddServicesDI(builder.Services);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }            

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            var scope = app.Services.CreateScope();
            var dbcontext = scope.ServiceProvider.GetService<EFInMemoryDbContext>();

            AddRoom(dbcontext);

            app.Run();
        }   
        
        private static void AddServicesDI(IServiceCollection services)
        {               
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IBookingService), typeof(BookingService));
            services.AddScoped(typeof(ICurrentDateTime), typeof(CurrentDateTime));
            services.AddScoped(typeof(ICustomerService), typeof(CustomerService));            

            services.AddScoped(typeof(Application.Interfaces.IBookingService), typeof(Application.Services.BookingService));
        }

        public static void AddRoom(EFInMemoryDbContext context)
        {
            context.Rooms.Add(new Room { Id = new Guid("61F3E658-B377-4DC5-96B1-AD64EF2E03AE"), Type = Domain.Enums.RoomType.DOUBLE });
            context.SaveChanges();
        }
    }
}