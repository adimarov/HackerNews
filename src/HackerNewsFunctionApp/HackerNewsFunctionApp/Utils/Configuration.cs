using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNewsFunctionApp.Utils
{
    class Configuration
    {

        public static IConfigurationRoot GetConfig(ExecutionContext context)
        {
            try
            {
                return new ConfigurationBuilder()
                                .SetBasePath(context.FunctionAppDirectory)
                                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                                .AddEnvironmentVariables()
                                .Build();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
