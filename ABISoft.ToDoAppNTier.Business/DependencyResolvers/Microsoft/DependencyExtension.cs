using ABISoft.ToDoAppNTier.Business.Interfaces;
using ABISoft.ToDoAppNTier.Business.Mappings.AutoMapper;
using ABISoft.ToDoAppNTier.Business.Services;
using ABISoft.ToDoAppNTier.Business.ValidationRules;
using ABISoft.ToDoAppNTier.DataAccess.Contexts;
using ABISoft.ToDoAppNTier.DataAccess.UnitofWork;
using ABISoft.ToDoAppNTier.Dtos.WorkDtos;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace ABISoft.ToDoAppNTier.Business.DependencyResolvers.Microsoft
{
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddDbContext<ToDoContext>(
                options => options.UseSqlServer("Data Source=DESKTOP-473BNVL; Initial Catalog=ToDoDb1; User id=sa; Password=mssql1234;")
                .LogTo(Console.WriteLine, LogLevel.Information)
            );

            SetAutoMapper(services);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IWorkService, WorkService>();
            services.AddTransient<IValidator<WorkCreateDto>, WorkCreateDtoValidator>();
            services.AddTransient<IValidator<WorkUpdateDto>, WorkUpdateDtoValidator>();
        }

        public static void SetAutoMapper(IServiceCollection services)
        {
            var configuration = new MapperConfiguration(opt => {
                opt.AddProfile(new WorkProfile()); 
            });
            var mapper = configuration.CreateMapper();
            services.AddSingleton<IMapper>(mapper); 
            //services.AddSingleton(mapper);   
        }
    }
}
