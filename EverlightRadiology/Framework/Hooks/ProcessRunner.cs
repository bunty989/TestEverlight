using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverlightRadiology.Framework.Hooks
{
    internal class ProcessRunner : IDisposable
    {
        private readonly Process _process;

        public ProcessRunner(string fileName, string arguments)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            _process = new Process { StartInfo = startInfo };
        }

        public void Start()
        {
            _process.Start();
        }

        public void Stop()
        {
            if (!_process.HasExited)
            {
                _process.Kill();
                _process.WaitForExit();
            }
        }

        public void Dispose()
        {
            Stop();
            _process.Dispose();
        }
    }

    // Retry helper class for retrying logic with delays
    public static class RetryHelper
    {
        public static void Retry(Action action, int maxRetries, int delayMilliseconds)
        {
            for (var i = 0; i < maxRetries; i++)
            {
                try
                {
                    action();
                    return;
                }
                catch
                {
                    // Log or handle the exception if needed
                    Thread.Sleep(delayMilliseconds);
                }
            }

            // If still unsuccessful after retries, throw an exception
            throw new Exception("Retry attempts failed.");
        }

        public static async Task RetryAsync(Func<Task> action, int maxRetries, int delayMilliseconds)
        {
            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    await action();
                    return;
                }
                catch
                {
                    // Log or handle the exception if needed
                    await Task.Delay(delayMilliseconds);
                }
            }

            // If still unsuccessful after retries, throw an exception
            throw new Exception("Retry attempts failed.");
        }
    }
 }
