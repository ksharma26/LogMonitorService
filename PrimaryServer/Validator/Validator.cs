using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using LogMonitorService.Models;

namespace LogMonitorService.Services
{
    public class Validator
    {
        private readonly AllowedIpsConfig _config;

        //for testing on windows platform
        //private readonly string logDirectory = "C:\\Users\\karasha\\Desktop";

        private readonly string logDirectory = "/var/log";
        public Validator(IOptions<AllowedIpsConfig> config)
        {
            _config = config.Value;

            // Debugging: Log the configuration values
            if (_config.AllowedIps == null)
            {
                Console.WriteLine("AllowedIps is null");
            }
            else
            {
                Console.WriteLine("AllowedIps: " + string.Join(", ", _config.AllowedIps));
            }
        }

        //Can add more validations 
        public bool IsValidIp(string ip)
        {
            return _config.AllowedIps != null && _config.AllowedIps.Contains(ip);
        }

        public bool isFileNameValid(string filename)
        {
            // Combine the filename with the log directory and get the full path
            string fullPath = Path.GetFullPath(Path.Combine(logDirectory, filename));

            Console.WriteLine("Karan full path: " + fullPath);

            // Ensure that the resulting path is within the log directory
            return fullPath.StartsWith(logDirectory);
        }
    }
}
