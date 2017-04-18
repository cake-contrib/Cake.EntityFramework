using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.EntityFramework.Tests
{
    internal static class Util
    {
        private static readonly string _BasePath;

        static Util()
        {
            _BasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory
                                                            .Replace("\\bin", string.Empty)
                                                            .Replace("Debug", string.Empty)
                                                            .Replace("Release", string.Empty)
                                                            .TrimEnd('\\'), "App_Data");

            if (!Directory.Exists(_BasePath))
                Directory.CreateDirectory(_BasePath);
        }

        internal static Process StartDockerComposeProcess(string dockerComposeYmlFilePath, string arguments, int milliseconds = 0)
        {
            var processStartInfo = new ProcessStartInfo()
            {
                Arguments = arguments ?? string.Empty,
                WindowStyle = ProcessWindowStyle.Normal,
                UseShellExecute = false,
                CreateNoWindow = true,
                FileName = "docker-compose",
                RedirectStandardOutput = false,
                WorkingDirectory = !string.IsNullOrEmpty(Path.GetDirectoryName(dockerComposeYmlFilePath)) ? Path.GetDirectoryName(dockerComposeYmlFilePath) : string.Empty
            };

            var process = new Process();
            process.StartInfo = processStartInfo;

            process.OutputDataReceived += (sender, e) =>
            {
                Debug.WriteLine(e.Data);
                Console.WriteLine(e.Data);
            };           

            process.Start();
            process.WaitForExit(milliseconds);
            return process;
        }
    }
}
