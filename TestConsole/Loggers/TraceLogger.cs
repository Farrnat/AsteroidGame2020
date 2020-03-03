using System;
using System.Diagnostics;

namespace TestConsole.Loggers
{
    static partial class Program
    {
        public class TraceLogger : DebugLogger, IDisposable
        {
            public void Dispose()
            {
                Trace.Flush();
            }

            public override void Log(string Message)
            {
                System.Diagnostics.Trace.WriteLine($">>>>>>>> {Message}");
            }

            public override void Log(string Message, string Category)
            {
                System.Diagnostics.Trace.WriteLine($">>>>>>>> {Message}", Category);
            }

        }
    }

    
}
