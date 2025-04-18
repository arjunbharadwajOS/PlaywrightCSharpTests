using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIAutomation_Playwright
{
    public class ApplicationConfiguration
    {
        private static IConfigurationRoot _configuration = null;
        static ApplicationConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _configuration = builder.Build();
        }
        public static string GetSetting(string key)
        {
            return _configuration[key].ToString();
        }

    }
}
