﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Domain.Entities;

namespace Skillshare.Domain
{
    public static class DomainServiceRegistration
    {
        public static IServiceCollection InfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}