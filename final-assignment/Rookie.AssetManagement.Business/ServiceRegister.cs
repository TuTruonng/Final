﻿using Microsoft.Extensions.DependencyInjection;
using Rookie.AssetManagement.Business.Interfaces;
using Rookie.AssetManagement.Business.Services;
using System.Reflection;

namespace Rookie.AssetManagement.Business
{
    public static class ServiceRegister
    {
        public static void AddBusinessLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient(typeof(IUserService), typeof(UserService));
            services.AddTransient(typeof(IAssetService), typeof(AssetService));
            services.AddTransient(typeof(IAssignmentService), typeof(AssignmentServive));
        }
    }
}