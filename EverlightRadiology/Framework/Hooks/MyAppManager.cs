
using Serilog;

namespace EverlightRadiology.Framework.Hooks
{
    public class MyAppManager
    {
        private static ProcessRunner _processRunner;
        private static string _baseUrl;

        public static void StartApplication()
        {
            var assemblyDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var appCsProj = Path.GetFullPath(Path.Combine(assemblyDirectory, @"..\..\..\..\AutomationTestSample\AutomationTestSample.csproj"));
            // Start your .NET Core application
            _processRunner = new ProcessRunner("dotnet", "run --project " + appCsProj);
            _processRunner.Start();

            _baseUrl = "https://localhost:7150";

            // Retry logic to wait for the application to start
            RetryHelper.Retry(() =>
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        var response = client.GetAsync(_baseUrl).Result;
                        response.EnsureSuccessStatusCode(); // Throws exception for non-success status codes

                        // Application is running, no need to retry
                    }
                    catch
                    {
                        Log.Error("Application not reachable yet.");
                        Console.WriteLine("Application not reachable yet.");
                        // Application not yet running, retry
                        throw new Exception("Application not reachable yet.");
                    }
                }
            }, maxRetries: 30, delayMilliseconds: 1000); // Adjust the retry count and delay as needed
        }

        public static void StopApplication()
        {
            _processRunner?.Stop();
        }

        public string GetBaseUrl()
        {
            return _baseUrl;
        }
    }
}
