using System;
using System.Collections.Generic;

namespace TestConsole
{
    static partial class Program
    {
        public abstract class Logger
        {
            public abstract void Log(string Message);
            

            public void LogInformation(string Message)
            {
                Log($"[info]:{Message}");
            }

            public void LogWarning(string Message)
            {
                Log($"[warn]:{Message}");
            }

            public void LogError(string Message)
            {
                Log($"[error]:{Message}");
            }
        }

        public class ListLogger : Logger
        {

            private readonly List<string> _Messages = new List<string>();

            // public string[] Messages => _Messages.ToArray();
            public string[] Messages
            {
                get
                {
                    return _Messages.ToArray();
                }
            }

            public override void Log(string Message)
            {
                _Messages.Add($"({ DateTime.Now}) {Message}");

            }

        }

        public class FileLogger : Logger
        {
            private int _Index;


            public string FilePath { get; }

            public FileLogger(string FilePath) { this.FilePath = FilePath; }

            public override void Log(string Message)
            {
                System.IO.File.AppendAllText(FilePath, $"{++_Index}:{Message}\r\n");
            }


        }

        public abstract class DebugLogger : Logger
        {
            public abstract void Log(string Message, string Category);
        }

        public class VisualStudioOutputLogger : DebugLogger
        {
            public override void Log(string Message)
            {
                System.Diagnostics.Debug.WriteLine($">>>>>>>> {Message}");
            }

            public override void Log(string Message, string Category)
            {
                System.Diagnostics.Debug.WriteLine($">>>>>>>> {Message}", Category);
            }
        }

        public class TraceLogger : DebugLogger
        {
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
