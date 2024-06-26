﻿using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.User.Application.User
{
    public class StartupBackgroundService : BackgroundService
    {
        private readonly StartupHealthCheck _healthCheck;

        public StartupBackgroundService(StartupHealthCheck healthCheck)
            => _healthCheck = healthCheck;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Simulate the effect of a long-running task.
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

            _healthCheck.StartupCompleted = true;
        }
    }
}
